using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace ImageServiceWebApp.Models
{
    public class Photo
    {

        [Required]
        public string PhotoPath { get; }

        public string realTumbPath {get; set;}

        [Required]
        public string PhotoThumbPath { get; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Year")]
        public string Year { get; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Month")]
        public string Month { get; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        public string Name { get; }
        /// <summary>
        /// constructor.
        /// </summary>
        /// <param name="path">path to outputdir</param>
        /// <param name="thumbPath">path to outputdir/thumbnails</param>
        public Photo(string thumbPath, string folderName)
        {
            try
            {
                int folderNameLocation, length;
                //keep the full thumbnail path
                realTumbPath = thumbPath;
                //get infromation about name and date
                Name = Path.GetFileName(thumbPath);
                Month = Path.GetDirectoryName(thumbPath);
                Month = new DirectoryInfo(Month).Name;
                Year = Path.GetDirectoryName(Path.GetDirectoryName(thumbPath));
                Year = new DirectoryInfo(Year).Name;

                length = thumbPath.Length;
                folderNameLocation = thumbPath.IndexOf(folderName);
                //get the path from the directory name.
                string DirName = thumbPath.Substring(folderNameLocation, length - folderNameLocation);
                //add to present in web.
                PhotoThumbPath = @"~\" + DirName;
                //keep dir photo path
                PhotoPath = realTumbPath.Replace(@"Thumbnails\", string.Empty);
            } catch(Exception e)
            {
                Console.WriteLine(e.Data.ToString());
            }

        }
    }
}