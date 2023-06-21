using System.ComponentModel.DataAnnotations.Schema;

namespace EcoVidaCR.Models
{
    public class VoluntariadosDestinos
    {
        [ForeignKey("idDestino")]
        public Destinos Destinos { get; set; }

        [ForeignKey("idVoluntariado")]
        public Voluntariados Voluntariados { get; set; }

        public int idDestino { get; set; }

        public int idVoluntariado { get; set; }
    }
}
