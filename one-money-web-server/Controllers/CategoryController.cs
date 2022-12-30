using common.WebApi.RoutingConfiguration;
using common.WebApi;
using Microsoft.AspNetCore.Mvc;
using common.Helpers.Enums;
using domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using common.Helpers;
using System.IO;

namespace one_money_web_server.Controllers
{
    [AssignControllerRoute(WebApiEnvironment.Current, WebApiVersion.ApiVersion1, "category")]
    public sealed class CategoryController : Controller
    {
        [HttpGet]
        [AssignActionRoute("get")]
        public async Task<IActionResult> GetCategories(string payload)
        {

            List<Category> categories = new List<Category>();

            categories.Add(new Category {
                Name = "Food", 
                Image = FolderManager.Convert(Path.Combine(FolderManager.GetCategoryFolder(), "shopping-cart.svg"), $"{Request.Scheme}://{Request.Host.Value}"), 
                Color = "green", 
                Amount = 2453.98 
            });
            categories.Add(new Category { 
                Name = "Gifts", 
                Image = FolderManager.Convert(Path.Combine(FolderManager.GetCategoryFolder(), "gift.svg"), $"{Request.Scheme}://{Request.Host.Value}"), 
                Color = "orange", 
                Amount = 3000.00 
            });
            categories.Add(new Category { 
                Name = "Taxes", 
                Image = FolderManager.Convert(Path.Combine(FolderManager.GetCategoryFolder(), "balance.svg"), $"{Request.Scheme}://{Request.Host.Value}"), 
                Color = "blue", 
                Amount = 534.50 
            });

            try
            {
                return Ok(categories);
            }
            catch (Exception)
            {
                return BadRequest("Bad request");
            }
        }
    }
}
