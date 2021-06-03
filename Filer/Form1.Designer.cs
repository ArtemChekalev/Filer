
namespace Filer
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.getBack = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toDisks = new System.Windows.Forms.Button();
            this.changeColour = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.changeFont = new System.Windows.Forms.Button();
            this.SavePrefs = new System.Windows.Forms.Button();
            this.Registry = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.SearchBy = new System.Windows.Forms.Button();
            this.Books = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(26, 29);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(218, 23);
            this.textBox1.TabIndex = 1;
            // 
            // getBack
            // 
            this.getBack.Location = new System.Drawing.Point(26, 104);
            this.getBack.Name = "getBack";
            this.getBack.Size = new System.Drawing.Size(34, 23);
            this.getBack.TabIndex = 2;
            this.getBack.Text = "<-";
            this.getBack.UseVisualStyleBackColor = true;
            this.getBack.Click += new System.EventHandler(this.getBack_Click);
            // 
            // listView1
            // 
            this.listView1.ContextMenuStrip = this.ContextMenuStrip;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(26, 133);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(650, 226);
            this.listView1.TabIndex = 3;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick_1);
            this.listView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseUp);
            // 
            // ContextMenuStrip
            // 
            this.ContextMenuStrip.Name = "Копировать";
            this.ContextMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.ContextMenuStrip.ShowCheckMargin = true;
            this.ContextMenuStrip.Size = new System.Drawing.Size(83, 4);
            // 
            // toDisks
            // 
            this.toDisks.Location = new System.Drawing.Point(273, 58);
            this.toDisks.Name = "toDisks";
            this.toDisks.Size = new System.Drawing.Size(75, 41);
            this.toDisks.TabIndex = 4;
            this.toDisks.Text = "Перейти к дискам";
            this.toDisks.UseVisualStyleBackColor = true;
            this.toDisks.Click += new System.EventHandler(this.toDisks_Click);
            // 
            // changeColour
            // 
            this.changeColour.Location = new System.Drawing.Point(192, 58);
            this.changeColour.Name = "changeColour";
            this.changeColour.Size = new System.Drawing.Size(75, 41);
            this.changeColour.TabIndex = 5;
            this.changeColour.Text = "Изменить цвет";
            this.changeColour.UseVisualStyleBackColor = true;
            this.changeColour.Click += new System.EventHandler(this.changeColour_Click);
            // 
            // changeFont
            // 
            this.changeFont.Location = new System.Drawing.Point(111, 58);
            this.changeFont.Name = "changeFont";
            this.changeFont.Size = new System.Drawing.Size(75, 41);
            this.changeFont.TabIndex = 6;
            this.changeFont.Text = "Изменить шрифт";
            this.changeFont.UseVisualStyleBackColor = true;
            this.changeFont.Click += new System.EventHandler(this.changeFont_Click);
            // 
            // SavePrefs
            // 
            this.SavePrefs.Location = new System.Drawing.Point(66, 104);
            this.SavePrefs.Name = "SavePrefs";
            this.SavePrefs.Size = new System.Drawing.Size(75, 23);
            this.SavePrefs.TabIndex = 7;
            this.SavePrefs.Text = "Сохранить";
            this.SavePrefs.UseVisualStyleBackColor = true;
            this.SavePrefs.Click += new System.EventHandler(this.SavePrefs_Click);
            // 
            // Registry
            // 
            this.Registry.Location = new System.Drawing.Point(26, 58);
            this.Registry.Name = "Registry";
            this.Registry.Size = new System.Drawing.Size(79, 40);
            this.Registry.TabIndex = 8;
            this.Registry.Text = "Установить пароль";
            this.Registry.UseVisualStyleBackColor = true;
            this.Registry.Click += new System.EventHandler(this.Registry_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(576, 75);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 23);
            this.textBox2.TabIndex = 9;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(441, 75);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 23);
            this.comboBox1.TabIndex = 10;
            // 
            // SearchBy
            // 
            this.SearchBy.Location = new System.Drawing.Point(576, 104);
            this.SearchBy.Name = "SearchBy";
            this.SearchBy.Size = new System.Drawing.Size(100, 23);
            this.SearchBy.TabIndex = 11;
            this.SearchBy.Text = "Поиск";
            this.SearchBy.UseVisualStyleBackColor = true;
            this.SearchBy.Click += new System.EventHandler(this.SearchBy_Click);
            // 
            // Books
            // 
            this.Books.Location = new System.Drawing.Point(354, 58);
            this.Books.Name = "Books";
            this.Books.Size = new System.Drawing.Size(75, 41);
            this.Books.TabIndex = 12;
            this.Books.Text = "Книги";
            this.Books.UseVisualStyleBackColor = true;
            this.Books.Click += new System.EventHandler(this.Books_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(699, 409);
            this.Controls.Add(this.Books);
            this.Controls.Add(this.SearchBy);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.Registry);
            this.Controls.Add(this.SavePrefs);
            this.Controls.Add(this.changeFont);
            this.Controls.Add(this.changeColour);
            this.Controls.Add(this.toDisks);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.getBack);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button getBack;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button toDisks;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem Вставить;
        private System.Windows.Forms.ToolStripMenuItem Удалить;
        private System.Windows.Forms.ContextMenuStrip ContextMenuStrip;
        private System.Windows.Forms.Button changeColour;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.Button changeFont;
        private System.Windows.Forms.Button SavePrefs;
        private System.Windows.Forms.Button Registry;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button SearchBy;
        private System.Windows.Forms.Button Books;
    }
}

