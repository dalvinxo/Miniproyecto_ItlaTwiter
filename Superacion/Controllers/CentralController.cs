using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Superacion.Models;
using Superacion.ViewModels;

namespace Superacion.Controllers
{
    public class CentralController : Controller
    {
        private DateTime fecha = DateTime.Now;
        private readonly ItlaTwitter4Context _context;
        private readonly IMapper _mapper;

        public CentralController(ItlaTwitter4Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            //UsuarioClient = InstanciaUsuario;
            var Id = HttpContext.Session.GetInt32("IdUsuarioLogueado");
            var user = HttpContext.Session.GetString("UserNameLogueado");
            if (string.IsNullOrEmpty(user)) {

                return RedirectToAction("Restringido", "Principal");
            }

            var publica1 = await _context.PublicacionViewModels.FromSql("GetHomePublicacion @IdUsuario = {0}", Id).ToListAsync();

            var coment1 = await _context.comentarioViewModels.FromSql("GetCommentarioALL").ToListAsync();


            PublicacionViewModel plb = new PublicacionViewModel();
            plb.Public = publica1;
            plb.Coment = coment1;

            return View(plb);
        }

        [HttpPost]
        public async Task<IActionResult> Publicar(PublicacionViewModel pb) {

            
            pb.Fecha = fecha;

            if (ModelState.IsValid)
            {
                var Publicacion = _mapper.Map<Publicacion>(pb);
                _context.Publicacion.Add(Publicacion);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View("Friends");
            
        }

        [HttpPost]
        public async Task<ActionResult> Comentar(int IdPublicacion, int aplicacion,string Comentario1)
        {
              try
             {

             var Id = HttpContext.Session.GetInt32("IdUsuarioLogueado");
                ComentarioViewModels coment = new ComentarioViewModels()
            {
               
                Comentario1 = Comentario1,
                IdUsuario =Id,
                IdPublicacion = IdPublicacion,
                Fecha = fecha
            };


                if (ModelState.IsValid)
                {
                    var ComentarioPublicado = _mapper.Map<Comentario>(coment);
                    _context.Comentario.Add(ComentarioPublicado);
                    if (aplicacion == 1)
                    {
                        await _context.SaveChangesAsync();
                        return RedirectToAction("Index");
                    } else {
                        await _context.SaveChangesAsync();
                        return RedirectToAction("Friends");

                    }

                }

                return RedirectToAction("Index");

            }
            catch
            {
               return RedirectToAction("Error");

            }

        }

        public async Task<IActionResult> Logout()
        {

            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Principal");
        }

        public async Task<IActionResult> Friends() {
            var Id = HttpContext.Session.GetInt32("IdUsuarioLogueado");
            var user = HttpContext.Session.GetString("UserNameLogueado");
            if (string.IsNullOrEmpty(user))
            {

                return RedirectToAction("Restringido", "Principal");
            }
            
            var ConsultaAmigo = await _context.publicacionAmigoViewModels.FromSql("GetFriendPublicacion @IdUsuario = {0}", Id).ToListAsync();
            var coment1 = await _context.comentarioViewModels.FromSql("GetCommentarioALL").ToListAsync();
           var Amigos= await _context.amigoViewModels.FromSql("GetAllFriends @IdUsuario={0}", Id).ToListAsync();

            PublicacionViewModel pb = new PublicacionViewModel();
            pb.AmigosPublic = ConsultaAmigo;
            pb.Coment = coment1;
            pb.AmigosAdd = Amigos;

            return View(pb);
        }

        public async Task<IActionResult> EliminarAmigo(int IdAmigo, int IdUsuario) {
            try
            {
                _context.Database.ExecuteSqlCommand("DELETE FROM Amigo WHERE IdUsuario_A=" + IdUsuario + " and IdAmigo_B=" + IdAmigo + ";");

                return RedirectToAction("Friends");
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddFriends(string NameUsuario, int IdUsuario) {

             try
            {
                    var VerificacionAmigo = await _context.Usuario.FirstOrDefaultAsync(x=> x.Usuario1 == NameUsuario);

                    if (VerificacionAmigo == null)
                    {
                        return RedirectToAction("UsuarioNo", new { NameUsuario = NameUsuario });
                    }

                    AgregarAmigoViewModels NewAmigo = new AgregarAmigoViewModels()
                    {
                        IdUsuarioA = IdUsuario,
                        IdAmigoB = VerificacionAmigo.IdUsuario
                    };

                    if (ModelState.IsValid)
                    {

                        var amigo = _mapper.Map<Amigo>(NewAmigo);
                        _context.Amigo.Add(amigo);
                        await _context.SaveChangesAsync();
                        return RedirectToAction("Friends");
                    }
                         return RedirectToAction("Friends");

            }
            catch
            {

                return NotFound();

            }
            
        }

        public async Task<IActionResult> UsuarioNo(string NameUsuario) {
            var user = HttpContext.Session.GetString("UserNameLogueado");
            if (string.IsNullOrEmpty(user))
            {

                return RedirectToAction("Restringido", "Principal");
            }

            ViewBag.Name = NameUsuario;

            return View();
        }

    }
}