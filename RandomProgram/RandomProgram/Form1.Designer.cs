namespace RandomProgram
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_read_word = new System.Windows.Forms.Button();
            this.btn_write_word = new System.Windows.Forms.Button();
            this.progress_bar = new System.Windows.Forms.ProgressBar();
            this.lab_status = new System.Windows.Forms.Label();
            this.cb_all_size = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.text_write_space_count = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.cb_file_words = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cb_file_word_item = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.text_insert_count = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.text_edit_count = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.lab_file_word = new System.Windows.Forms.Label();
            this.lab_file_word_item = new System.Windows.Forms.Label();
            this.ch_format = new System.Windows.Forms.CheckBox();
            this.lab_graph = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.檔案ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_read_word
            // 
            this.btn_read_word.Location = new System.Drawing.Point(11, 39);
            this.btn_read_word.Margin = new System.Windows.Forms.Padding(2);
            this.btn_read_word.Name = "btn_read_word";
            this.btn_read_word.Size = new System.Drawing.Size(307, 30);
            this.btn_read_word.TabIndex = 0;
            this.btn_read_word.Text = "讀取Word";
            this.btn_read_word.UseVisualStyleBackColor = true;
            this.btn_read_word.Click += new System.EventHandler(this.btn_read_word_Click);
            // 
            // btn_write_word
            // 
            this.btn_write_word.Location = new System.Drawing.Point(12, 460);
            this.btn_write_word.Margin = new System.Windows.Forms.Padding(2);
            this.btn_write_word.Name = "btn_write_word";
            this.btn_write_word.Size = new System.Drawing.Size(306, 30);
            this.btn_write_word.TabIndex = 1;
            this.btn_write_word.Text = "產生題目";
            this.btn_write_word.UseVisualStyleBackColor = true;
            this.btn_write_word.Click += new System.EventHandler(this.btn_write_word_Click);
            // 
            // progress_bar
            // 
            this.progress_bar.Location = new System.Drawing.Point(337, 424);
            this.progress_bar.Name = "progress_bar";
            this.progress_bar.Size = new System.Drawing.Size(352, 30);
            this.progress_bar.Step = 100;
            this.progress_bar.TabIndex = 2;
            // 
            // lab_status
            // 
            this.lab_status.Location = new System.Drawing.Point(337, 472);
            this.lab_status.Name = "lab_status";
            this.lab_status.Size = new System.Drawing.Size(352, 18);
            this.lab_status.TabIndex = 3;
            this.lab_status.Text = "無";
            this.lab_status.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cb_all_size
            // 
            this.cb_all_size.FormattingEnabled = true;
            this.cb_all_size.Location = new System.Drawing.Point(57, 41);
            this.cb_all_size.Name = "cb_all_size";
            this.cb_all_size.Size = new System.Drawing.Size(78, 20);
            this.cb_all_size.TabIndex = 7;
            this.cb_all_size.Text = "B4";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "填空數";
            // 
            // text_write_space_count
            // 
            this.text_write_space_count.Location = new System.Drawing.Point(57, 13);
            this.text_write_space_count.Name = "text_write_space_count";
            this.text_write_space_count.Size = new System.Drawing.Size(78, 22);
            this.text_write_space_count.TabIndex = 11;
            this.text_write_space_count.Text = "1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "尺寸";
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(337, 39);
            this.listView1.Margin = new System.Windows.Forms.Padding(2);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(352, 375);
            this.listView1.TabIndex = 13;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // cb_file_words
            // 
            this.cb_file_words.FormattingEnabled = true;
            this.cb_file_words.Items.AddRange(new object[] {
            "全部"});
            this.cb_file_words.Location = new System.Drawing.Point(42, 12);
            this.cb_file_words.Name = "cb_file_words";
            this.cb_file_words.Size = new System.Drawing.Size(251, 20);
            this.cb_file_words.TabIndex = 14;
            this.cb_file_words.SelectedIndexChanged += new System.EventHandler(this.cb_file_words_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 15;
            this.label4.Text = "檔名";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 39);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 16;
            this.label5.Text = "項目";
            // 
            // cb_file_word_item
            // 
            this.cb_file_word_item.FormattingEnabled = true;
            this.cb_file_word_item.Location = new System.Drawing.Point(42, 37);
            this.cb_file_word_item.Name = "cb_file_word_item";
            this.cb_file_word_item.Size = new System.Drawing.Size(251, 20);
            this.cb_file_word_item.TabIndex = 17;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 63);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 19;
            this.label6.Text = "題數";
            // 
            // text_insert_count
            // 
            this.text_insert_count.Location = new System.Drawing.Point(41, 61);
            this.text_insert_count.Margin = new System.Windows.Forms.Padding(2);
            this.text_insert_count.Name = "text_insert_count";
            this.text_insert_count.Size = new System.Drawing.Size(252, 22);
            this.text_insert_count.TabIndex = 20;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(211, 87);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(82, 30);
            this.button1.TabIndex = 21;
            this.button1.Text = "新增";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(11, 83);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(82, 30);
            this.button2.TabIndex = 28;
            this.button2.Text = "刪除";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // text_edit_count
            // 
            this.text_edit_count.Location = new System.Drawing.Point(42, 57);
            this.text_edit_count.Margin = new System.Windows.Forms.Padding(2);
            this.text_edit_count.Name = "text_edit_count";
            this.text_edit_count.Size = new System.Drawing.Size(251, 22);
            this.text_edit_count.TabIndex = 27;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 59);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 26;
            this.label7.Text = "題數";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 35);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 24;
            this.label8.Text = "項目";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 11);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 23;
            this.label9.Text = "檔名";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(211, 83);
            this.button3.Margin = new System.Windows.Forms.Padding(2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(82, 30);
            this.button3.TabIndex = 29;
            this.button3.Text = "修改";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // lab_file_word
            // 
            this.lab_file_word.AutoSize = true;
            this.lab_file_word.Location = new System.Drawing.Point(43, 11);
            this.lab_file_word.Name = "lab_file_word";
            this.lab_file_word.Size = new System.Drawing.Size(17, 12);
            this.lab_file_word.TabIndex = 30;
            this.lab_file_word.Text = "無";
            // 
            // lab_file_word_item
            // 
            this.lab_file_word_item.AutoSize = true;
            this.lab_file_word_item.Location = new System.Drawing.Point(43, 35);
            this.lab_file_word_item.Name = "lab_file_word_item";
            this.lab_file_word_item.Size = new System.Drawing.Size(17, 12);
            this.lab_file_word_item.TabIndex = 31;
            this.lab_file_word_item.Text = "無";
            // 
            // ch_format
            // 
            this.ch_format.AutoSize = true;
            this.ch_format.Checked = true;
            this.ch_format.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ch_format.Location = new System.Drawing.Point(57, 66);
            this.ch_format.Margin = new System.Windows.Forms.Padding(2);
            this.ch_format.Name = "ch_format";
            this.ch_format.Size = new System.Drawing.Size(84, 16);
            this.ch_format.TabIndex = 32;
            this.ch_format.Text = "自動格式化";
            this.ch_format.UseVisualStyleBackColor = true;
            // 
            // lab_graph
            // 
            this.lab_graph.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lab_graph.Location = new System.Drawing.Point(357, 281);
            this.lab_graph.Name = "lab_graph";
            this.lab_graph.Size = new System.Drawing.Size(295, 18);
            this.lab_graph.TabIndex = 33;
            this.lab_graph.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lab_graph.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.cb_file_words);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.cb_file_word_item);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.text_insert_count);
            this.panel1.Location = new System.Drawing.Point(12, 77);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(306, 124);
            this.panel1.TabIndex = 34;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(144, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 35;
            this.label1.Text = "新增";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.text_edit_count);
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.lab_file_word_item);
            this.panel2.Controls.Add(this.button3);
            this.panel2.Controls.Add(this.lab_file_word);
            this.panel2.Location = new System.Drawing.Point(12, 219);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(306, 124);
            this.panel2.TabIndex = 35;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(144, 212);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 12);
            this.label10.TabIndex = 36;
            this.label10.Text = "修改";
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.text_write_space_count);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.ch_format);
            this.panel3.Controls.Add(this.cb_all_size);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Location = new System.Drawing.Point(12, 359);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(306, 96);
            this.panel3.TabIndex = 37;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(144, 353);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 12);
            this.label11.TabIndex = 38;
            this.label11.Text = "設定";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.檔案ToolStripMenuItem});
            this.menuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(701, 24);
            this.menuStrip1.TabIndex = 39;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 檔案ToolStripMenuItem
            // 
            this.檔案ToolStripMenuItem.Name = "檔案ToolStripMenuItem";
            this.檔案ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.檔案ToolStripMenuItem.Text = "預留";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(701, 500);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lab_graph);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.lab_status);
            this.Controls.Add(this.progress_bar);
            this.Controls.Add(this.btn_write_word);
            this.Controls.Add(this.btn_read_word);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "RandomProgram";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_read_word;
        private System.Windows.Forms.Button btn_write_word;
        private System.Windows.Forms.ProgressBar progress_bar;
        private System.Windows.Forms.Label lab_status;
        private System.Windows.Forms.ComboBox cb_all_size;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox text_write_space_count;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ComboBox cb_file_words;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cb_file_word_item;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox text_insert_count;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox text_edit_count;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label lab_file_word;
        private System.Windows.Forms.Label lab_file_word_item;
        private System.Windows.Forms.CheckBox ch_format;
        private System.Windows.Forms.Label lab_graph;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 檔案ToolStripMenuItem;
    }
}

