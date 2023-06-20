using EcoVidaCR.Data;
using EcoVidaCR.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcoVidaCR.Controllers
{
    public class DestinosController : Controller
    {
        //Variable que permite manejar la referencia del contexto 
        private readonly Contexto contexto;

        public DestinosController(Contexto context)
        {
            contexto = context;
        }
        //Es el primer metodo que se ejecuta en el controlador
        public IActionResult Index(string Buscar)
        {
            var ListaP = contexto.Destinos.ToList();
            if (Buscar != null)
            {
                string CodBuscar = Buscar;
                var NewList = ListaP.Where(x => x.ubicacion == CodBuscar);
                ListaP = NewList.ToList();
            }

            return View(ListaP);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        /* 
           idDestino int not null primary key identity, 
           nombre varchar(500) not null,
           descripcion varchar(500) not null,
           ubicacion varchar(500) not null,
           aspectos varchar(500) not null
      */
        public async Task<IActionResult> Create([Bind("nombre,descripcion,ubicacion,aspectos,rutaURLimg")] Destinos destinos)
        {
            destinos.idDestino = 0;
            if (ModelState.IsValid)
            {
                contexto.Add(destinos);
                await contexto.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                return View(destinos);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var destinos = await contexto.Destinos.FindAsync(Id);

            if (destinos == null)
            {
                return NotFound();
            }

            return View(destinos);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, [Bind("idDestino, nombre,descripcion,ubicacion,aspectos,rutaURLimg")] Destinos destinos)
        {
            if (Id != destinos.idDestino)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                contexto.Update(destinos);

                await contexto.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            else
            {
                return View(destinos);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var destinos  = await contexto.Destinos.FindAsync(Id);

            if (destinos == null)
            {
                return NotFound();
            }
            else
            {
                return View(destinos);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Delete(int Id)
        {
            var des = await contexto.Destinos.FindAsync(Id);

            contexto.Destinos.Remove(des);

            await contexto.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var destinos = await contexto.Destinos.FindAsync(Id);

            if (destinos == null)
            {
                return NotFound();
            }

            return View(destinos);
        }
    }
}
