using System.Collections.Generic;
using System.Linq;

namespace OnlineStore.Domain.Entities
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public void AddItem(Film film, int quantity)
        {
            CartLine line = lineCollection
                .Where(g => g.Film.FilmId == film.FilmId)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Film = film,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(Film film)
        {
            lineCollection.RemoveAll(l => l.Film.FilmId == film.FilmId);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Film.Price * e.Quantity);

        }
        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }
    }

    public class CartLine
    {
        public Film Film { get; set; }
        public int Quantity { get; set; }
    }
}
