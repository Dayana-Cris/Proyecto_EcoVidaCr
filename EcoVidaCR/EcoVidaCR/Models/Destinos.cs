using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace EcoVidaCR.Models
{
    public class Destinos
    {

       /* 
            idDestino int not null primary key identity, 
	        nombre varchar(500) not null,
	        descripcion varchar(500) not null,
	        ubicacion varchar(500) not null,
	        aspectos varchar(500) not null
       */

        [Key]
        public int idDestino { get; set; }

        [Required(ErrorMessage = "Debe ingresar el nombre del destino")]
        [Display(Name = "Nombre del destino")]
        [StringLength(500, ErrorMessage = "El nombre que ingreso es demasiado largo")]
        public string nombre { get; set; }


        [Required(ErrorMessage = "Debe ingresar la descripcion del destino turistico sostenible")]
        [Display(Name = "Descripcion del destino: ")]
        [StringLength(500, ErrorMessage = "La descripcion que ingreso es demasiada larga")]
        public string descripcion { get; set; }

        [Required(ErrorMessage = "Debe ingresar la ubicacion del destino turistico sostenible")]
        [Display(Name = "Ubicacion")]
        [StringLength(500, ErrorMessage = "La ubicacion que ingreso es demasiada larga")]
        public string ubicacion { get; set; }


        [Required(ErrorMessage = "Debe ingresar la descripcion del destino turistico sostenible")]
        [Display(Name = "Aspectos importantes del destino turistico")]
        [StringLength(500, ErrorMessage = "Los apectos importantes que ingreso es demasiado largo")]
        public string aspectos { get; set; }

        [Required(ErrorMessage = "Debe ingresar una ruta valida de imagen")]
        [Display(Name = "Url para la imagen del destino")]
        [StringLength(500, ErrorMessage = "La ruta ingresada es demasiada larga")]
        [DataType(DataType.ImageUrl)]
        public string rutaURLimg { get; set; }

        

    }
}
