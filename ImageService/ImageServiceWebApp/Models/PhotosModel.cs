using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace ImageServiceWebApp.Models
{
    public class PhotosModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "PhotoPath")]
        public string PhotoPath { get; set; }

        [Required]
        [Display(Name = "ListPhotos")]
        public List<Photo> ListPhotos = new List<Photo>();

        private ConfigInfoModel conModel = ConfigInfoModel.SingeltonConfig;

        public PhotosModel()
        {
            string outputdir = conModel.OutPutDir;

            InitiliazeList(outputdir);
        }
        public int NumOfPhotos()
        {
            return this.ListPhotos.Count;
        }
        public void InitiliazeList(string outputPath)
        {
            string thumbnail = outputPath + "//Thumbnails";
            DirectoryInfo dirThumb = new DirectoryInfo(thumbnail);
            foreach (DirectoryInfo dir in dirThumb.GetDirectories())
            {
                foreach (DirectoryInfo sub_dir in dir.GetDirectories())
                {
                    foreach (FileInfo file in sub_dir.GetFiles())
                    {
                        if (CheckIfValidPhoto(file.Extension.ToLower()))
                        {
                            ListPhotos.Add(new Photo(outputPath + "//" + dir.Name + "//" + sub_dir.Name + "//" + file.Name, file.FullName));
                            // need to change photo
                        }
                    }
                }
            }

        }
        bool CheckIfValidPhoto(string pht)
        {
            if (pht.Contains(".jpg") || pht.Contains(".bmp") || pht.Contains(".gif")
                || pht.Contains(".png"))
            {
                return true;
            }
            return false;
        }
        public void RemovePhoto(Photo rem)
        {
            foreach (Photo p in ListPhotos)
            {
                if (rem.Name.Equals(p.Name))
                {
                    ListPhotos.Remove(rem);
                    string regularPathToRemove = rem.PhotoPath;
                    File.Delete(regularPathToRemove);
                    //need to add path of thumbnails to remove
                    break;
                }
            }
        }
    }
}