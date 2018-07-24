using System.Collections.Generic;
using OnlineStore.Domain.Entities;

namespace OnlineStore.WebUI.Models
{
    public class FilmsListViewModel
    {
        public IEnumerable<Film> Films { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}