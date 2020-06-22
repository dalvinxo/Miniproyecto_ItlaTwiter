using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Superacion.Models;
using Superacion.ViewModels;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;
using System.Text;
using ItlaTwitter.Action;

namespace Superacion.Controllers
{
    public class PrincipalController : Controller
    {

        private readonly ItlaTwitter4Context _context;
        private readonly IMapper _mapper;

        public PrincipalController(ItlaTwitter4Context context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        //GET:
        public async Task<IActionResult> Index()
        {

            var user = HttpContext.Session.GetString("UserNameLogueado");
            if (!string.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Central");
            }


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginUsuarioViewModels login)
        {
          
                if (ModelState.IsValid)
                {
                    
                    var UsuarioLogeado = _mapper.Map<Usuario>(login);

                    Usuario InstanciaUser = new Usuario();
                     InstanciaUser = UsuarioLogeado;

                   // var autentificacion = await _context.Usuario.FirstOrDefaultAsync(c => c.Usuario1 ==login.Usuario &&  c.Clave == login.Clave);
                     var autentificacion = await _context.Usuario.FirstOrDefaultAsync(c => c.Usuario1 ==InstanciaUser.Usuario1 &&  c.Clave == InstanciaUser.Clave);

                    if (autentificacion != null)
                    {

                    if (autentificacion.Estado == "desactivado")
                    {
                        GestionEmail cuenta = new GestionEmail();
                        cuenta.sendEmailAccount(autentificacion.Correo, autentificacion.Usuario1, autentificacion.IdUsuario);
                        return RedirectToAction("VerificacionEmail", new { NameUsuario = autentificacion.Usuario1, Numero = 3, Correo = autentificacion.Correo });

                    }
                    else {

                        HttpContext.Session.SetInt32("IdUsuarioLogueado", autentificacion.IdUsuario);
                        HttpContext.Session.SetString("UserNameLogueado", autentificacion.Usuario1);


                        return RedirectToAction("Index", "Central", InstanciaUser);
                    }
                  
                    }
                    
                    ModelState.AddModelError("UserError","El usuario y contraseña no coinciden.");                   

                    return View(login);
                }
                return View(login);

        }
        
        public async Task<IActionResult> Signup()
        { 
            var user = HttpContext.Session.GetString("UserNameLogueado");
            if (!string.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Central");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signup(RegistroUsuarioViewModel NewUser)
        {

            try
            {

                if (ModelState.IsValid)
                {
                    var Usuario = _mapper.Map<Usuario>(NewUser);
                    _context.Usuario.Add(Usuario);
                  await _context.SaveChangesAsync();
                    GestionEmail cuenta = new GestionEmail();
                    cuenta.sendEmailAccount(Usuario.Correo, Usuario.Usuario1, Usuario.IdUsuario);
                    return RedirectToAction("VerificacionEmail", new { NameUsuario = Usuario.Usuario1, Numero = 3, Correo = Usuario.Correo });

                }
                return View(NewUser);

            }
            catch(Exception ex)
            {
                return RedirectToAction("Error");

            }
            
        }
        
        //Get
        public IActionResult Restringido(){ return View(); }

        [HttpPost]
        public  async Task<ActionResult> Restores(string NameUsuario)
        {
            var verificacion = await _context.Usuario.FirstOrDefaultAsync(x => x.Usuario1 == NameUsuario);
            string Correo = "";

            if (verificacion == null)
            {
               
                return RedirectToAction("VerificacionEmail", new {NameUsuario = NameUsuario, Numero = 1, Correo = Correo});
            }
            
                GestionEmail Email = new GestionEmail();
                String Clave = Email.randomClave(6);

            //Enviar Correo

                Correo = verificacion.Correo;
                Email.sendEmail(Correo, NameUsuario, Clave);
                 return RedirectToAction("VerificacionEmail", new { NameUsuario = NameUsuario, Numero = 2, Correo = Correo });
            
        }

        public async Task<ActionResult> VerificacionEmail( string NameUsuario, int Numero, string Correo) {
            
            var user = HttpContext.Session.GetString("UserNameLogueado");
            if (!string.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Central");
            }
            
            ViewBag.Name = NameUsuario;
            ViewBag.Numero = Numero;
            ViewBag.Correo = Correo;
            return View();
        }

        public async Task<ActionResult> ConfirmAccount(int? Id) {

            var UsuarioAcount = await _context.Usuario.FirstOrDefaultAsync(x=>x.IdUsuario == Id);

            if (UsuarioAcount == null) {

                return NotFound();
            }

            _context.Database.ExecuteSqlCommand("UPDATE Usuario SET Estado = 'activado' WHERE IdUsuario ="+UsuarioAcount.IdUsuario+";");


            return RedirectToAction("Index");
        }

    }
}