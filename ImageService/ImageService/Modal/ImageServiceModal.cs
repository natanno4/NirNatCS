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

namespace ImageService.Modal
{
    public class ImageServiceModal : IImageServiceModal
    {
        #region Members
        private string m_OutputFolder;            // The Output Folder
        private int m_thumbnailSize;              // The Size Of The Thumbnail Size
        public ImageServiceModal (string output, int size)
        {
            this.m_OutputFolder = output;
            this.m_thumbnailSize = size;
        }
        #endregion
        public string CreateFolder(string year)
        {
            
            string newPath = m_OutputFolder + "/" + year;
            if (!Directory.Exists(newPath))
            {
                return Directory.CreateDirectory(newPath).ToString;
                
            }
            return this.m_OutputFolder + "/" + year;
        }
        public string AddFile(string path, out bool result)
        {
            try
            {
                System.IO.File.Move(path, new_path);
            } catch (Exception e)
            {
                Console.WriteLine("Error with moving the image");
                result = false;
                return path;
            }
            result = true;
            return new_path;
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
    }
}
