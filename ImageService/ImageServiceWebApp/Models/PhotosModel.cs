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

        public bool DeleteFromView { get; set; }

        public string OutputDir { get; set; }

        public Photo viewPhoto { get; set; }

        public Photo photoToDelete { get; set; }

        public PhotosModel()
        {
            if(!conModel.IsConnected())
            {
                return;
            }
            OutputDir = conModel.OutPutDir;
            DeleteFromView = false;
            viewPhoto = null;
            photoToDelete = null;
        }

        /// <summary>
        /// return numbers of photos.
        /// </summary>
        /// <returns></returns>
        public int NumOfPhotos()
        {
            return this.ListPhotos.Count;
        }
        /// <summary>
        /// initliaze Photos list.
        /// create a DirInfo of Thumbnails, search in all his sub-directores, 
        /// and for every photo create an instance of class photo with his details
        /// and add to the photos list.
        /// </summary>
        /// <param name="outputPath">outputdir path</param>
        public void InitiliazeList(string outputPath)
        {
            string thumbnail = outputPath + @"\\Thumbnails";
            DirectoryInfo dirThumb = new DirectoryInfo(thumbnail);
            DirectoryInfo dirFolder = new DirectoryInfo(outputPath);
            string folderName = dirFolder.Name; 
            foreach (DirectoryInfo dir in dirThumb.GetDirectories())
            {
                foreach (DirectoryInfo sub_dir in dir.GetDirectories())
                {
                    foreach (FileInfo file in sub_dir.GetFiles())
                    {
                        if (CheckIfValidPhoto(file.Extension.ToLower()))
                        {
                            ListPhotos.Add(new Photo(file.FullName , folderName));
                        }
                    }
                }
            }

        }
        /// <summary>
        /// receive a full path to a specific photo, and check if
        /// it fit to format the the ImageService support.
        /// </summary>
        /// <param name="pht">path</param>
        /// <returns>true if fit to one of the four extension, else false</returns>
        private  bool CheckIfValidPhoto(string pht)
        {
            if (pht.Contains(".jpg") || pht.Contains(".bmp") || pht.Contains(".gif")
                || pht.Contains(".png"))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// RemovePhoto.
        /// </summary>
        /// <param name="rem">Photo to remove</param>
        public void RemovePhoto(Photo rem)
        {
            try
            {
                foreach (Photo p in ListPhotos)
                {
                    if (rem.Name.Equals(p.Name))
                    {
                        ListPhotos.Remove(rem);
                        string regularPathToRemove = rem.PhotoPath;
                        File.Delete(regularPathToRemove);
                        string regularThumbPathToRemove = rem.realTumbPath;
                        File.Delete(regularThumbPathToRemove);
                    break;
                    }
                }
            } catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}