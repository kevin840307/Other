namespace ProductCodeSearch
{
    partial class ProductEdit
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.text_code = new System.Windows.Forms.TextBox();
            this.text_remarks = new System.Windows.Forms.TextBox();
            this.btn_update = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.picboxShow = new System.Windows.Forms.PictureBox();
            this.btn_pic_big = new System.Windows.Forms.Button();
            this.btn_pic_small = new System.Windows.Forms.Button();
            this.btn_edit = new System.Windows.Forms.Button();
            this.text_filename = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_file = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picboxShow)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("PMingLiU", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(12, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "國際條碼：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("PMingLiU", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(12, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "備註：";
            // 
            // text_code
            // 
            this.text_code.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.text_code.BackColor = System.Drawing.SystemColors.Info;
            this.text_code.Enabled = false;
            this.text_code.Font = new System.Drawing.Font("PMingLiU", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.text_code.Location = new System.Drawing.Point(146, 49);
            this.text_code.Name = "text_code";
            this.text_code.Size = new System.Drawing.Size(651, 31);
            this.text_code.TabIndex = 2;
            // 
            // text_remarks
            // 
            this.text_remarks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.text_remarks.BackColor = System.Drawing.SystemColors.Info;
            this.text_remarks.Enabled = false;
            this.text_remarks.Font = new System.Drawing.Font("PMingLiU", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.text_remarks.Location = new System.Drawing.Point(146, 89);
            this.text_remarks.Multiline = true;
            this.text_remarks.Name = "text_remarks";
            this.text_remarks.Size = new System.Drawing.Size(651, 130);
            this.text_remarks.TabIndex = 3;
            // 
            // btn_update
            // 
            this.btn_update.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_update.Location = new System.Drawing.Point(17, 281);
            this.btn_update.Name = "btn_update";
            this.btn_update.Size = new System.Drawing.Size(780, 42);
            this.btn_update.TabIndex = 4;
            this.btn_update.Text = "更新(Ctrl + S)";
            this.btn_update.UseVisualStyleBackColor = true;
            this.btn_update.Click += new System.EventHandler(this.btn_Update_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_cancel.Location = new System.Drawing.Point(18, 329);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(780, 42);
            this.btn_cancel.TabIndex = 5;
            this.btn_cancel.Text = "取消(ESC)";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // picboxShow
            // 
            this.picboxShow.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.picboxShow.Location = new System.Drawing.Point(132, 422);
            this.picboxShow.Name = "picboxShow";
            this.picboxShow.Size = new System.Drawing.Size(546, 137);
            this.picboxShow.TabIndex = 6;
            this.picboxShow.TabStop = false;
            this.picboxShow.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picboxShow_MouseDown);
            this.picboxShow.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picboxShow_MouseMove);
            this.picboxShow.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picboxShow_MouseUp);
            this.picboxShow.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.picboxShow_MouseWheel);
            // 
            // btn_pic_big
            // 
            this.btn_pic_big.Location = new System.Drawing.Point(17, 379);
            this.btn_pic_big.Name = "btn_pic_big";
            this.btn_pic_big.Size = new System.Drawing.Size(57, 29);
            this.btn_pic_big.TabIndex = 7;
            this.btn_pic_big.Text = "放大+";
            this.btn_pic_big.UseVisualStyleBackColor = true;
            this.btn_pic_big.Click += new System.EventHandler(this.btn_pic_big_Click);
            // 
            // btn_pic_small
            // 
            this.btn_pic_small.Location = new System.Drawing.Point(80, 379);
            this.btn_pic_small.Name = "btn_pic_small";
            this.btn_pic_small.Size = new System.Drawing.Size(57, 29);
            this.btn_pic_small.TabIndex = 8;
            this.btn_pic_small.Text = "原圖";
            this.btn_pic_small.UseVisualStyleBackColor = true;
            this.btn_pic_small.Click += new System.EventHandler(this.btn_pic_small_Click);
            // 
            // btn_edit
            // 
            this.btn_edit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_edit.Location = new System.Drawing.Point(18, 233);
            this.btn_edit.Name = "btn_edit";
            this.btn_edit.Size = new System.Drawing.Size(780, 42);
            this.btn_edit.TabIndex = 9;
            this.btn_edit.Text = "編輯(Ctrl + E)";
            this.btn_edit.UseVisualStyleBackColor = true;
            this.btn_edit.Click += new System.EventHandler(this.btn_edit_Click);
            // 
            // text_filename
            // 
            this.text_filename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.text_filename.BackColor = System.Drawing.SystemColors.Info;
            this.text_filename.Enabled = false;
            this.text_filename.Font = new System.Drawing.Font("PMingLiU", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.text_filename.Location = new System.Drawing.Point(147, 12);
            this.text_filename.Name = "text_filename";
            this.text_filename.Size = new System.Drawing.Size(651, 31);
            this.text_filename.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("PMingLiU", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(13, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(137, 25);
            this.label3.TabIndex = 10;
            this.label3.Text = "檔案名稱：";
            // 
            // btn_file
            // 
            this.btn_file.Enabled = false;
            this.btn_file.Location = new System.Drawing.Point(143, 379);
            this.btn_file.Name = "btn_file";
            this.btn_file.Size = new System.Drawing.Size(57, 29);
            this.btn_file.TabIndex = 12;
            this.btn_file.Text = "更換";
            this.btn_file.UseVisualStyleBackColor = true;
            this.btn_file.Click += new System.EventHandler(this.btn_file_Click);
            // 
            // ProductEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(810, 668);
            this.Controls.Add(this.btn_file);
            this.Controls.Add(this.text_filename);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btn_edit);
            this.Controls.Add(this.btn_pic_small);
            this.Controls.Add(this.btn_pic_big);
            this.Controls.Add(this.picboxShow);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_update);
            this.Controls.Add(this.text_remarks);
            this.Controls.Add(this.text_code);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.KeyPreview = true;
            this.Name = "ProductEdit";
            this.Text = "ProductEdit";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ProductEdit_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ProductEdit_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.picboxShow)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox text_code;
        private System.Windows.Forms.TextBox text_remarks;
        private System.Windows.Forms.Button btn_update;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.PictureBox picboxShow;
        private System.Windows.Forms.Button btn_pic_big;
        private System.Windows.Forms.Button btn_pic_small;
        private System.Windows.Forms.Button btn_edit;
        private System.Windows.Forms.TextBox text_filename;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_file;
    }
}