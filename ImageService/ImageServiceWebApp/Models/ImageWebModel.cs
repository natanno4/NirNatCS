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
            this.UpdateStudentList();
            ConfigInfoModel model = ConfigInfoModel.SingeltonConfig;
            OutPutDir = model.OutPutDir;
            if (this.IsConnected)
            {
                System.Threading.Thread.Sleep(5);
                client.HandleRecived();
            }
        }

        private string OutPutDir { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "IsConnected")]
        public bool IsConnected { get; set; }


        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Number Of Photos in Output Directory:")]
        public int NumberOfPhotos { get; set; }


        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "StudentsInfo")]
        public List<Student> StudentsInfo { get; set; }
        /// <summary>
        /// Update the student list, reads the them from the StudentsInfo.txt file in the App_Data directory.
        /// </summary>
        private void UpdateStudentList()
        {
            try
            {
                string line;
                StreamReader file = new StreamReader(HttpContext.Current.Server.MapPath("~/App_Data/StudentsInfo.txt"));
                while ((line = file.ReadLine()) != null)
                {
                    string[] info = line.Split(';');
                    this.StudentsInfo.Add(new Student(info[0], info[1], info[2]));
                }
                file.Close();
            } catch(Exception e)
            {
                Console.WriteLine(e.Data.ToString());
            }
        }

        /// <summary>
        /// update the number of photos in current outputdir.
        /// </summary>
        /// <returns>0 if there isnt connectin, else the number of photos</returns>
        public int UpdateNumberOFphotos()
        {
            if(!client.IsConnected())
            {
                return 0;
            }
            
            string path = OutPutDir;
            int count = 0;
            try
            {
                //count phtos in dir directory and its subs.
                count += (int)(from file in Directory.EnumerateFiles(path, "*bmp", SearchOption.AllDirectories) select file).Count();
                count += (int)(from file in Directory.EnumerateFiles(path, "*jpg", SearchOption.AllDirectories) select file).Count();
                count += (int)(from file in Directory.EnumerateFiles(path, "*png", SearchOption.AllDirectories) select file).Count();
                count += (int)(from file in Directory.EnumerateFiles(path, "*gif", SearchOption.AllDirectories) select file).Count();
                count = count / 2;
            }catch(Exception e)
            {
                Console.WriteLine(e.Data.ToString());
                return 0;
            }
            return count;
        }
    }
}