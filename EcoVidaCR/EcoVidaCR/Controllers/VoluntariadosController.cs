using EcoVidaCR.Data;
using EcoVidaCR.Models;
using Microsoft.AspNetCore.Mvc;


namespace EcoVidaCR.Controllers
{
    public class VoluntariadosController : Controller
    {
        //Variable que permite manejar la referencia del contexto 
        private readonly Contexto contexto;

        public VoluntariadosController(Contexto context)
        {
            contexto = context;
        }
        //Es el primer metodo que se ejecuta en el controlador
        public IActionResult Index()
        {
            return View(contexto.Voluntariados.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("nombreVoluntariado,descripcion,idDestino,correo,telefono,rutaURLimg")] Voluntariados voluntariados)
        {
            if (ModelState.IsValid)
            {
                contexto.Add(voluntariados);
                await contexto.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                return View(voluntariados);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var voluntariado = await contexto.Voluntariados.FindAsync(Id);

            if (voluntariado == null)
            {
                return NotFound();
            }

            return View(voluntariado);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, [Bind("idVoluntariado,nombreVoluntariado,descripcion,idDestino,correo,telefono,rutaURLimg")] Voluntariados voluntariado)
        {
            if (Id != voluntariado.idVoluntariado)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                contexto.Update(voluntariado);

                await contexto.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            else
            {
                return View(voluntariado);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var voluntariado = await contexto.Voluntariados.FindAsync(Id);

            if (voluntariado == null)
            {
                return NotFound();
            }
            else
            {
                return View(voluntariado);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Delete(int Id)
        {
            var voluntariado = await contexto.Voluntariados.FindAsync(Id);

            contexto.Voluntariados.Remove(voluntariado);

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

            var voluntariado = await contexto.Voluntariados.FindAsync(Id);

            if (voluntariado == null)
            {
                return NotFound();
            }

            return View(voluntariado);
        }
        [HttpGet]
        public async Task<IActionResult> Enviado(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var voluntariado = await contexto.Voluntariados.FindAsync(Id);

            EnviarEmail(voluntariado);

            return View();
        }
        private bool EnviarEmail(Voluntariados voluntario)
        {
            try
            {
                bool enviado = false;
                //gestiona la comunicación con el servidor de email
                Email email = new Email();

                var correo = User.Identity.Name;


                //utilizamos el método pero le enviamos los datos del usuario dentro de la variable User
                email.Enviar(correo, voluntario);

                //si todo salio bien,  true representa envio exitoso.
                enviado = true;

                //enviamos la variable bandera
                return enviado;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
