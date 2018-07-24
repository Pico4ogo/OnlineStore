using System.Linq;
using System.Web.Mvc;
using OnlineStore.Domain.Entities;
using OnlineStore.Domain.Abstract;
using OnlineStore.WebUI.Models;

namespace OnlineStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IFilmRepository repository;
        private IOrderProcessor orderProcessor;

        public CartController(IFilmRepository repo, IOrderProcessor processor)
        {
            repository = repo;
            orderProcessor = processor;
        }

        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Ваша корзина пуста!");
            }

            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        public RedirectToRouteResult AddToCart(Cart cart, int filmId, string returnUrl)
        {
            Film film = repository.Films
                .FirstOrDefault(g => g.FilmId == filmId);

            if (film != null)
            {
                cart.AddItem(film, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int filmId, string returnUrl)
        {
            Film film = repository.Films
                .FirstOrDefault(g => g.FilmId == filmId);

            if (film != null)
            {
                cart.RemoveLine(film);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
	}
}