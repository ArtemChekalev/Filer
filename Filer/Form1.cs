using System;
using System.IO;
using System.Diagnostics;
using System.IO.Compression;
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.VisualBasic.FileIO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Net;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Filer
{
    public partial class Form1 : Form
    {
        ListViewItem item;
        string itempath;
        UserPrefs prefs = new UserPrefs();
        string login;
        string password;
        bool flag;
        bool bookflag;
        static CancellationToken token;
        public Form1()
        {
            InitializeComponent();
            prefs = UserPrefs.Get();
            SetPrefs();
            Check(flag);
            Disks();
            
            ToolStripMenuItem copyMenuItem = new ToolStripMenuItem("Копировать");
            ToolStripMenuItem pasteMenuItem = new ToolStripMenuItem("Вставить");
            ToolStripMenuItem deleteMenuItem = new ToolStripMenuItem("Удалить");
            ToolStripMenuItem renameMenuItem = new ToolStripMenuItem("Переименовать");
            ToolStripMenuItem archiveMenyItem = new ToolStripMenuItem("Архивировать");
            ToolStripMenuItem downloadMenuItem = new ToolStripMenuItem("Скачать в");
            ToolStripMenuItem cancelMenuItem = new ToolStripMenuItem("Остановить");
            ContextMenuStrip.Items.AddRange(new[] { copyMenuItem, pasteMenuItem, deleteMenuItem, renameMenuItem, archiveMenyItem, downloadMenuItem, cancelMenuItem });
            listView1.ContextMenuStrip = ContextMenuStrip;
            copyMenuItem.Click += copyMenuItem_Click;
            pasteMenuItem.Click += pasteMenuItem_Click;
            deleteMenuItem.Click += deleteMenuItem_Click;
            renameMenuItem.Click += renameMenuItem_Click;
            archiveMenyItem.Click += archiveMenyItem_Click;
            downloadMenuItem.Click += downloadMenuItem_Click;
            cancelMenuItem.Click += cancelMenuItem_Click;
        }

        private void Disks() //вывод списка дисков
        {
            textBox1.Text = "";
            listView1.Items.Clear();
            listView1.Columns.Clear();
            DriveInfo[] drives = DriveInfo.GetDrives();
            listView1.Columns.Add("Имя", 150, HorizontalAlignment.Left);
            
            foreach (DriveInfo drive in drives)
            {
                try
                {
                    listView1.Items.Add(drive.Name);
                }
                catch (Exception)
                {
                    MessageBox.Show("Устройство не готово");
                }
            }
            //listView1.Columns.Add("Объём", 100, HorizontalAlignment.Left);
            //foreach (DriveInfo drive in drives)
            //{
            //    listView1.Items.Add((drive.AvailableFreeSpace / 1073741824).ToString() + " Гб");
            //}
        }

        private void GetPath() //
        {
            try
            {
                textBox1.Text += listView1.SelectedItems[0].Text + (textBox1.Text.Length + listView1.SelectedItems[0].Text.Length > 3 ? '\\' : "");
                Fill();
            }
            catch { }
        }

        private void listView1_DoubleClick_1(object sender, EventArgs e) //нажатие на элемент listview
        {
            if (bookflag)
            {
                var link = listView1.SelectedItems[0].Tag;
               // Process.Start(link);
            }
            else
            {
                if (Path.GetExtension(Path.Combine(textBox1.Text, listView1.SelectedItems[0].Text)) == "") GetPath();
                else
                {
                    var p = new Process();
                    p.StartInfo = new ProcessStartInfo(Path.Combine(textBox1.Text, listView1.SelectedItems[0].Text))
                    {
                        UseShellExecute = true
                    };
                    p.Start();
                }
            }
        }

        private void toDisks_Click(object sender, EventArgs e) //Возвращает к списку дисков. 
        {
            Disks();
        }

        private void listView1_MouseUp(object sender, MouseEventArgs e) //Открывает меню ContextMenuStrip
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip.Show(MousePosition, ToolStripDropDownDirection.Right);
            }
        }
        void deleteMenuItem_Click(object sender, EventArgs e) //удаляет объект
        {
            var path = Path.Combine(textBox1.Text, listView1.SelectedItems[0].Text);
            ListViewItem _path = new ListViewItem(path);
            listView1.Items.Remove(_path);
            if (File.Exists(path)) File.Delete(path);
            if (Directory.Exists(path)) Directory.Delete(path);
            Fill();
        }
        void Fill() //метод для заполнения listview
        {
            DirectoryInfo dir = new DirectoryInfo(textBox1.Text);
            DirectoryInfo[] dirs = dir.GetDirectories();
            listView1.Items.Clear();
            foreach (DirectoryInfo Dir in dirs) listView1.Items.Add(Dir.Name);
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files) listView1.Items.Add(file.Name);
        }
        void renameMenuItem_Click(object sender, EventArgs e) //переименование 
        {
            string newName = Microsoft.VisualBasic.Interaction.InputBox("Новое имя файла: ");
            string sourceFile = textBox1.Text + listView1.SelectedItems[0].Text;
            string extension = Path.GetExtension(listView1.SelectedItems[0].Text);
            if (extension != "")
            {
                System.IO.FileInfo fi = new System.IO.FileInfo(sourceFile);
                if (fi.Exists)
                {
                    fi.MoveTo(textBox1.Text + newName + extension);
                }
            }
            else
            {
                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(sourceFile);
                if (di.Exists)
                {
                    di.MoveTo(textBox1.Text + newName);
                }
            }
            Fill();
        }

        void copyMenuItem_Click(object sender, EventArgs e) //копирование 
        {
            item = listView1.SelectedItems[0];
            itempath = textBox1.Text + listView1.SelectedItems[0].Text;
        }

        public void Compress(string sourceFile, string compressedFile) //метод для архивации
        {
            using (FileStream sourceStream = new FileStream(sourceFile, FileMode.OpenOrCreate))
            {
                using (FileStream targetStream = File.Create(compressedFile))
                {
                    using (GZipStream compressionStream = new GZipStream(targetStream, CompressionMode.Compress))
                    {
                        sourceStream.CopyTo(compressionStream);
                    }
                }
            }
        }
        void archiveMenyItem_Click(object sender, EventArgs e) //архивация 
        {
            var path = textBox1.Text + listView1.SelectedItems[0].Text;
            var item = listView1.SelectedItems[0].Text;
            if (File.Exists(path))
            {
                Compress(textBox1.Text + item, textBox1.Text + item.Substring(0, item.LastIndexOf('.')) + ".gz");
                ListViewItem afile = new ListViewItem(item.Substring(0, item.LastIndexOf('.')) + ".gz");
                afile.Name = afile.Text;
                listView1.Items.Add(afile);
            }
            else if (Directory.Exists(path))
            {
                ZipFile.CreateFromDirectory(textBox1.Text + item, textBox1.Text + item + ".zip");
                ListViewItem adir = new ListViewItem(item + ".zip");
                adir.Name = adir.Text;
                listView1.Items.Add(adir);
            }
        }
        async void pasteMenuItem_Click(object sender, EventArgs e) //вставка элемента 
        {
            var path = textBox1.Text;
            if (File.Exists(itempath))
            {
                using (FileStream SourceStream = File.Open(itempath, FileMode.Open))
                {
                    using (FileStream DestinationStream = File.Create(path + "\\" + item.Text.Substring(item.Text.LastIndexOf('\\') + 1)))
                    {
                        await SourceStream.CopyToAsync(DestinationStream);
                    }
                }
            }
            else if (Directory.Exists(itempath))
            {
                // Directory.Move(item.Text, path);
                new DirectoryInfo(itempath).MoveTo(path);

            }
            Fill();
        }
        public void SetPrefs() //Восстановление настроек
        {
            flag = prefs.flag;
            listView1.BackColor = prefs.color;
            listView1.Font = prefs.font;
            password = (String)prefs.password;
            login = prefs.login;
        }
        private void changeColour_Click(object sender, EventArgs e) //Изменение цвета фона listview
        {
            colorDialog1.Color = listView1.BackColor;
            if (colorDialog1.ShowDialog() == DialogResult.Cancel) return;
            listView1.BackColor = colorDialog1.Color;
        }

        private void getBack_Click(object sender, EventArgs e) //выход из каталога
        {
            try
            {
                if (textBox1.Text[textBox1.Text.Length - 1] == '\\')
                {
                    textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1, 1);
                    while (textBox1.Text[textBox1.Text.Length - 1] != '\\')
                    {
                        textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1, 1);
                    }
                }
                else if (textBox1.Text[textBox1.Text.Length - 1] != '\\')
                {
                    while (textBox1.Text[textBox1.Text.Length - 1] != '\\')
                    {
                        textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1, 1);
                    }
                }
                Fill();
            }
            catch (Exception)
            {
                Disks();
            }
        }

        private void changeFont_Click(object sender, EventArgs e) //изменение шрифта listview
        {
            fontDialog1.Font = listView1.Font;
            if (fontDialog1.ShowDialog() == DialogResult.Cancel) return;
            listView1.Font = fontDialog1.Font;
        }
        private void SavePrefs_Click(object sender, EventArgs e)//Сохранение настроек
        {
            prefs.color = listView1.BackColor;
            prefs.font = listView1.Font;
            prefs.password = password;
            prefs.login = login;
            prefs.flag = flag;
            prefs.SavePrefs();
        }
        public void Check(bool Flag) //проверка логина и пароля
        {
            if (Flag)
            {
                string possiblelog = Microsoft.VisualBasic.Interaction.InputBox("Ваш логин");
                while (possiblelog != login)
                {
                    possiblelog = Microsoft.VisualBasic.Interaction.InputBox("Неверный логин. Повторите попытку");
                }
                string possiblepas = Microsoft.VisualBasic.Interaction.InputBox("Ваш пароль");
                while (possiblepas != password)
                {
                    possiblepas = Microsoft.VisualBasic.Interaction.InputBox("Неверный пароль. Повторите попытку");
                }
            }
        }

        private void Registry_Click(object sender, EventArgs e) //Установка логина и пароля
        {
            flag = true;
            login = Microsoft.VisualBasic.Interaction.InputBox("Ваш логин");
            password = Microsoft.VisualBasic.Interaction.InputBox("Ваш пароль");
        }
        private void Search(string path, string regex) //Параллельный поиск
        {
            try
            {
                if (File.Exists(path))
                {
                    if (Path.GetExtension(path) == ".txt" || Path.GetExtension(path) == ".docx")
                    {
                        using (StreamReader sr = new StreamReader(path))
                        {
                            Match[] matches = Regex.Matches(sr.ReadToEnd(), regex).Cast<Match>().ToArray();
                            this.BeginInvoke((Action)delegate
                            {
                                comboBox1.Items.AddRange(matches);
                            }
                            );
                        }
                    }
                }
                else  if (Directory.Exists(path))
                {
                    string[] files = Directory.GetFiles(path);
                    string[] directories = Directory.GetDirectories(path);
                    Parallel.ForEach(files, current =>
                    {
                        Search(current, regex);
                    }
                    );

                    if (directories != null && directories.Length > 0)
                    {
                        Parallel.ForEach(directories, directory =>
                        {
                            Search(directory, regex);
                        }
                    );
                    }
                }
            }
            catch (Exception)
            {


            }
        }
    


        private void SearchBy_Click(object sender, EventArgs e) //Параллельный поиск
        {
            try
            {
                string path = textBox1.Text + listView1.SelectedItems[0].Text;
                string regex = textBox2.Text;
                comboBox1.Items.Clear();
                Search(path, regex);
            }
            catch (Exception)
            {
                MessageBox.Show("Выберите каталог для поиска");
            }
            
        }
        public static int DownloadFile(string remoteFilename, string localFilename) //Скачивание файла
        {
            int bytesProcessed = 0;
            Stream remoteStream = null;
            Stream localStream = null;
            WebResponse response = null;
            try
            {
                WebRequest request = WebRequest.Create(remoteFilename);
                if (request != null)
                {
                    response = request.GetResponse();
                    if (response != null)
                    {
                        remoteStream = response.GetResponseStream();
                        localStream = File.Create(localFilename);
                        byte[] buffer = new byte[1024];
                        int bytesRead;
                        do
                        {
                            bytesRead = remoteStream.Read(buffer, 0, buffer.Length);
                            localStream.Write(buffer, 0, bytesRead);
                            bytesProcessed += bytesRead;
                        } while (bytesRead > 0 && token.IsCancellationRequested);
                    }
                }
            }
            catch (Exception)
            {
                
            }
            finally
            {
                if (response != null) response.Close();
                if (remoteStream != null) remoteStream.Close();
                if (localStream != null) localStream.Close();
            }
            return bytesProcessed;
        }

        public void downloadMenuItem_Click(object sender, EventArgs e) //Скачивание файла
        {
            string currentfile = textBox1.Text + listView1.SelectedItems[0].Text;
            string remotefile = Microsoft.VisualBasic.Interaction.InputBox("Введите ссылку: ");
            
            DownloadFile(remotefile, currentfile);
        }

        public static void CancelDownload() //Отмена скачивания файла
        {
            CancellationTokenSource source = new CancellationTokenSource();
            token = source.Token;
            source.Cancel();
        }
        public void cancelMenuItem_Click(object sender, EventArgs e) //Отмена скачивания файла
        {
            CancelDownload();
        }

        public void DisplayBooks(string viewItem) //Отображение книжек
        {
            List<Book> Books = ShowBooks.Show(viewItem, 30);
            listView1.Items.Clear();
            listView1.Columns.Clear();
            listView1.Columns.Add("Название", 250);
            listView1.Columns.Add("Автор", 120);
            listView1.Columns.Add("Рейтинг", 70);
            listView1.Columns.Add("Цена", 50);
            if (Books != null)
            {
                foreach (var book in Books)
                    listView1.Items.Add(new ListViewItem(new[] { book.Name, book.Author, book.Rating, book.Price, book.Link }));
            }
        }

        private void Books_Click(object sender, EventArgs e) //Считывание языка программирования и вывод книжек
        {
            string item = Microsoft.VisualBasic.Interaction.InputBox("Введите название языка программирования");
            DisplayBooks(item);
        }
    }
}
