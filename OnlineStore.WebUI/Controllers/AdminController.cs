using System.Web.Mvc;
using OnlineStore.Domain.Abstract;
using OnlineStore.Domain.Entities;
using System.Linq;
using System.Web;

namespace OnlineStore.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        IFilmRepository repository;

        public AdminController (IFilmRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            return View(repository.Films);
        }

        public ViewResult Edit(int filmId)
        {
            Film film = repository.Films
                .FirstOrDefault(g => g.FilmId == filmId);
            return View(film);
        }

        [HttpPost]
        public ActionResult Edit(Film film, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    film.ImageMimeType = image.ContentType;
                    film.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(film.ImageData, 0, image.ContentLength);
                }
                repository.SaveFilm(film);
                TempData["message"] = string.Format("Изменения в фильме \"{0}\" были сохранены", film.Name);
                return RedirectToAction("Index");
            }
            else
            {
                // Что-то не так со значениями данных
                return View(film);
            }
        }

        public ViewResult Create()
        {
            return View("Edit", new Film());
        }

        [HttpPost]
        public ActionResult Delete(int filmId)
        {
            Film deletedFilm = repository.DeleteFilm(filmId);
            if (deletedFilm != null)
            {
                TempData["message"] = string.Format("Фильм \"{0}\" был удален",
                    deletedFilm.Name);
            }
            return RedirectToAction("Index");
        }
	}
}