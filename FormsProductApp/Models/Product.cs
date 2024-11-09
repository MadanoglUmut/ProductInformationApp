using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FormsProductApp.Models
{
    public class Product
    {
        [Display(Name="Urun Id")]
        public int ProductId { get; set; }  

        [Display(Name ="Urun Adi")]
        [Required]
        public string? Name { get; set; }
        
        [Display(Name ="Urun Fiyati")]
        [Required(ErrorMessage ="Gerekli Alan")]
        [Range(0,999999)]
        public int? Price { get; set; }  
        
        [Display(Name="Urun Resmi")]
        public string? Image { get; set; }  

        [Display(Name="Mecut Mu")]
        public bool IsActive { get; set; }  
        
        [Display (Name ="Kategori")]
        [Required]
        public int? CategoryId { get; set; } 


    }
}