using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;


using Petalos.Models;
using Petalos.Areas.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace Petalos.Controllers
{
    [Area("Admin")]
    public class FloresController : Controller
    {
        public floresContext Context { get; }
        public IWebHostEnvironment Host { get; }

        public FloresController(floresContext context, IWebHostEnvironment host)
        {
            Context = context;
            Host = host;
        }
        public IActionResult Index()
        {
            var TodasLasFloresParaAdministrar = Context.Datosflores.OrderBy(x => x.Nombre);
            return View(TodasLasFloresParaAdministrar);
        }
        [HttpGet]
        public IActionResult AgregarFlor()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AgregarFlor(FloresCrud vm)
        {
            if (string.IsNullOrWhiteSpace(vm.Flores.Nombre))
            {
                ModelState.AddModelError("", "El nombre de la flor está vacío");
                return View(vm);
            }
            if (string.IsNullOrWhiteSpace(vm.Flores.Nombrecientifico))
            {
                ModelState.AddModelError("", "El nombre cientifico de la flor está vacío");
                return View(vm);
            }
            if (string.IsNullOrWhiteSpace(vm.Flores.Origen))
            {
                ModelState.AddModelError("", "El origen de la flor está vacío");
                return View(vm);
            }
            if (string.IsNullOrWhiteSpace(vm.Flores.Descripcion))
            {
                ModelState.AddModelError("", "La descripción de la flor está vacía");
                return View(vm);
            }
            if (Context.Datosflores.Any(x => x.Nombre == vm.Flores.Nombre))
            {
                ModelState.AddModelError("", "El nombre de la flor ya existe");
                return View(vm);
            }
            if (Context.Datosflores.Any(x => x.Nombrecientifico == vm.Flores.Nombrecientifico))
            {
                ModelState.AddModelError("", "El nombre científico de la flor ya existe");
                return View(vm);
            }
            if (Context.Datosflores.Any(x => x.Nombrecomun == vm.Flores.Nombrecomun))
            {
                ModelState.AddModelError("", "El nombre común de la flor ya existe");
                return View(vm);
            }
            Context.Add(vm.Flores);
            Context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AgregarImagenes(int id)
        {
            
            var fotoAAgregarMedianteElId = Context.Datosflores.Include(x => x.Imagenesflores).FirstOrDefault(x => x.Idflor == id);
            if (fotoAAgregarMedianteElId == null)
            {
                return RedirectToAction("Index");
            }
            FloresCrud vm = new();
            vm.ImagenesMedianteEl_ID = Context.Imagenesflores.FirstOrDefault(x => x.Idflor == fotoAAgregarMedianteElId.Idflor);
            vm.Flores = fotoAAgregarMedianteElId;
            vm.ListaFlores = Context.Datosflores.OrderBy(x => x.Nombre);
          
            return View(vm);

             
          
        }
        [HttpPost]
        public IActionResult AgregarImagenes(FloresCrud vm,IFormFile foto)
        {
            if (foto==null)
            {
              
               
               
                vm.ListaFlores = Context.Datosflores.OrderBy(x => x.Nombre);
                ModelState.AddModelError("", "No hay ninguna fotografia");
                return View(vm);
            }
            if (foto != null)
            {
                if (foto.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("", "Solo se permite la carga de archivos JPG");
                    return View(vm);

                }
                if (foto.Length > 1024 * 1024 * 5)
                {
                    ModelState.AddModelError("", "No se permite la carga de archivos mayores a 5MB");
                    return View(vm);


                }
            }
            vm.ImagenesMedianteEl_ID.Nombreimagen = foto.FileName;
            Context.Add(vm.ImagenesMedianteEl_ID);
           
            Context.SaveChanges();
            if (foto != null)
            {
                String var = "Hola Mundo";
                int tam_var = var.Length;
                String Var_Sub = var.Substring((tam_var - 2), 2);
                //if (foto.)
                //{

                //}
                var path = Host.WebRootPath + "/images/" + vm.ImagenesMedianteEl_ID.Nombreimagen /*+ ".jpg"*/;
                FileStream fs = new FileStream(path, FileMode.Create);
                foto.CopyTo(fs);
                fs.Close();
            }
            return RedirectToAction("AgregarImagenes");
          
        }
        [HttpGet]
        public IActionResult EditarFlor(int id)
        {
            var f = Context.Datosflores.FirstOrDefault(x => x.Idflor == id);
            if (f == null)
            {
                return RedirectToAction("Index");
            }

            FloresCrud vm = new FloresCrud
            {
                Flores = f
            };

            return View(vm);

        }
        [HttpPost]
        public IActionResult EditarFlor(FloresCrud vm)
        {

            var f = Context.Datosflores.FirstOrDefault(x => x.Idflor == vm.Flores.Idflor);
            if (f == null)
            {
                return RedirectToAction("Index");
            }
            if (string.IsNullOrWhiteSpace(vm.Flores.Nombre))
            {
                ModelState.AddModelError("", "El nombre de la flor está vacío");
                return View(vm);
            }
            if (string.IsNullOrWhiteSpace(vm.Flores.Nombrecientifico))
            {
                ModelState.AddModelError("", "El nombre cientifico de la flor está vacío");
                return View(vm);
            }
            if (string.IsNullOrWhiteSpace(vm.Flores.Origen))
            {
                ModelState.AddModelError("", "El origen de la flor está vacío");
                return View(vm);
            }
            if (string.IsNullOrWhiteSpace(vm.Flores.Descripcion))
            {
                ModelState.AddModelError("", "La descripción de la flor está vacía");
                return View(vm);
            }
            if (Context.Datosflores.Any(x => x.Nombre == vm.Flores.Nombre && x.Idflor != vm.Flores.Idflor))
            {
                ModelState.AddModelError("", "El nombre de la flor ya existe");
                return View(vm);
            }
            if (Context.Datosflores.Any(x => x.Nombrecientifico == vm.Flores.Nombrecientifico && x.Idflor != vm.Flores.Idflor))
            {
                ModelState.AddModelError("", "El nombre científico de la flor ya existe");
                return View(vm);
            }
            if (Context.Datosflores.Any(x => x.Nombrecomun == vm.Flores.Nombrecomun && x.Idflor != vm.Flores.Idflor))
            {
                ModelState.AddModelError("", "El nombre común de la flor ya existe");
                return View(vm);
            }
            f.Nombrecientifico = vm.Flores.Nombrecientifico;
            f.Nombrecomun = vm.Flores.Nombrecomun;
            f.Origen = vm.Flores.Origen;
            f.Descripcion = vm.Flores.Descripcion;
            f.Nombre = vm.Flores.Nombre;
            Context.Update(f);
            Context.SaveChanges();
            return RedirectToAction("Index");

        }
        [HttpGet]
        public IActionResult EliminarFlor(int id)
        {
            var f = Context.Datosflores.FirstOrDefault(x => x.Idflor == id);
            if (f == null)
            {
                return RedirectToAction("Index");
            }
            return View(f);
        }
        [HttpGet]
        public IActionResult EliminarImagen(int id)
        {
            var img = Context.Imagenesflores.FirstOrDefault(x => x.Idimagen == id);
            if (img == null)
            {
                return RedirectToAction("Index");
            }
            return View(img);
        }
        [HttpPost]
        public IActionResult EliminarImagen(Imagenesflore imagenF)
        {
            var img = Context.Imagenesflores.FirstOrDefault(x => x.Idimagen == imagenF.Idimagen);

            if (img == null)
            {
                ModelState.AddModelError("", $"La fotografía {imagenF.Nombreimagen} ha sido eliminada, o no existe");
                return View(imagenF);
            }
            Context.Remove(img);
            Context.SaveChanges();
            string path = Host.WebRootPath + "/images/" + img.Nombreimagen;
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult EliminarFlor(Datosflore flor)
        {
            var f = Context.Datosflores.FirstOrDefault(x => x.Idflor == flor.Idflor);

            if (f == null)
            {
                ModelState.AddModelError("", "La flor ya ha sido eliminada o no existe");
                return View(flor);
            }
            Context.Remove(f);
            Context.SaveChanges();

            return RedirectToAction("Index");
        }

    }


}