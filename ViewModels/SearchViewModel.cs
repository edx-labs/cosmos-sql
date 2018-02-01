using RealEstateCatalog.Models;
using System.Collections.Generic;

namespace RealEstateCatalog.ViewModels
{
    public class SearchViewModel
    {
        public string Realtor { get; set; }

        public IList<Home> Homes { get; set; }
    }
}