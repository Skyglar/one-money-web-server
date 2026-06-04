using Duende.IdentityServer.Services;
using Identity.API.Models;
using Identity.API.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers;

[AllowAnonymous]
public sealed class AccountController : Controller
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IIdentityServerInteractionService _interaction;
    private readonly ILogger<AccountController> _logger;

    public AccountController(
        SignInManager<ApplicationUser> signInManager,
        IIdentityServerInteractionService interaction,
        ILogger<AccountController> logger)
    {
        _signInManager = signInManager;
        _interaction = interaction;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Login(string? returnUrl)
    {
        var context = await _interaction.GetAuthorizationContextAsync(returnUrl);
        if (context is null && !string.IsNullOrEmpty(returnUrl) && !Url.IsLocalUrl(returnUrl))
            return BadRequest("Invalid return URL.");

        return View(new LoginInputModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginInputModel model, string? returnUrl)
    {
        var context = await _interaction.GetAuthorizationContextAsync(returnUrl);
        if (context is null && !string.IsNullOrEmpty(returnUrl) && !Url.IsLocalUrl(returnUrl))
            return BadRequest("Invalid return URL.");

        if (!ModelState.IsValid)
            return View(model);

        var result = await _signInManager.PasswordSignInAsync(
            model.Email,
            model.Password,
            model.RememberLogin,
            lockoutOnFailure: true);

        if (result.Succeeded)
        {
            if (!string.IsNullOrEmpty(returnUrl))
                return Redirect(returnUrl);

            return Redirect("~/");
        }

        ModelState.AddModelError(string.Empty, "Invalid email or password.");
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Logout(string? logoutId)
    {
        var context = await _interaction.GetLogoutContextAsync(logoutId);
        await _signInManager.SignOutAsync();
        _logger.LogInformation("User signed out.");

        if (!string.IsNullOrEmpty(context?.PostLogoutRedirectUri))
            return Redirect(context.PostLogoutRedirectUri);

        return Redirect("~/");
    }
}
