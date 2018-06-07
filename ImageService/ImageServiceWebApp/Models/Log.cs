using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ImageService.Logging.Modal;

namespace ImageServiceWebApp.Models
{
    public class Log
    {
        public Log(int type, string message)
        {
            Message = message;
            if(type == (int)MessageTypeEnum.FAIL)
            {
                Type = "Fail";
                return;
            } 
            if(type == (int)MessageTypeEnum.INFO)
            {
                Type = "INFO";
                return;
            }

            if(type == (int)MessageTypeEnum.WARNING)
            {
                Type = "WARNING";
                return;
            }
        }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Type")]
        public string Type { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Message")]
        public string Message { get; set; }
    }
}