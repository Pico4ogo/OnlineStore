using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineStore.Domain.Abstract;
using OnlineStore.Domain.Entities;
using OnlineStore.WebUI.Models;

namespace OnlineStore.WebUI.Controllers
{
    public class FilmController : Controller
    {
        private IFilmRepository repository;
        public int pageSize = 4;

        public FilmController(IFilmRepository repo)
        {
            repository = repo;
        }

        public ViewResult List(string category, int page = 1)
        {
            FilmsListViewModel model = new FilmsListViewModel
            {
                Films = repository.Films
                    .Where(p => category == null || p.Category == category)
                    .OrderBy(film => film.FilmId)
                    .Skip((page - 1)*pageSize)
                    .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = category == null ?
                        repository.Films.Count() :
                        repository.Films.Where(film => film.Category == category).Count()
                },
                CurrentCategory = category
            };
            return View(model);
        }

        public FileContentResult GetImage(int filmId)
        {
            Film film = repository.Films
                .FirstOrDefault(g => g.FilmId == filmId);

            if (film != null)
            {
                return File(film.ImageData, film.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
	}
}