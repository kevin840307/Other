namespace ProductCodeSearch
{
    partial class ProductInsert
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.text_filename = new System.Windows.Forms.TextBox();
            this.text_code = new System.Windows.Forms.TextBox();
            this.text_remarks = new System.Windows.Forms.TextBox();
            this.picboxShow = new System.Windows.Forms.PictureBox();
            this.btn_select_data = new System.Windows.Forms.Button();
            this.btn_confirm = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picboxShow)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("PMingLiU", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(7, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "國際條碼：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("PMingLiU", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(7, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "備註：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("PMingLiU", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(7, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 25);
            this.label3.TabIndex = 3;
            this.label3.Text = "檔名：";
            // 
            // text_filename
            // 
            this.text_filename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.text_filename.Font = new System.Drawing.Font("PMingLiU", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.text_filename.Location = new System.Drawing.Point(155, 9);
            this.text_filename.Name = "text_filename";
            this.text_filename.Size = new System.Drawing.Size(597, 31);
            this.text_filename.TabIndex = 4;
            // 
            // text_code
            // 
            this.text_code.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.text_code.Font = new System.Drawing.Font("PMingLiU", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.text_code.Location = new System.Drawing.Point(155, 46);
            this.text_code.Name = "text_code";
            this.text_code.Size = new System.Drawing.Size(597, 31);
            this.text_code.TabIndex = 5;
            // 
            // text_remarks
            // 
            this.text_remarks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.text_remarks.Font = new System.Drawing.Font("PMingLiU", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.text_remarks.Location = new System.Drawing.Point(155, 83);
            this.text_remarks.Multiline = true;
            this.text_remarks.Name = "text_remarks";
            this.text_remarks.Size = new System.Drawing.Size(597, 130);
            this.text_remarks.TabIndex = 6;
            // 
            // picboxShow
            // 
            this.picboxShow.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.picboxShow.Location = new System.Drawing.Point(84, 395);
            this.picboxShow.Name = "picboxShow";
            this.picboxShow.Size = new System.Drawing.Size(597, 181);
            this.picboxShow.TabIndex = 7;
            this.picboxShow.TabStop = false;
            // 
            // btn_select_data
            // 
            this.btn_select_data.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_select_data.Location = new System.Drawing.Point(12, 229);
            this.btn_select_data.Name = "btn_select_data";
            this.btn_select_data.Size = new System.Drawing.Size(740, 45);
            this.btn_select_data.TabIndex = 8;
            this.btn_select_data.Text = "選擇檔案(Ctrl + F)";
            this.btn_select_data.UseVisualStyleBackColor = true;
            this.btn_select_data.Click += new System.EventHandler(this.btn_select_data_Click);
            // 
            // btn_confirm
            // 
            this.btn_confirm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_confirm.Location = new System.Drawing.Point(12, 280);
            this.btn_confirm.Name = "btn_confirm";
            this.btn_confirm.Size = new System.Drawing.Size(740, 45);
            this.btn_confirm.TabIndex = 9;
            this.btn_confirm.Text = "新增(Ctrl + S)";
            this.btn_confirm.UseVisualStyleBackColor = true;
            this.btn_confirm.Click += new System.EventHandler(this.btn_confirm_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_cancel.Location = new System.Drawing.Point(12, 331);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(740, 45);
            this.btn_cancel.TabIndex = 10;
            this.btn_cancel.Text = "取消(ESC)";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // ProductInsert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(764, 616);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_confirm);
            this.Controls.Add(this.btn_select_data);
            this.Controls.Add(this.picboxShow);
            this.Controls.Add(this.text_remarks);
            this.Controls.Add(this.text_code);
            this.Controls.Add(this.text_filename);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.KeyPreview = true;
            this.Name = "ProductInsert";
            this.Text = "ProductInsert";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ProductInsert_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ProductInsert_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.picboxShow)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox text_filename;
        private System.Windows.Forms.TextBox text_code;
        private System.Windows.Forms.TextBox text_remarks;
        private System.Windows.Forms.PictureBox picboxShow;
        private System.Windows.Forms.Button btn_select_data;
        private System.Windows.Forms.Button btn_confirm;
        private System.Windows.Forms.Button btn_cancel;

    }
}