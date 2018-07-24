using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using OnlineStore.Domain.Abstract;

namespace OnlineStore.WebUI.Controllers
{
    public class NavController : Controller
    {
        private IFilmRepository repository;

        public NavController(IFilmRepository repo)
        {
            repository = repo;
        }

        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;

            IEnumerable<string> categories = repository.Films
                .Select(film => film.Category)
                .Distinct()
                .OrderBy(x => x);

            return PartialView("FlexMenu", categories);
        }
	}
}