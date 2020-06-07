using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCodeSearch
{
    class ProductClass
    {
        private static ProductClass g_pcProductAllData = null;
        private static List<string> g_listTitleData = null;
        private static List<string> g_listImageName = null;
        private static List<bool> g_listListShow = null;
        private static string g_sProductImagePath = AppDomain.CurrentDomain.BaseDirectory + "Image\\";
        private static string g_sProductDataPath = AppDomain.CurrentDomain.BaseDirectory + "ProductData\\";
        private static int g_iTitleSize = 0;

        private List<ProductData> g_listProductData = null;

        public ProductClass()
        {
            g_listProductData = new List<ProductData>();
        }
        public static void fnInit()
        {
            fnInitFile();
            fnInitDataTitle();
        }
        private static void fnInitDataTitle()
        {
            string sPath = g_sProductDataPath + "DataTitle.GP";
            StreamReader srReader = new StreamReader(sPath, Encoding.Default);

            g_listTitleData = new List<string>();
            g_iTitleSize = 0;

            string sLine = "";
            while ((sLine = srReader.ReadLine()) != null)
            {
                g_listTitleData.Add(sLine.ToString());
                g_iTitleSize++;
            }
        }
        private static void fnInitFile()
        {
            DirectoryInfo diPath = new DirectoryInfo("Image");
            string[] sFilterDatas = { "jpg", "png", "jpge", "bmp" };

            g_pcProductAllData = new ProductClass();
            g_listListShow = new List<bool>();
            g_listImageName = new List<string>();

            FileInfo[] fiFilters = diPath.GetFiles("*.*")
                          .Where(data => sFilterDatas.Contains(data.ToString().Split('.').Last().ToLower())).ToArray();
            for (int iPos = 0; iPos < fiFilters.Length; iPos++)
            {
                g_listImageName.Add(fiFilters[iPos].Name);
                g_listListShow.Add(true);
            }
        }
        public static ProductClass ProductAllData
        {
            get
            {
                return g_pcProductAllData;
            }
        }
        public static List<string> TitleData
        {
            get
            {
                return g_listTitleData;
            }
        }
        public static List<bool> ListShow
        {
            get
            {
                return g_listListShow;
            }
        }
        public static List<string> ImageAllName
        {
            get
            {
                return g_listImageName;
            }
        }
        public static string DataPath
        {
            get
            {
                return g_sProductDataPath;
            }
        }

        public static string ImagePath
        {
            get
            {
                return g_sProductImagePath;
            }
        }

        public static void fnAddAll(ProductData prodData)
        {
            g_pcProductAllData.fnAdd(prodData);
            g_listListShow.Add(true);
            g_listImageName.Add(prodData.FileName + ".png");
        }

        public static void fnRemoveAtAll(int iPos)
        {
            g_pcProductAllData.fnRemoveAt(iPos);
            g_listListShow.RemoveAt(iPos);
            g_listImageName.RemoveAt(iPos);
        }
        public static int Size
        {
            get
            {
                return g_listImageName.Count;
            }
        }
        public static int TitleSize
        {
            get
            {
                return g_iTitleSize;
            }
        }
        public ProductData fnGet(int iPos)
        {
            return g_listProductData[iPos];
        }
        public void fnClear()
        {
            g_listProductData.Clear();
        }
        public void fnAdd(ProductData prdData)
        {
            g_listProductData.Add(prdData);
        }

        public void fnRemoveAt(int iPos)
        {
            g_listProductData.RemoveAt(iPos);
        }
    }

}
