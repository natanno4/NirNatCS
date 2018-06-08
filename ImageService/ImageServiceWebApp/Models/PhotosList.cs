using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace ImageServiceWebApp.Models
{
    public class PhotosList
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "PhotoPath")]
        public string PhotoPath { get; set; }

        [Required]
        [Display(Name = "ListPhotos")]
        private List<Photo> ListPhotos = new List<Photo>();

      
        public int NumOfPhotos()
        {
            return this.ListPhotos.Count;
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