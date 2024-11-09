using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormsProductApp.Models
{
    public class ProductViewModel
    {
        public List<Product> Products { get; set; } = null!;

        public List<Categorty> Categorties {get; set;} = null!;

        public string SelectedCategory { get; set; } = null!;
    }
}