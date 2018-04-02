using ImageService.Infrastructure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Configuration;

namespace ImageService.Modal
{
    public class ImageServiceModal : IImageServiceModal
    {
        #region Members
        private string m_OutputFolder;            // The Output Folder
        private int m_thumbnailSize;             // The Size Of The Thumbnail Size
        public ImageServiceModal ()
        {
            this.m_OutputFolder = ConfigurationManager.AppSettings["OutputDir"];
            DirectoryInfo info = Directory.CreateDirectory(this.m_OutputFolder);
            if (!Int32.TryParse(ConfigurationManager.AppSettings["ThumbnailSize"], out this.m_thumbnailSize))
            {

            }

        }
        #endregion
        public string CreateFolder(string year, string month)
        {
            string tumbnailPath = this.m_OutputFolder + "/Thumbnails";
            string newPath = m_OutputFolder + "/" + year + month;
            if (!Directory.Exists(newPath))
            {
                return Directory.CreateDirectory(newPath).Name;
                
            }
            string newThumbPath = tumbnailPath + "/" + year + month;
            if (!Directory.Exists(newThumbPath))
            {
                return Directory.CreateDirectory(newThumbPath).Name;
            }
            return newPath;
        }
        public string AddFile(string path, out bool result)
        {
            Image image = Image.FromFile(path);
            string tumbPath = this.m_OutputFolder + "/Thumbnails";
            if (File.Exists(path))
            {
                DateTime t = this.ExtractDate(path);
                string newPath = this.CreateFolder(this.ConvertDate(t, "year"), this.ConvertDate(t, "month"));
                string fileName = "/" + Path.GetFileName(path);  
                File.Move(path, newPath + fileName);
                string tumb = this.CreateTumbnailFolder(this.ConvertDate(t, "year"), this.ConvertDate(t, "month"));
                File.Copy(tumb, tumb + fileName);
                if (path.Equals(newPath))
                {
                    result = false;
                    return path;
                }
                result = true;
                return newPath;
            }
            result = false;
            return path;
          
        }
        public DateTime ExtractDate(string path)
        {
            Image myImage = Image.FromFile(path);
            PropertyItem propItem = myImage.GetPropertyItem(306);
            DateTime dtaken;

            //Convert date taken metadata to a DateTime object
            string sdate = Encoding.UTF8.GetString(propItem.Value).Trim();
            string secondhalf = sdate.Substring(sdate.IndexOf(" "), (sdate.Length - sdate.IndexOf(" ")));
            string firsthalf = sdate.Substring(0, 10);
            firsthalf = firsthalf.Replace(":", "-");
            sdate = firsthalf + secondhalf;
            return DateTime.Parse(sdate);
        }
        public string ConvertDate(DateTime dt, string component)
        {
            if (component.Equals("month"))
            {
                return dt.Month.ToString;
            } else if (component.Equals("year"))
            {
                return dt.Year.ToString;
            } else
            {
                return dt.Day.ToString;
            }
        }

        private string CreateTumbnailFolder(string year, string month)
        {
            string tumbnailPath = this.m_OutputFolder + "/Thumbnails";
            string newThumbPath = tumbnailPath + "/" + year + month;
            if (!Directory.Exists(newThumbPath))
            {
                return Directory.CreateDirectory(newThumbPath).Name;
            }
            return newThumbPath;

        }
    }
    
}
