using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductCodeSearch
{
    public partial class ProductEdit : Form
    {
        Point g_pPos = new Point();
        bool g_bImageChange = false;
        int g_iOffsetX = 0, g_iOffsetY = 0;
        int g_iX = 0, g_iY = 0;
        bool g_bMove = false;
        int iPicW = 0;

        private ProductData g_prodData = null;
        public ProductEdit()
        {
            InitializeComponent();
        }

        public ProductEdit(ProductData prodData)
        {
            InitializeComponent();

            text_code.Text = prodData.Code;
            text_remarks.Text = prodData.Remarks;
            text_filename.Text = prodData.FileName;
            this.Text = prodData.FileName;
            g_prodData = prodData;
            fnInitPic();
        }

        private void fnInitPic()
        {
            FileStream fsStream = new FileStream(g_prodData.FileImagePath, FileMode.Open);
            Bitmap btPicture = new Bitmap(fsStream);
            picboxShow.Width = btPicture.Width;
            picboxShow.Height = btPicture.Height;
            picboxShow.Image = btPicture;
            g_pPos = new Point(((this.Width - btPicture.Width) / 2), btn_pic_small.Location.Y + 50);
            picboxShow.Location = g_pPos;
            iPicW = btPicture.Width;
            fsStream.Close();
            //btPicture.Dispose();
        }

        private void picboxShow_MouseWheel(object sender, MouseEventArgs e)
        {
            fnChangePicSize(0, e.Delta);
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            if (text_code.Enabled)
            {
                var confirmResult = MessageBox.Show("確認要更新嗎?", "更新!!", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    if (fnUpdateProduct())
                    {
                        MessageBox.Show("更新成功！！");
                        DialogResult = DialogResult.OK;
                    }
                    this.Close();
                }
                else
                {
                }
            }
            else
            {
                MessageBox.Show("目前為唯讀模式！！");
            }
        }

        private bool fnUpdateProduct()
        {
            StreamWriter swWrite = null;
            try
            {
                string sNewImgPath = ProductClass.ImagePath + text_filename.Text + ".png";
                string sNewDataPath = ProductClass.DataPath + text_filename.Text + ".GP";
                swWrite = new StreamWriter(g_prodData.FilePath, false, Encoding.Default);
                if (text_filename.Text == g_prodData.FileName || (!File.Exists(sNewImgPath) && !File.Exists(sNewDataPath)))
                {
                    fnChangeImage();
                    swWrite.WriteLine(text_code.Text.ToString());
                    swWrite.WriteLine(text_remarks.Text.ToString());
                    swWrite.Close();
                    g_prodData.Code = text_code.Text;
                    g_prodData.Remarks = text_remarks.Text;
                    fnChangeFileName(sNewImgPath, sNewDataPath);
                }
                else
                {
                    MessageBox.Show("檔名重複");
                    return false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Edit更新錯誤");
                return false;
            }
            finally
            {
                swWrite.Close();
            }
            return true;
        }

        private void fnChangeImage()
        {
            if (g_bImageChange)
            {
                picboxShow.Image.Save(g_prodData.FileImagePath);
            }
        }

        private void fnChangeFileName(string sNewImgPath, string sNewDataPath)
        {
            if (text_filename.Text != g_prodData.FileName)
            {
                File.Move(g_prodData.FileImagePath, sNewImgPath);
                File.Move(g_prodData.FilePath, sNewDataPath);
                g_prodData.FileName = text_filename.Text;
                g_prodData.fnRefreshString(true);
            }
            else
            {
                g_prodData.fnRefreshString();
            }
        }
        private void ProductEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)27: // ESC
                    this.Close();
                    break;
                case '+': // +
                    btn_pic_big_Click(sender, e);
                    break;
                case '-': // -
                    btn_pic_small_Click(sender, e);
                    break;
            }
        }

        private void ProductEdit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true)
            {
                switch (e.KeyCode)
                {
                    case Keys.S:
                        btn_Update_Click(sender, e);
                        break;
                    case Keys.E:
                        btn_edit_Click(sender, e);
                        break;
                }

            }
        }

        private void picboxShow_MouseMove(object sender, MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (g_bMove)
            {
                //picboxShow.Left += e.X - g_pPos.X;
                //picboxShow.Top += e.Y - g_pPos.Y;

                g_iOffsetX = g_pPos.X - e.X;
                g_iOffsetY = g_pPos.Y - e.Y;
                g_iX -= g_iOffsetX;
                g_iY -= g_iOffsetY;
                Bitmap btImage = new Bitmap(this.picboxShow.Image);
                Graphics ghSaveType = picboxShow.CreateGraphics();
                ghSaveType.Clear(picboxShow.BackColor);
                ghSaveType.DrawImage(btImage, g_iX, g_iY);
                g_pPos.X = e.X;
                g_pPos.Y = e.Y;
                btImage.Dispose();
                ghSaveType.Dispose();
            }
        }



        private void picboxShow_MouseDown(object sender, MouseEventArgs e)
        {
            picboxShow.Cursor = Cursors.Hand;
            g_bMove = true;
            g_pPos.X = e.X;
            g_pPos.Y = e.Y;
        }

        private void picboxShow_MouseUp(object sender, MouseEventArgs e)
        {
            picboxShow.Cursor = Cursors.Default; //松开鼠标时，形状恢复为箭头
            g_bMove = false;
        }

        private void btn_pic_big_Click(object sender, EventArgs e)
        {
            fnChangePicSize(0, 100);
        }
        private void btn_pic_small_Click(object sender, EventArgs e)
        {
            fnChangePicSize(1);
        }

        private void fnChangePicSize(int iType, int iOffset = 0)
        {
            Bitmap btPicture = null;
            try
            {
                if (iType == 0)
                {
                    if (iPicW <= picboxShow.Image.Width + iOffset)
                    {
                        btPicture = new Bitmap(picboxShow.Image.Width + iOffset, picboxShow.Image.Height + iOffset);
                        Graphics ghSaveType = Graphics.FromImage(btPicture);
                        // 插值算法
                        ghSaveType.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        ghSaveType.DrawImage(picboxShow.Image, new Rectangle(0, 0, btPicture.Width, btPicture.Height), new Rectangle(0, 0, picboxShow.Image.Width, picboxShow.Image.Height), GraphicsUnit.Pixel);
                        ghSaveType.Dispose();
                        picboxShow.Image = btPicture;
                        picboxShow.Width = btPicture.Width;
                        picboxShow.Height = btPicture.Height;
                        g_pPos = new Point(((this.Width - btPicture.Width) / 2), btn_pic_small.Location.Y + 50);
                        picboxShow.Location = g_pPos;
                    }
                }
                else
                {
                    fnInitPic();
                }
            }
            catch
            {
                fnInitPic();
            }

        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            text_code.Enabled = !text_code.Enabled;
            text_remarks.Enabled = !text_remarks.Enabled;
            text_filename.Enabled = !text_filename.Enabled;
            btn_file.Enabled = !btn_file.Enabled;
        }

        private void btn_file_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFile = new OpenFileDialog();
                openFile.Filter = "圖片(jpeg, bmp, png) | *.jpg;*.jpeg;*.png;*.bmp|" + "所有檔案 |*.*";
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
                    g_bImageChange = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

    }
}
