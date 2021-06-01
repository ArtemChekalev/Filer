using System;
using System.IO;
using System.Diagnostics;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.VisualBasic.FileIO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
            ContextMenuStrip.Items.AddRange(new[] { copyMenuItem, pasteMenuItem, deleteMenuItem, renameMenuItem, archiveMenyItem });
            listView1.ContextMenuStrip = ContextMenuStrip;
            copyMenuItem.Click += copyMenuItem_Click;
            pasteMenuItem.Click += pasteMenuItem_Click;
            deleteMenuItem.Click += deleteMenuItem_Click;
            renameMenuItem.Click += renameMenuItem_Click;
            archiveMenyItem.Click += archiveMenyItem_Click;
            
        }

        private void Disks()
        {
            textBox1.Text = "";
            listView1.Items.Clear();
            listView1.Columns.Clear();
            DriveInfo[] drives = DriveInfo.GetDrives();
            listView1.Columns.Add("Имя", 150, HorizontalAlignment.Left);
            listView1.Columns.Add("Объём", 100, HorizontalAlignment.Left);
            foreach (DriveInfo drive in drives)
            {
                try
                {
                    listView1.Items.Add(drive.Name);
                    listView1.Items.Add((drive.AvailableFreeSpace/1073741824).ToString() + " Гб");
                }
                catch (Exception)
                {
                    MessageBox.Show("Устройство не готово");
                }
            }
        }

        private void GetPath()
        {
            try
            {
                textBox1.Text += listView1.SelectedItems[0].Text + (textBox1.Text.Length + listView1.SelectedItems[0].Text.Length > 3 ? '\\' : "");
                Fill();
            }
            catch { }
        }

        private void listView1_DoubleClick_1(object sender, EventArgs e)
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

        private void toDisks_Click(object sender, EventArgs e)
        {
            Disks();
        }

        private void listView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip.Show(MousePosition, ToolStripDropDownDirection.Right);
            }
        }
        void deleteMenuItem_Click(object sender, EventArgs e)
        {
            var path = Path.Combine(textBox1.Text, listView1.SelectedItems[0].Text);
            ListViewItem _path = new ListViewItem(path);
            listView1.Items.Remove(_path);
            if (File.Exists(path)) File.Delete(path);
            if (Directory.Exists(path)) Directory.Delete(path);
            Fill();
        }
        void Fill()
        {
            DirectoryInfo dir = new DirectoryInfo(textBox1.Text);
            DirectoryInfo[] dirs = dir.GetDirectories();
            listView1.Items.Clear();
            foreach (DirectoryInfo Dir in dirs) listView1.Items.Add(Dir.Name);
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files) listView1.Items.Add(file.Name);
        }
        void renameMenuItem_Click(object sender, EventArgs e)
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

        void copyMenuItem_Click(object sender, EventArgs e)
        {
            item = listView1.SelectedItems[0];
            itempath = textBox1.Text + listView1.SelectedItems[0].Text;
        }

        public void Compress(string sourceFile, string compressedFile)
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
        void archiveMenyItem_Click(object sender, EventArgs e)
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
            else if(Directory.Exists(path))
            {
                ZipFile.CreateFromDirectory(textBox1.Text + item, textBox1.Text + item + ".zip");
                ListViewItem adir = new ListViewItem(item + ".zip");
                adir.Name = adir.Text;
                listView1.Items.Add(adir);
            }
        }
        async void pasteMenuItem_Click(object sender, EventArgs e)
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
        public void SetPrefs()
        {
            flag = prefs.flag;
            listView1.BackColor = prefs.color;
            listView1.Font = prefs.font;
            password = (String)prefs.password;
            login = prefs.login;
        }
        private void changeColour_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = listView1.BackColor;
            if (colorDialog1.ShowDialog() == DialogResult.Cancel) return;
            listView1.BackColor = colorDialog1.Color;
        }

        private void getBack_Click(object sender, EventArgs e)
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

        private void changeFont_Click(object sender, EventArgs e)
        {
            fontDialog1.Font = listView1.Font;
            if (fontDialog1.ShowDialog() == DialogResult.Cancel) return;
            listView1.Font = fontDialog1.Font;
        }

        private void SavePrefs_Click(object sender, EventArgs e)
        {
            prefs.color = listView1.BackColor;
            prefs.font = listView1.Font;
            prefs.password = password;
            prefs.login = login;
            prefs.flag = flag;
            prefs.SavePrefs();
        }
        public void Check(bool Flag)
        {
            if (Flag)
            {
                string possiblelog = Microsoft.VisualBasic.Interaction.InputBox("Ваш логин");
                while(possiblelog != login)
                {
                    possiblelog = Microsoft.VisualBasic.Interaction.InputBox("Неверный логин. Повторите попытку");
                }
                string possiblepas = Microsoft.VisualBasic.Interaction.InputBox("Ваш пароль");
                while(possiblepas != password)
                {
                    possiblepas = Microsoft.VisualBasic.Interaction.InputBox("Неверный пароль. Повторите попытку");
                }
            }
        }

        private void Registry_Click(object sender, EventArgs e)
        {
            flag = true;
            login = Microsoft.VisualBasic.Interaction.InputBox("Ваш логин");
            password = Microsoft.VisualBasic.Interaction.InputBox("Ваш пароль");
        }
    }
}
