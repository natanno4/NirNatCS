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

        public string PhotoPath { get; }

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

        public Photo(string path, string thumbPath)
        {
            Name = Path.GetFileName(path);
            Month = Path.GetDirectoryName(path);
            Month = new DirectoryInfo(Month).Name;
            Year = Path.GetDirectoryName(Path.GetDirectoryName(path));
            Year = new DirectoryInfo(Year).Name;
        }
    }
}