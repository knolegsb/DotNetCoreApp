using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DotNetCoreApp.Data;
using DotNetCoreApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetCoreApp.Controllers
{
    [Route("api/[controller]")]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true, Duration = -1)]
    public class ProductController : Controller
    {
        private ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public IEnumerable<dynamic> Get()
        {
            return _context.Products.ToList();
        }

        [HttpPost]
        public string Post([FromBody] Product product)
        {
            Response.StatusCode = 200;

            try
            {
                Product newProduct = new Product();
                newProduct.Name = product.Name;
                newProduct.Description = product.Description;
                _context.Products.Add(newProduct);
                _context.SaveChanges();
            }
            catch(Exception ex)
            {
                Response.StatusCode = 400;
                return ex.ToString();
            }
            return "OK";
        }

        [HttpPut]
        public string Put([FromBody] Product product)
        {
            Response.StatusCode = 200;

            try
            {
                product.Name = product.Name;
                product.Description = product.Description;
                _context.Products.Attach(product);
                _context.Entry(product).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return ex.ToString();
            }
            return "OK";
        }

        [HttpDelete]
        public string Delete(int id)
        {
            Response.StatusCode = 200;

            try
            {
                Product newProduct = new Models.Product();
                newProduct.Id = id;
                _context.Products.Remove(newProduct);
                _context.SaveChanges();
            }
            catch(Exception ex)
            {
                return ex.ToString();
            }
            return "OK";
        }
    }
}