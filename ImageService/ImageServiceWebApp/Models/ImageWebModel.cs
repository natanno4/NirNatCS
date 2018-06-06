using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Communication;
using System.IO;

namespace ImageServiceWebApp.Models
{
    public class ImageWebModel
    {
        private IClient client;

        public ImageWebModel()
        {
            this.client = GuiClient.instanceS;
            this.client.Connect();
            this.IsConnected = this.client.IsConnected();
            this.StudentsInfo = new List<Student>();
            this.updateStudentList();
        }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "IsConnected")]
        public bool IsConnected { get; set; }


        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "IsConnected")]
        public bool NumberOfPhotos { get; set; }


        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "StudentsInfo")]
        public List<Student> StudentsInfo { get; set; }

        private void updateStudentList()
        {
            string line;
            StreamReader file = new StreamReader(HttpContext.Current.Server.MapPath("~/App_Data/StudentsInfo.txt"));
            while ((line = file.ReadLine()) != null)
            {
                string [] info = line.Split(';');
                this.StudentsInfo.Add(new Student(info[0], info[1], info[2]));
            }
            file.Close();
        }
    }




 

    
}