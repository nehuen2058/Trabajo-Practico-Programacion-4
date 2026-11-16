using Compras.data.Repositorios;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static Compras.data.Repositorios.UsuarioRepository;

public class LoginController : Controller
{
    private IUsuarioRepository _usuarioRepository;

    public LoginController(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> GoogleResponse()
    {
        var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        var claims = result.Principal.Identities.FirstOrDefault().Claims.Select(claim => new
        {
            claim.Issuer,
            claim.OriginalIssuer,
            claim.Type,
            claim.Value
        });
        // Si el usuario no esta en la tabla de usuarios, se da de alta
        var email = result.Principal.FindFirstValue(ClaimTypes.Email);
        var nombre = result.Principal.FindFirstValue(ClaimTypes.Name);
        var user = _usuarioRepository.ObtenerOCrearUsuario(email, nombre);

        List<String> listaPermisos = _usuarioRepository.ObtenerAcciones(email);


        // 3. Construir claims personalizados
        var claimsPersonalizadas = new List<Claim>(result.Principal.Claims)
            {
                new Claim("IdUsuario", user.IdUsuario.ToString() ?? "nuevo"),

            };

        foreach (var permiso in listaPermisos)
        {
            claimsPersonalizadas.Add(new Claim("Permiso", permiso));
        }

        var identity = new ClaimsIdentity(claimsPersonalizadas, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        // 4. Re-firmar la cookie con los claims nuevos
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal);


        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();

        return RedirectToAction("Index", "Home");
    }
}