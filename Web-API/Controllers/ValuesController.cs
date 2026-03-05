// using common.Helpers.Enums;
// using common.WebApi;
// using common.WebApi.RoutingConfiguration;
// using domain.Entities;
// using Microsoft.AspNetCore.Mvc;
// using System;
// using System.Collections.Generic;
// using System.Threading.Tasks;
//
// namespace one_money_web_server.Controllers
// {
//     [AssignControllerRoute(WebApiEnvironment.Current, WebApiVersion.ApiVersion1, "value")]
//     public sealed class ValuesController : Controller
//     {
//         [HttpGet]
//         [AssignActionRoute("get")]
//         public async Task<IActionResult> GetValue(string payload)
//         {
//             List<Account> accounts = new List<Account>();
//             accounts.Add(new Account { Name = "Credit card", Amount = 1000, AccountType = AccountType.Account });
//             accounts.Add(new Account { Name = "Debit card", Amount = 5000, AccountType = AccountType.Account });
//             accounts.Add(new Account { Name = "Saving box", Amount = 1500, AccountType = AccountType.Saving });
//
//             try
//             {
//                 return Ok(accounts);
//             }
//             catch (Exception)
//             {
//                 return BadRequest("Bad request");
//             }
//         }
//     }
// }
