using System.Collections.Generic;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Domain.Abstract
{
    public interface IFilmRepository
    {
        IEnumerable<Film> Films { get; }
        void SaveFilm(Film film);
        Film DeleteFilm(int filmId);
    }
}
