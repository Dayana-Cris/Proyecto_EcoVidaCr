using EcoVidaCR.Data;
using EcoVidaCR.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace EcoVidaCR.Controllers
{
    public class UsuariosController : Controller
    {
        //Variable que permite manejar la referencia del contexto 
        private readonly Contexto contexto;

        public UsuariosController(Contexto context)
        {
            contexto = context;
        }
        //Es el primer metodo que se ejecuta en el controlador
        public IActionResult Index()
        {
            return View(contexto.Usuarios.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        /// <summary>
        /// Ah este metodo solo va a tener acceso el administrador de la pagina
        /// y cada usuario que agregue manual mente el sera un administrador
        /// por lo que si la cuentas es para un cliente la debe de crear el mismo 
        /// cliente en le metodo CrearCuenta ()
        /// </summary>y
        /// /// <param name="usuarios"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("nombre, correo, password")] Usuarios user)
        {
            user.idUsuario = 0;
            user.rol = "Administrador";
            if (User != null)
            {
                //Se almacena el usuario
                this.contexto.Usuarios.Add(user);

                try
                {
                    //Se aplican los cambios
                    this.contexto.SaveChanges();

                    //Se crea un mensaje de informacion
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    //Ojo con el ex.Message se muestra el detalle tecnico del error
                    TempData["MensajeError"] = "Error, no se logro crear el usuario ";
                }

                //Se ubica al usuario en la View de Crear Cuenta
                return View();
            }
            else
            {
                //En caso de que no tenga datos mostramos el mensaje al usuario
                TempData["MensajeError"] = "No se logro crear el usuario";
                return View(user);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var usuario = await contexto.Usuarios.FindAsync(Id);

            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }
        /// <summary>
        /// El administrador va a poder modificar todos los datos de los usuarios registrados
        /// en el sistema menos el ID
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, [Bind("idUsuario, nombre, correo, password, rol")] Usuarios usuario)
        {
            if (Id != usuario.idUsuario)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                if(usuario.rol == "Administrador" || usuario.rol == "Cliente")
                {
                    contexto.Update(usuario);

                    await contexto.SaveChangesAsync();

                    return RedirectToAction("Index");
                }else
                { 
                    return View(usuario); 
                }
                
            }
            else
            {
                return View(usuario);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var usuario = await contexto.Usuarios.FindAsync(Id);

            if (usuario == null)
            {
                return NotFound();
            }
            else
            {
                return View(usuario);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Delete(int Id)
        {
            var usuario = await contexto.Usuarios.FindAsync(Id);

            contexto.Usuarios.Remove(usuario);

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

            var usuario = await contexto.Usuarios.FindAsync(Id);

            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Login([Bind] Usuarios usuario)
        {
            var temp = this.validarUsuarios(usuario);

            if (temp != null)
            {
                //De tener datos se crea identidad del usuario
                var userClaims = new List<Claim>() {
                    new Claim (ClaimTypes.Name, temp.correo),
                    new Claim(ClaimTypes.Role, temp.rol)

                };

                var grandmaIdentity = new ClaimsIdentity(userClaims, "User Identity");
                var userPrincipal = new ClaimsPrincipal(grandmaIdentity);

                //HttpContext encapsula informacion especifica de http sobre la solicitud del usuario
                //como un conjunto de notificaciones
                HttpContext.SignInAsync(userPrincipal);

                //Reubicamos al usuario en la pagina default
                return RedirectToAction("Index", "Home");
            }
            //Almacenamos un mensaje de error para mostrarlo a nivel de front-end
            TempData["Mensaje"] = "Error, usuario o contraseña incorrecto...";

            //Oj0 en caso que no exista informacion del usuario autenticado
            //enviamos el objeto nuevamente al front-end para que el usuario modifique los datos 
            return View(usuario);
        }
        private Usuarios validarUsuarios(Usuarios temp)
        {
            Usuarios autorizado = null;

            var user = this.contexto.Usuarios.FirstOrDefault(u => u.correo == temp.correo);

            if (user != null)
            {
                if (user.password.Equals(temp.password))
                {

                    autorizado = user;

                }
            }

            return autorizado;
        }

        public async Task<IActionResult> Logout()
        {
            //Ojo aqui se cierra la sesion
            await HttpContext.SignOutAsync();

            //Reubicamos al usuario en la pagina default 
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult CrearCuenta()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CrearCuenta([Bind("nombre, correo, password")] Usuarios user)
        {
            user.idUsuario = 0;
            user.rol = "Cliente";
            if (User != null)
            {


                //Se almacena el usuario
                this.contexto.Usuarios.Add(user);

                try
                {
                    //Se aplican los cambios
                    this.contexto.SaveChanges();

                    //Se crea un mensaje de informacion
                    return RedirectToAction("Index","Home");
                }
                catch (Exception ex)
                {
                    //Ojo con el ex.Message se muestra el detalle tecnico del error
                    TempData["MensajeError"] = "Error, no se logro crear el usuario";
                }

                //Se ubica al usuario en la View de Crear Cuenta
                return View();
            }
            else
            {
                //En caso de que no tenga datos mostramos el mensaje al usuario
                TempData["MensajeError"] = "No se logro crear el usuario";
                return View(user);
            }

        }

        [HttpGet]
        public async Task<IActionResult> EditCLient()
        {
            var usuario = this.contexto.Usuarios.FirstOrDefault(u => u.correo == User.Identity.Name);

            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }
        /// <summary>
        /// El administrador va a poder modificar todos los datos de los usuarios registrados
        /// en el sistema menos el ID
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCLient([Bind("idUsuario, nombre, correo, password, rol")] Usuarios usuario)
        {
            if ( User.Identity.Name != usuario.correo)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                if (usuario.rol == "Administrador" || usuario.rol == "Cliente")
                {
                    contexto.Update(usuario);

                    await contexto.SaveChangesAsync();

                    //Ver si se puede mandar un mensaje de confirmacion aqui

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View(usuario);
                }

            }
            else
            {
                return View(usuario);
            }
        }
    }
}
