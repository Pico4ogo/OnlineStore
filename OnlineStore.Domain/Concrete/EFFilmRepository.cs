using System.Collections.Generic;
using OnlineStore.Domain.Entities;
using OnlineStore.Domain.Abstract;
using System.Web;

namespace OnlineStore.Domain.Concrete
{
    public class EFFilmRepository : IFilmRepository
    {
        EFDbContext context;

        public EFFilmRepository()
        {
            string mdfFilePath = HttpContext.Current.Server.MapPath("~/App_Data/OnlineStore.mdf");
            context = new EFDbContext(string.Format(@"Data Source=.\SQLEXPRESS;AttachDbFilename={0};Integrated Security=True;User Instance=True", mdfFilePath));
        }

        public IEnumerable<Film> Films
        {
            get { return context.Films; }
        }

        public void SaveFilm(Film film)
        {
            if (film.FilmId == 0)
                context.Films.Add(film);
            else
            {
                Film dbEntry = context.Films.Find(film.FilmId);
                if (dbEntry != null)
                {
                    dbEntry.Name = film.Name;
                    dbEntry.Description = film.Description;
                    dbEntry.Price = film.Price;
                    dbEntry.Category = film.Category;
                    dbEntry.ImageData = film.ImageData;
                    dbEntry.ImageMimeType = film.ImageMimeType;
                }
            }
            context.SaveChanges();
        }


        public Film DeleteFilm(int filmId)
        {
            Film dbEntry = context.Films.Find(filmId);
            if (dbEntry != null)
            {
                context.Films.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
