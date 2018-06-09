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
        public Log(string type, string message)
        {
            Message = message;
            Type = type;
            //Type = ConverToString(type);
        }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Type")]
        public string Type { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Message")]
        public string Message { get; set; }


        public static string ConverToString(int type)
        {
            if (type == (int)MessageTypeEnum.FAIL)
            {
                return "FAIL";

            }
            if (type == (int)MessageTypeEnum.INFO)
            {
                return "INFO";

            }

            if (type == (int)MessageTypeEnum.WARNING)
            {
                return "WARNING";
            }
            return null;
        }
        
    }
}