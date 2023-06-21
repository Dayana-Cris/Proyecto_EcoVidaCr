using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoVidaCR.Models
{
    /*
        idVoluntariado int not null primary key identity, 
	    nombreVoluntariado varchar (500) not null,
	    descripcion varchar (500) not null,
	    nombreDestino varchar (500) not null,
	    correo varchar (500) not null,
	    telefono varchar (100)
     */
    public class Voluntariados
    {
        [Key]
        public int idVoluntariado { get; set; }

        [Required(ErrorMessage = "Debe ingresar el nombre del voluntariado")]
        [Display(Name = "Nombre del voluntariado")]
        [StringLength(500, ErrorMessage = "El nombre que ingreso es demasiado largo")]
        public string nombreVoluntariado { get; set; }

        [Required(ErrorMessage = "Debe ingresar la descripcion del voluntariado")]
        [Display(Name = "Descripcion del voluntariado")]
        [StringLength(500, ErrorMessage = "La descripcion que ingreso es demasiada larga")]
        public string descripcion { get; set; }

        [Required(ErrorMessage = "Debe ingresar el nombre del destino")]
        [Display(Name = "Id del destino")]
        public int idDestino { get; set; }

        [Required(ErrorMessage = "Debe ingresar el correo del usuario")]
        [Display(Name = "Correo electronico")]
        [StringLength(100, ErrorMessage = "El correo que ingreso es demasiado largo")]
        [DataType(DataType.EmailAddress)]
        public string correo { get; set; }

        [Required(ErrorMessage = "Debe ingresar el telefono de contacto para el voluntariado")]
        [Display(Name = "Telefono de contacto")]
        [StringLength(15, ErrorMessage = "El telefono que agrego es demasiado largo")]
        public string telefono { get; set; }

        [Required(ErrorMessage = "Debe ingresar una ruta valida de imagen")]
        [Display(Name = "Url para la imagen del destino")]
        [StringLength(500, ErrorMessage = "La ruta ingresada es demasiada larga")]
        [DataType(DataType.ImageUrl)]
        public string rutaURLimg { get; set; }

        [Display(Name = "Nombre del destino")]
        public string nombreDestino {
            get;


            set;
            
        }        

    }
}
