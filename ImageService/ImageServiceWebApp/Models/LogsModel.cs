using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ImageService.Logging.Modal;

namespace ImageServiceWebApp.Models
{
    public class LogsModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "logs:")]
        public List<MessageRecievedEventArgs> Logs { get; set; }

    }
}