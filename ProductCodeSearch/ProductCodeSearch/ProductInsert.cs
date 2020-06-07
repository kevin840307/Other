using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProductCodeSearch
{
    public partial class ProductInsert : Form
    {
        private ProductData g_prodData = null;

        public ProductInsert()
        {
            InitializeComponent();
        }

        public ProductInsert(ref ProductData prodData)
        {
            InitializeComponent();
            prodData = new ProductData();
            g_prodData = prodData;
        }

        private void btn_select_data_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFile = new OpenFileDialog();
                openFile.Filter = "圖片(jpeg, gif, bmp, png) | *.jpg;*.jpeg;*.png;*.bmp|" + "所有檔案 |*.*";
                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    Bitmap btPicture = new Bitmap(openFile.FileName);
                    int iStart = openFile.FileName.LastIndexOf("\\") + 1;
                    int iSize = openFile.FileName.Length - iStart;
                    //g_sFileName = openFile.FileName.Substring(iStart, iSize);
                    picboxShow.Width = btPicture.Width;
                    picboxShow.Height = btPicture.Height;
                    picboxShow.Image = btPicture;
                    picboxShow.Location = new Point(((this.Width - btPicture.Width) / 2), btn_cancel.Location.Y + 50);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void btn_confirm_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("確認要新增嗎?", "新增!!", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                if (fnCheckData())
                {
                    string sDataName = text_filename.Text;
                    Functions.fnSaveImage(picboxShow.Image, sDataName);
                    g_prodData.fnLoadData(sDataName);
                    g_prodData.FileImagePath = ProductClass.ImagePath + sDataName + ".png";

                    if (fnInsertProduct(ref g_prodData, text_code.Text, text_remarks.Text))
                    {
                        g_prodData.fnLoadData(sDataName);
                        DialogResult = DialogResult.OK;
                    }
                }
            }
        }

        private bool fnCheckData()
        {
            if (picboxShow.Image == null)
            {
                MessageBox.Show("請選擇圖片");
                return false;
            }
            else if (!Functions.fnMakeProductData(text_filename.Text))
            {
                MessageBox.Show("檔名重複");
                return false;
            }
            return true;
        }

        private bool fnInsertProduct(ref ProductData prodData, string sCode, string sRemarks)
        {
            StreamWriter swWrite = null;
            try
            {
                swWrite = new StreamWriter(prodData.FilePath, false, Encoding.Default);
                swWrite.WriteLine(sCode);
                swWrite.WriteLine(sRemarks);
            }
            catch
            {
                MessageBox.Show("Insert新增錯誤");
                return false;
            }
            finally
            {
                swWrite.Close();
            }
            return true;
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ProductInsert_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)27: // ESC
                    this.Close();
                    break;
            }
        }

        private void ProductInsert_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true)
            {
                switch (e.KeyCode)
                {
                    case Keys.F:
                        btn_select_data_Click(sender, e);
                        break;
                    case Keys.S:
                        btn_confirm_Click(sender, e);
                        break;
                }
            }
        }


    }
}
