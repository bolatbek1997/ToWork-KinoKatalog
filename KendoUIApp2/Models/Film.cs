using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KendoUIApp2.Models
{
   // [Bind(Exclude = "ID")]
    public class Film
    {
        //название, описание, год выпуска, режиссёр, пользователь, который выложил информацию, постер. Постер - это файл-изображение.
        // [ScaffoldColumn(false)]
        [Key]
        public int Id { get; set; }
     
        public string Name { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public string Producer { get; set; }
        public byte[] Poster { get; set; }
      //  [ForeignKey("UserModel")]
        public int? UserId { get; set; }
        public virtual UserModel User { get; set; }
        //public string ImageSource
        //{
        //    get
        //    {
        //        string mimeType = "image/png";
        //        string base64 = Poster!=null?Convert.ToBase64String(Poster):null;
        //        return string.Format("data:{0};base64,{1}", mimeType, base64);
        //    }
        //}
    }
}