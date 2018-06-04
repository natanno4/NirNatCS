using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImageServiceWebApp.Models
{
    public class configInfoModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "OutPut Directory:")]
        public string OutPutDir { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "SourceName:")]
        public string SourceName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "LogName:")]
        public string LogName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Thumnail size:")]
        public string Thumbnail { get; set; }


        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Handlers:")]
        public List<string> Handlers { get; set; }

    }
}