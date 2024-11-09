using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FormsProductApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FormsProductApp.Controllers;

public class HomeController : Controller
{


    public HomeController()
    {

    }

    public IActionResult Index(string searchString,string category)
    {
        var products = Repository.Products;
        if(!String.IsNullOrEmpty(searchString)){
            ViewBag.SearchString = searchString;
            products = products = products.Where(p => p.Name?.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
        }

        if(!String.IsNullOrEmpty(category) && category != "0"){
            products = products.Where(p=>p.CategoryId == int.Parse(category)).ToList();
        }

        //ViewBag.Categorties = new SelectList(Repository.Categorties,"CategoryId","CategortyName",category).ToList();
        var model = new ProductViewModel{
            Products = products,
            Categorties = Repository.Categorties,
            SelectedCategory = category
        };
        return View(model);
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Select = new SelectList(Repository.Categorties,"CategoryId","CategortyName").ToList();
        return View();
    }

    [HttpPost]
    public async Task <IActionResult> Create(Product model, IFormFile imageFile)
    {     
        var extantion = "";
        if(imageFile != null){
            var allowedExtantion = new[] {".jpeg",".jpg",".png"};
            extantion = Path.GetExtension(imageFile.FileName);   
            if(!allowedExtantion.Contains(extantion)){
                ModelState.AddModelError("", "Geçerli bir resim seçiniz");
            }

        }
        
        if(ModelState.IsValid){
            if(imageFile != null){
                var randomFileName = string.Format($"{Guid.NewGuid()}{extantion}");
                var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/img",randomFileName);
                using(var stream = new FileStream(path,FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
                model.Image = randomFileName;
                model.ProductId = Repository.Products.Count+1;
                Repository.AddProdutct(model);

            }
        return RedirectToAction("Index");
        }
        else
        {
        ViewBag.Select = new SelectList(Repository.Categorties,"CategoryId","CategortyName").ToList();
        return View(model);
        }
    }

    public IActionResult Edit(int? id){
        if(id == null){
            return NotFound();
        }

        var entitiy = Repository.Products.FirstOrDefault(p=> p.ProductId == id);
        if(entitiy == null){
            return NotFound();
        }

        ViewBag.Select = new SelectList(Repository.Categorties,"CategoryId","CategortyName").ToList();
        return View(entitiy);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int? id, Product model, IFormFile? imageFile){

        if(id != model.ProductId){
            NotFound();
        }

        if(ModelState.IsValid){
            if(imageFile != null)
            {
            var extantion = Path.GetExtension(imageFile.FileName);
            var randomFileName = string.Format($"{Guid.NewGuid()}{extantion}");
            var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/img",randomFileName);
            using(var stream = new FileStream(path,FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }
            model.Image = randomFileName;
            }

            Repository.UpdateProduct(model);
            return RedirectToAction("Index");


        }

        ViewBag.Select = new SelectList(Repository.Categorties,"CategoryId","CategortyName").ToList();
        return View(model);
    }

    public IActionResult Delete(int? id){
        if(id == null){
            return NotFound();

        }

        var entitiy = Repository.Products.FirstOrDefault(p=>p.ProductId == id);
        if(entitiy == null){
            return NotFound();

        }
        Repository.Delete(entitiy);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult EditProductss(List<Product> products){
        foreach(var x in products){
            Repository.UpdateIsActive(x);
        }
        return RedirectToAction("Index");
    }

/*
    [HttpPost]
    public IActionResult Delete(int id, int ProductId){
        if(id != ProductId){
            NotFound();
        }
        
        var entitiy = Repository.Products.FirstOrDefault(p=>p.ProductId == ProductId);
        if(entitiy == null){
            NotFound();
        }

        Repository.Delete(entitiy);
        return RedirectToAction("Index");

    }
*/
}
