using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KendoUIApp2.Models
{
    [Table("UserModel")]
    public class UserModel
    {
        [Key]
        [Column(Order = 1)]
        public int Id { get; set; }
        [Key]
        [Column(Order = 2)]
        public string Login { get; set; }
        public string Password { get; set; }
        public static UserModel GetUserByLogin(string login)
        {
            var db = new FilmContext();
            try
            {
                return db.Users.FirstOrDefault(x => x.Login == login);
            }
            catch (Exception ex)
            {
                throw ex;
            }             
        }
    }
   
}