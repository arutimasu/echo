namespace WinFormsApp1
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
            components = new System.ComponentModel.Container();
            panel1 = new Panel();
            textBox1 = new TextBox();
            label1 = new Label();
            username = new TextBox();
            button2 = new Button();
            field = new TextBox();
            button3 = new Button();
            label2 = new Label();
            toolTip1 = new ToolTip(components);
            getAddress = new Button();
            contextMenuStrip1 = new ContextMenuStrip(components);
            copyToolStripMenuItem = new ToolStripMenuItem();
            listView1 = new ListView();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.AutoScroll = true;
            panel1.Location = new Point(327, 42);
            panel1.Name = "panel1";
            panel1.Size = new Size(490, 376);
            panel1.TabIndex = 0;
            // 
            // textBox1
            // 
            textBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox1.Location = new Point(592, 13);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 23);
            textBox1.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 15);
            label1.Name = "label1";
            label1.Size = new Size(203, 15);
            label1.TabIndex = 3;
            label1.Text = "Ваше никнейм (имя пользователя):";
            // 
            // username
            // 
            username.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            username.AutoCompleteSource = AutoCompleteSource.CustomSource;
            username.Location = new Point(221, 12);
            username.Name = "username";
            username.Size = new Size(100, 23);
            username.TabIndex = 4;
            // 
            // button2
            // 
            button2.Location = new Point(327, 12);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 6;
            button2.Text = "Войти";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_ClickAsync;
            // 
            // field
            // 
            field.Location = new Point(327, 424);
            field.Multiline = true;
            field.Name = "field";
            field.Size = new Size(409, 50);
            field.TabIndex = 0;
            // 
            // button3
            // 
            button3.Location = new Point(742, 424);
            button3.Name = "button3";
            button3.Size = new Size(75, 50);
            button3.TabIndex = 7;
            button3.Text = "Отправить";
            button3.UseVisualStyleBackColor = true;
            button3.Click += sendButton_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(408, 16);
            label2.Name = "label2";
            label2.Size = new Size(181, 15);
            label2.TabIndex = 13;
            label2.Text = "Никнейм вашего собеседника: ";
            // 
            // getAddress
            // 
            getAddress.Location = new Point(698, 13);
            getAddress.Name = "getAddress";
            getAddress.Size = new Size(119, 23);
            getAddress.TabIndex = 18;
            getAddress.Text = "Получить адрес";
            getAddress.UseVisualStyleBackColor = true;
            getAddress.Click += getAddress_Click;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { copyToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(140, 26);
            // 
            // copyToolStripMenuItem
            // 
            copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            copyToolStripMenuItem.Size = new Size(139, 22);
            copyToolStripMenuItem.Text = "Копировать";
            // 
            // listView1
            // 
            listView1.Location = new Point(12, 41);
            listView1.Name = "listView1";
            listView1.Size = new Size(309, 433);
            listView1.TabIndex = 0;
            listView1.TileSize = new Size(228, 34);
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Tile;
            listView1.ItemActivate += listView1_ItemActivate;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(829, 486);
            Controls.Add(listView1);
            Controls.Add(getAddress);
            Controls.Add(label2);
            Controls.Add(button3);
            Controls.Add(field);
            Controls.Add(button2);
            Controls.Add(username);
            Controls.Add(label1);
            Controls.Add(textBox1);
            Controls.Add(panel1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private TextBox textBox1;
        private Label label1;
        private TextBox username;
        private Button button2;
        private TextBox field;
        private Button button3;
        private Label label2;
        private ToolTip toolTip1;
        private Button getAddress;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem copyToolStripMenuItem;
        private ListView listView1;
    }
}