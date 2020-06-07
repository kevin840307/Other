using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCodeSearch
{
    public class ProductData
    {
        private string g_sFilePath = "";
        private string g_sFileImagePath = "";
        private string g_sDataString = "";
        private string g_sFileName = "";
        private List<string> g_sData = new List<string>();

        public ProductData()
        {
        }
        public ProductData(string sFileName)
        {
            fnInit(sFileName);
        }

        private void fnInit(string sFileName)
        {
            g_sFileName = sFileName;
            g_sFilePath = ProductClass.DataPath + sFileName + ".GP";
            InitDataString();
        }

        public string FilePath
        {
            get
            {
                return g_sFilePath;
            }
        }

        public string FileImagePath
        {
            get
            {
                return g_sFileImagePath;
            }
            set
            {
                g_sFileImagePath = value;
            }
        }

        public string FileName
        {
            get
            {
                return g_sFileName;
            }
            set
            {
                g_sFileName = value;
            }
        }

        public string Code
        {
            get
            {
                if (g_sData.Count > 0)
                {
                    return g_sData[0];
                }
                return "";
            }

            set
            {
                g_sData[0] = value;
            }
        }

        public string Remarks
        {
            get
            {
                if (g_sData.Count > 1)
                {
                    return g_sData[1];
                }
                return "";
            }
            set
            {
                g_sData[1] = value;
            }
        }

        private void InitDataString()
        {
            StreamReader srReader = null;
            try
            {
                string sLine = "";
                srReader = new StreamReader(g_sFilePath, Encoding.Default);
                g_sData.Clear();
                for (int iPos = 0; iPos < ProductClass.TitleSize; iPos++)
                {
                    if ((sLine = srReader.ReadLine()) != null && sLine.Replace(" ", "") != "")
                    {
                        g_sDataString += ProductClass.TitleData[iPos].ToString() + "：" + sLine.ToString() + "\n";
                        g_sData.Add(sLine.ToString());
                    }
                    else
                    {
                        g_sData.Add("");
                    }
                }
                srReader.Close();
            }
            catch
            {
            }
            finally
            {
                srReader.Close();
            }
        }

        public void fnLoadData(string sFileName)
        {
            fnInit(sFileName);
        }

        public void fnRefreshString(bool bIsNew = false)
        {
            g_sDataString = "";
            for (int iPos = 0; iPos < ProductClass.TitleSize; iPos++)
            {
                if (g_sData[iPos].Length > 0)
                {
                    g_sDataString += ProductClass.TitleData[iPos].ToString() + "：" + g_sData[iPos] + "\n";
                }
            }
            if (bIsNew)
            {
                g_sFilePath = ProductClass.DataPath + g_sFileName + ".GP";
                g_sFileImagePath = ProductClass.ImagePath + g_sFileName + ".png";
            }
        }

        public override string ToString()
        {
            if (g_sDataString.Length == 0)
            {
                return "無新增資料";
            }
            return g_sDataString.ToString();
        }
    }
}
