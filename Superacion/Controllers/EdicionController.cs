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
    public class EdicionController : Controller
    {
        private readonly ItlaTwitter4Context _context;
        private readonly IMapper _mapper;
        private DateTime fecha = DateTime.Now;

        public EdicionController(ItlaTwitter4Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IActionResult> EditPublic(int? Id)
        {
            var user = HttpContext.Session.GetString("UserNameLogueado");
            if (string.IsNullOrEmpty(user))
            {

                return RedirectToAction("Restringido", "Principal");
            }

            if (Id == null)
            {
                return NotFound();
            }

            var publicacion = _context.Publicacion.FirstOrDefault(x => x.IdPublicacion == Id);

            if (publicacion == null)
            {
                return NotFound();
            }


            return View(publicacion);
        }

        [HttpPost]
        public async Task<IActionResult> EditPublic(int? Id, Publicacion pa)
        {


            pa.Fecha = fecha;
            if (Id != pa.IdPublicacion)
            {

                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(pa);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Central");
            }

            return View(pa);
        }

        public async Task<IActionResult> DeletePublic(int? Id)
        {
            var user = HttpContext.Session.GetString("UserNameLogueado");
            if (string.IsNullOrEmpty(user))
            {

                return RedirectToAction("Restringido", "Principal");
            }

            if (Id == null)
            {
                return NotFound();
            }

            var publicacion = _context.Publicacion.FirstOrDefault(x => x.IdPublicacion == Id);

            if (publicacion == null)
            {
                return NotFound();
            }


            return View(publicacion);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(int IdPublicacion)
        {
            
                var publicacion = _context.Publicacion.FirstOrDefault(x => x.IdPublicacion == IdPublicacion);

                if (publicacion == null)
                {
                    return NotFound();
                }


            _context.Database.ExecuteSqlCommand("DELETE FROM Comentario WHERE IdPublicacion =" + IdPublicacion + ";");
            _context.Database.ExecuteSqlCommand("DELETE FROM Publicacion WHERE IdPublicacion ="+IdPublicacion+";");

              return RedirectToAction("Index", "Central");

           }        
           

        




    }

}