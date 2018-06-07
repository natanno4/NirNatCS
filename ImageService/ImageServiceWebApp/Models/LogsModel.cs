using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Communication;

namespace ImageServiceWebApp.Models
{
    public class LogsModel
    {

        private IClient client;

        public LogsModel()
        {
            client = GuiClient.instanceS;
            client.Connect();
        }

        [Required]
        [DataType(DataType.Text)]
        public List<Log> Logs { get; set; }
    }
}