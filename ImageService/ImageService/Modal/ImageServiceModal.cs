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
        /// <summary>
        /// constructor.
        /// receive all his data from the AppConfig.
        /// </summary>
        public ImageServiceModal()
        {
            this.m_OutputFolder = ConfigurationManager.AppSettings["OutputDir"];
            string tumbnailPath = this.m_OutputFolder + "/Thumbnails";
            DirectoryInfo info = Directory.CreateDirectory(this.m_OutputFolder);
            Directory.CreateDirectory(tumbnailPath);
            if (!Int32.TryParse(ConfigurationManager.AppSettings["ThumbnailSize"], out this.m_thumbnailSize))
            {

            }

        }
        #endregion
        /// <summary>
        /// create an appropriate folder (if doesnt exist already) with year and month.
        /// </summary>
        /// <param name="year">year that photo has taken</param>
        /// <param name="month">month that photo has taken</param>
        /// <returns>return path to new folder</returns>
        public string CreateFolder(string year, string month)
        {
            string newPath = m_OutputFolder + "/" + year + month;
            if (!Directory.Exists(newPath))
            {
                return Directory.CreateDirectory(newPath).Name;

            }
            return newPath;
        }
        /// <summary>
        /// add file to appropriate folder (and also thumbnails folder), a part of interface IImageModal.
        /// </summary>
        /// <param name="path"> path to the file</param>
        /// <param name="result"> boolean result of process</param>
        /// <returns> ......</returns>
        public string AddFile(string path, out bool result)
        {
            Image image = Image.FromFile(path);
            Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(TumbnailCallback);
            Bitmap bitMap = new Bitmap(image);
            if (File.Exists(path))
            {
                DateTime t = this.ExtractDate(path);
                string newPath = this.CreateFolder(this.ConvertDate(t, "year"), this.ConvertDate(t, "month"));
                string fileName = "/" + Path.GetFileName(path);
                File.Move(path, newPath + fileName);
                string tumb = this.CreateTumbnailFolder(this.ConvertDate(t, "year"), this.ConvertDate(t, "month"));
                Image tumbImage = bitMap.GetThumbnailImage(this.m_thumbnailSize, this.m_thumbnailSize, myCallback, IntPtr.Zero);
                tumbImage.Save(tumb);
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
        /// <summary>
        /// from a given path to a photo, extract the date which the photo has taken.
        /// </summary>
        /// <param name="path"> path to the photo</param>
        /// <returns>the full date (in shape of DateTime class) of photo</returns>
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
        /// <summary>
        /// extract from a given DateTime the specified component which 
        /// we want to use.
        /// </summary>
        /// <param name="dt">DateTime</param>
        /// <param name="component"> component of DateTime(day/month/year)</param>
        /// <returns>a string represent of the component</returns>
        public string ConvertDate(DateTime dt, string component)
        {
            if (component.Equals("month"))
            {
                return dt.Month.ToString();
            }
            else if (component.Equals("year"))
            {
                return dt.Year.ToString();
            }
            else
            {
                return dt.Day.ToString();
            }
        }
        /// <summary>
        /// create a thumbnail folder with year and month.
        /// </summary>
        /// <param name="year">year</param>
        /// <param name="month">month</param>
        /// <returns>path to new folder</returns>
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
        /// <summary>
        /// .....
        /// </summary>
        /// <returns>bool</returns>
        private bool TumbnailCallback()
        {
            return false;
        }
    }

}
