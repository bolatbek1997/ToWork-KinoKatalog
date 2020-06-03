using System.Data.Entity;

namespace KendoUIApp2.Models
{
    public partial class FilmContext : DbContext
    {
        public FilmContext()
            : base("name=Task")
        {

        }
        public virtual DbSet<Tasks> Tasks { get; set; }
        public virtual DbSet<Film> Films { get; set; }
        public virtual DbSet<UserModel> Users { get; set; }
       
    }
}
