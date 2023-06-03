using System.ComponentModel.DataAnnotations;

namespace EcoVidaCR.Models
{
    public class Usuarios
    {

        [Key]
        public int idUsuario { get; set; }

        [Required(ErrorMessage = "Debe ingresar el nombre del usuario")]
        [Display(Name = "Nombre de usuario")]
        [StringLength(100, ErrorMessage = "El nombre que ingreso es demasiado largo")]
        public string nombre { get; set; }

        [Required(ErrorMessage = "DEbe ingresar el correo del usuario")]
        [Display(Name = "Correo electronico")]
        [StringLength(100, ErrorMessage = "El correo que ingreso es demasiado largo")]
        [DataType(DataType.EmailAddress)]
        public string correo { get; set; }


        [Required(ErrorMessage = "Debe ingresar la contraseña")]
        [Display(Name = "Contraseña")]
        [StringLength(100, ErrorMessage = "La contraseña que ingreso es demasiado larga")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Required(ErrorMessage = "Debe ingresar el rol")]
        [Display(Name = "Rol")]
        [StringLength(100, ErrorMessage = "El rol ingresado es demasiado largo")]
        
        public string rol { get; set; }
    }
}
