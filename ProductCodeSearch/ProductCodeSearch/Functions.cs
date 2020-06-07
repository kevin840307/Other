using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCodeSearch
{
    class Functions
    {
        public static Image fnGetImage(string sFileName, bool bHasPath = false)
        {
            FileStream fsStream = null;
            if (bHasPath)
            {
                fsStream = new FileStream(sFileName, FileMode.Open);
            }
            else
            {
                fsStream = new FileStream(AppDomain.CurrentDomain.BaseDirectory + sFileName, FileMode.Open);
            }
            Image imaData = Image.FromStream(fsStream);
            fsStream.Close();
            return imaData;
        }

        public static bool fnMakeProductData(string sDataName)
        {
            string sData = (sDataName.IndexOf(".") >= 0) ? sDataName.Substring(0, sDataName.IndexOf(".")) : sDataName;
            string sDataPath = ProductClass.DataPath + sData + ".GP";
            if (!File.Exists(sDataPath))
            {
                File.WriteAllText(sDataPath, "");
                return true;
            }
            return false;
        }

        public static bool fnSaveImage(Image imgData, string sDataName)
        {
            try
            {
                string sPath = ProductClass.ImagePath + sDataName + ".png";
                imgData.Save(sPath, ImageFormat.Png);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
