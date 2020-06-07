using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductCodeSearch
{
    public partial class ProductForm : Form
    {
        private ProductClass g_pcProductNowData = null;
        private ImageList g_liImage = null;

        public ProductForm()
        {
            InitializeComponent();
            fnInit();

        }

        #region "初始化"
        private void fnInit()
        {
            List<string> listPImageName = new List<string>();
            ProductClass.fnInit();
            g_pcProductNowData = new ProductClass();
            g_liImage = fnInitListImage(listPImageName);
            fnInitListView(listPImageName, ProductClass.ProductAllData);
            fnInitListView(listPImageName, g_pcProductNowData);
        }
        private ImageList fnInitListImage(List<string> listPImageName)
        {
            ImageList ilImage = new ImageList();
            int iPos = 0;

            ilImage.ImageSize = new Size(100, 130);
            foreach (string sFileName in ProductClass.ImageAllName)
            {
                if (ProductClass.ListShow[iPos])
                {
                    Functions.fnMakeProductData(sFileName);
                    Image imgImage = Functions.fnGetImage("Image\\" + sFileName);
                    ilImage.Images.Add(imgImage);
                    listPImageName.Add(sFileName);
                }
                iPos++;
            }
            return ilImage;
        }

        private void fnInitListView(List<string> listPImageName, ProductClass prodClass)
        {
            lvProduct.Clear();
            lvProduct.View = View.LargeIcon;
            lvProduct.LargeImageList = g_liImage;
            lvProduct.ShowItemToolTips = true;
            fnSetImage(lvProduct, prodClass, listPImageName);

        }
        private void fnSetImage(ListView lvItem, ProductClass prodClass, List<string> listPImageName)
        {
            for (int iPos = 0; iPos < g_liImage.Images.Count; iPos++)
            {
                string sShowName = listPImageName[iPos].Substring(0, listPImageName[iPos].IndexOf("."));
                ProductData prodData = new ProductData(sShowName);

                prodData.FileImagePath = ProductClass.ImagePath + listPImageName[iPos];
                fnInsertProductShow(prodClass, prodData);
            }
        }

        #endregion

        #region "Data處理事件"
        private void fnInsertProductShow(ProductClass prodClass, ProductData prodData, bool bIsAll = false)
        {
            int iPos = lvProduct.Items.Count;
            lvProduct.Items.Add("");
            lvProduct.Items[iPos].ImageIndex = iPos;
            lvProduct.Items[iPos].ToolTipText = prodData.ToString();
            lvProduct.Items[iPos].Text = prodData.FileName + "\n" + prodData.Code;
            prodClass.fnAdd(prodData);
            if (bIsAll)
            {
                ProductClass.fnAddAll(prodData);
            }
        }

        private void fnUpdateProductShow(ProductData prodData, int iPos)
        {
            Bitmap btData = new Bitmap(Functions.fnGetImage(prodData.FileImagePath, true), 100, 130);
            g_liImage.ImageSize = new Size(100, 130);
            lvProduct.Items[iPos].ToolTipText = prodData.ToString();
            lvProduct.Items[iPos].Text = prodData.FileName + "\n" + prodData.Code;
            g_liImage.Images[iPos] = btData;
        }
        #endregion

        #region "搜尋"
        private void btn_search_Click(object sender, EventArgs e)
        {
            fnCodeSearch();
            fnFileNameSearch();
            fnFilterShow();
        }

        private void fnFilterShow()
        {
            List<string> listPImageName = new List<string>();
            g_pcProductNowData.fnClear();
            g_liImage = fnInitListImage(listPImageName);
            fnInitListView(listPImageName, g_pcProductNowData);
        }

        private void fnCodeSearch()
        {
            string sCode = text_code.Text;
            for (int iPos = 0; iPos < ProductClass.Size; iPos++)
            {
                if (ProductClass.ProductAllData.fnGet(iPos).Code.IndexOf(sCode) >= 0 || sCode == "")
                {
                    ProductClass.ListShow[iPos] = true;
                }
                else
                {
                    ProductClass.ListShow[iPos] = false;
                }
            }
        }

        private void fnFileNameSearch()
        {
            string sFileName = text_file_name.Text;
            for (int iPos = 0; iPos < ProductClass.Size; iPos++)
            {
                if (ProductClass.ListShow[iPos] &&
                    (ProductClass.ProductAllData.fnGet(iPos).FileName.IndexOf(sFileName) >= 0 || sFileName == ""))
                {
                    ProductClass.ListShow[iPos] = true;
                }
                else
                {
                    ProductClass.ListShow[iPos] = false;
                }
            }
        }
        #endregion

        private void lvProduct_DoubleClick(object sender, EventArgs e)
        {
            fnOpenEdit();
        }

        #region "Form回傳事件"
        private string fnProductEditResult(ref ProductEdit formProductEdit, ProductData prodData, int iPos)
        {
            switch (formProductEdit.ShowDialog())
            {
                case DialogResult.OK:
                    fnUpdateProductShow(prodData, iPos);
                    return "更新成功";
                default:
                    return "";
            }
        }

        private string fnProductInsertResult(ref ProductInsert formProductEdit, ref ProductData prodData)
        {
            switch (formProductEdit.ShowDialog())
            {
                case DialogResult.OK:
                    fnInsertProductShow(g_pcProductNowData, prodData, true);
                    MessageBox.Show("新增成功");
                    return "新增成功";
                default:
                    return "";
            }
        }
        #endregion

        #region "Menu事件"
        private void menu_open_insert_Click(object sender, EventArgs e)
        {
            ProductData prodData = null;
            ProductInsert formProductInsert = new ProductInsert(ref prodData);
            formProductInsert.StartPosition = FormStartPosition.CenterParent;
            fnProductInsertResult(ref formProductInsert, ref prodData);
        }

        private void menu_open_edit_Click(object sender, EventArgs e)
        {
            fnOpenEdit();
        }
        #endregion

        private void fnOpenEdit()
        {
            if (lvProduct.SelectedItems.Count > 0)
            {
                int iPos = lvProduct.SelectedItems[0].Index;
                ProductEdit formProductEdit = new ProductEdit(g_pcProductNowData.fnGet(iPos));
                formProductEdit.StartPosition = FormStartPosition.CenterParent;
                fnProductEditResult(ref formProductEdit, g_pcProductNowData.fnGet(iPos), iPos);
            }
        }

        private void fnDelete(ProductData prodData, int iPos)
        {
            lvProduct.Items.RemoveAt(iPos);
            g_liImage.Images.RemoveAt(iPos);
            File.Delete(prodData.FilePath);
            File.Delete(prodData.FileImagePath);
            fnInit();
        }

        private void lvProduct_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (lvProduct.FocusedItem.Bounds.Contains(e.Location) == true)
                {
                    cmenu_item_action.Show(Cursor.Position);
                }
            }
        }

        private void menu_delete_Click(object sender, EventArgs e)
        {
            if (lvProduct.SelectedItems.Count > 0)
            {
                int iPos = lvProduct.SelectedItems[0].Index;
                var confirmResult = MessageBox.Show("確認要刪除" + g_pcProductNowData.fnGet(iPos).FileName + "嗎?", "刪除", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    fnDelete(g_pcProductNowData.fnGet(iPos), iPos);
                }

            }
        }

        private void menu_delets_Click(object sender, EventArgs e)
        {
            menu_delete_Click(sender, e);
        }


    }
}
