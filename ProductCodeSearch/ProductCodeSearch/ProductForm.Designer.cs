namespace ProductCodeSearch
{
    partial class ProductForm
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
            this.components = new System.ComponentModel.Container();
            this.lvProduct = new System.Windows.Forms.ListView();
            this.text_file_name = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.text_code = new System.Windows.Forms.TextBox();
            this.btn_search = new System.Windows.Forms.Button();
            this.menu_action = new System.Windows.Forms.MenuStrip();
            this.檔案ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_open_insert = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_open_edit = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_delets = new System.Windows.Forms.ToolStripMenuItem();
            this.cmenu_item_action = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menu_delete = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_action.SuspendLayout();
            this.cmenu_item_action.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvProduct
            // 
            this.lvProduct.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvProduct.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvProduct.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lvProduct.Font = new System.Drawing.Font("PMingLiU", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lvProduct.Location = new System.Drawing.Point(16, 182);
            this.lvProduct.Name = "lvProduct";
            this.lvProduct.ShowItemToolTips = true;
            this.lvProduct.Size = new System.Drawing.Size(850, 456);
            this.lvProduct.TabIndex = 0;
            this.lvProduct.TileSize = new System.Drawing.Size(20, 20);
            this.lvProduct.UseCompatibleStateImageBehavior = false;
            this.lvProduct.DoubleClick += new System.EventHandler(this.lvProduct_DoubleClick);
            this.lvProduct.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvProduct_MouseClick);
            // 
            // text_file_name
            // 
            this.text_file_name.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.text_file_name.Font = new System.Drawing.Font("PMingLiU", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.text_file_name.Location = new System.Drawing.Point(131, 36);
            this.text_file_name.Name = "text_file_name";
            this.text_file_name.Size = new System.Drawing.Size(735, 31);
            this.text_file_name.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("PMingLiU", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(12, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "檔案名稱：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("PMingLiU", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(12, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "國際條碼：";
            // 
            // text_code
            // 
            this.text_code.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.text_code.Font = new System.Drawing.Font("PMingLiU", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.text_code.Location = new System.Drawing.Point(131, 77);
            this.text_code.Name = "text_code";
            this.text_code.Size = new System.Drawing.Size(735, 31);
            this.text_code.TabIndex = 5;
            // 
            // btn_search
            // 
            this.btn_search.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_search.Location = new System.Drawing.Point(16, 114);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(850, 51);
            this.btn_search.TabIndex = 7;
            this.btn_search.Text = "查詢";
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // menu_action
            // 
            this.menu_action.BackColor = System.Drawing.SystemColors.Control;
            this.menu_action.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.menu_action.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menu_action.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.檔案ToolStripMenuItem});
            this.menu_action.Location = new System.Drawing.Point(0, 0);
            this.menu_action.Name = "menu_action";
            this.menu_action.Size = new System.Drawing.Size(878, 32);
            this.menu_action.TabIndex = 9;
            this.menu_action.Text = "menuStrip1";
            // 
            // 檔案ToolStripMenuItem
            // 
            this.檔案ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_open_insert,
            this.menu_open_edit,
            this.menu_delets});
            this.檔案ToolStripMenuItem.Name = "檔案ToolStripMenuItem";
            this.檔案ToolStripMenuItem.Size = new System.Drawing.Size(60, 28);
            this.檔案ToolStripMenuItem.Text = "檔案";
            // 
            // menu_open_insert
            // 
            this.menu_open_insert.Name = "menu_open_insert";
            this.menu_open_insert.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.menu_open_insert.Size = new System.Drawing.Size(198, 28);
            this.menu_open_insert.Text = "新增";
            this.menu_open_insert.Click += new System.EventHandler(this.menu_open_insert_Click);
            // 
            // menu_open_edit
            // 
            this.menu_open_edit.Name = "menu_open_edit";
            this.menu_open_edit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.menu_open_edit.Size = new System.Drawing.Size(198, 28);
            this.menu_open_edit.Text = "修改";
            this.menu_open_edit.Click += new System.EventHandler(this.menu_open_edit_Click);
            // 
            // menu_delets
            // 
            this.menu_delets.Name = "menu_delets";
            this.menu_delets.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.menu_delets.Size = new System.Drawing.Size(198, 28);
            this.menu_delets.Text = "刪除";
            this.menu_delets.Click += new System.EventHandler(this.menu_delets_Click);
            // 
            // cmenu_item_action
            // 
            this.cmenu_item_action.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmenu_item_action.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_delete});
            this.cmenu_item_action.Name = "cmenu_item_action";
            this.cmenu_item_action.Size = new System.Drawing.Size(171, 30);
            // 
            // menu_delete
            // 
            this.menu_delete.Name = "menu_delete";
            this.menu_delete.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.menu_delete.Size = new System.Drawing.Size(170, 26);
            this.menu_delete.Text = "刪除";
            this.menu_delete.Click += new System.EventHandler(this.menu_delete_Click);
            // 
            // ProductForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(878, 650);
            this.Controls.Add(this.btn_search);
            this.Controls.Add(this.text_code);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.text_file_name);
            this.Controls.Add(this.lvProduct);
            this.Controls.Add(this.menu_action);
            this.MainMenuStrip = this.menu_action;
            this.Name = "ProductForm";
            this.Text = "ProductSearch";
            this.menu_action.ResumeLayout(false);
            this.menu_action.PerformLayout();
            this.cmenu_item_action.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvProduct;
        private System.Windows.Forms.TextBox text_file_name;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox text_code;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.MenuStrip menu_action;
        private System.Windows.Forms.ToolStripMenuItem 檔案ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menu_open_insert;
        private System.Windows.Forms.ToolStripMenuItem menu_open_edit;
        private System.Windows.Forms.ContextMenuStrip cmenu_item_action;
        private System.Windows.Forms.ToolStripMenuItem menu_delete;
        private System.Windows.Forms.ToolStripMenuItem menu_delets;
    }
}

