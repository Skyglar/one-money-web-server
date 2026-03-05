// using common.WebApi.RoutingConfiguration;
// using common.WebApi;
// using Microsoft.AspNetCore.Mvc;
// using domain.Entities;
// using System.Collections.Generic;
// using System.Threading.Tasks;
// using System;
// using common.Helpers;
// using System.IO;
// using Microsoft.AspNetCore.Authorization;
//
// namespace one_money_web_server.Controllers {
//     [Authorize]
//     [AssignControllerRoute(WebApiEnvironment.Current, WebApiVersion.ApiVersion1, "category")]
//     public sealed class CategoryController : Controller
//     {
//         private readonly ICategoryService _categoryService;
//
//         public CategoryController(ICategoryService categoryService) {
//             _categoryService = categoryService;
//         }
//
//         [HttpGet]
//         [AssignActionRoute("get/static")]
//         public async Task<IActionResult> GetCategories(string payload)
//         {
//             List<Category> categories = new List<Category>();
//
//             categories.Add(new Category {
//                 Name = "Food", 
//                 Image = FolderManager.Convert(Path.Combine(FolderManager.GetCategoryFolder(), "shopping-cart.svg"), $"{Request.Scheme}://{Request.Host.Value}"), 
//                 Color = "green", 
//                 Amount = 2453.98 
//             });
//             categories.Add(new Category { 
//                 Name = "Gifts", 
//                 Image = FolderManager.Convert(Path.Combine(FolderManager.GetCategoryFolder(), "gift.svg"), $"{Request.Scheme}://{Request.Host.Value}"), 
//                 Color = "orange", 
//                 Amount = 3000.00 
//             });
//             categories.Add(new Category { 
//                 Name = "Taxes", 
//                 Image = FolderManager.Convert(Path.Combine(FolderManager.GetCategoryFolder(), "balance.svg"), $"{Request.Scheme}://{Request.Host.Value}"), 
//                 Color = "blue", 
//                 Amount = 534.50 
//             });
//             categories.Add(new Category {
//                 Name = "Travel",
//                 Image = FolderManager.Convert(Path.Combine(FolderManager.GetCategoryFolder(), "globe.svg"), $"{Request.Scheme}://{Request.Host.Value}"),
//                 Color = "#47a4f3",
//                 Amount = 22453.98
//             });
//             categories.Add(new Category {
//                 Name = "Shopping",
//                 Image = FolderManager.Convert(Path.Combine(FolderManager.GetCategoryFolder(), "cash-machine.svg"), $"{Request.Scheme}://{Request.Host.Value}"),
//                 Color = "#9e7eeb",
//                 Amount = 13000.00
//             });
//             categories.Add(new Category {
//                 Name = "Entertainment",
//                 Image = FolderManager.Convert(Path.Combine(FolderManager.GetCategoryFolder(), "star.svg"), $"{Request.Scheme}://{Request.Host.Value}"),
//                 Color = "#fafd41",
//                 Amount = 1534.50
//             });
//             categories.Add(new Category {
//                 Name = "Charity",
//                 Image = FolderManager.Convert(Path.Combine(FolderManager.GetCategoryFolder(), "shopping-cart.svg"), $"{Request.Scheme}://{Request.Host.Value}"),
//                 Color = "#074e8f",
//                 Amount = 2453.98
//             });
//             categories.Add(new Category {
//                 Name = "Health",
//                 Image = FolderManager.Convert(Path.Combine(FolderManager.GetCategoryFolder(), "gift.svg"), $"{Request.Scheme}://{Request.Host.Value}"),
//                 Color = "#9b2bd5",
//                 Amount = 3000.00
//             });
//             categories.Add(new Category {
//                 Name = "Other",
//                 Image = FolderManager.Convert(Path.Combine(FolderManager.GetCategoryFolder(), "balance.svg"), $"{Request.Scheme}://{Request.Host.Value}"),
//                 Color = "#0ce7aa",
//                 Amount = 534.50
//             });
//
//             try
//             {
//                 return Ok(categories);
//             }
//             catch (Exception)
//             {
//                 return BadRequest("Bad request");
//             }
//         }
//
//         [HttpPost]
//         [AssignActionRoute("new")]
//         public async Task<IActionResult> AddCategoryAsync([FromBody] Category category) {
//             try {
//                 return Ok(await _categoryService.AddCategoryAsync(category));
//             }
//             catch (Exception) {
//                 return BadRequest("Bad request");
//             }
//         }
//
//         [HttpGet]
//         [AssignActionRoute("all")]
//         public async Task<IActionResult> GetAllCategoriesAsync() {
//             try {
//                 return Ok(await _categoryService.GetAllCategoriesAsync());
//             }
//             catch (Exception) {
//                 return BadRequest("Bad request");
//             }
//         }
//
//         [HttpPatch]
//         [AssignActionRoute("edit")]
//         public async Task<IActionResult> UpdateCategoryAsync([FromBody] Category category) {
//             try {
//                 return Ok(await _categoryService.UpdateCategoryAsync(category));
//             } catch(Exception) {
//                 return BadRequest("Bad request");
//             }
//         }
//     }
// }
