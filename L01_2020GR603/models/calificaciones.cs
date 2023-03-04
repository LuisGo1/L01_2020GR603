using System.ComponentModel.DataAnnotations;

namespace L01_2020GR603.models
{
    public class calificaciones
    {
        [Key]
        public int calificacionesId { get; set; }

        public int publicacionesId { get; set; }

        public int usuariosId { get; set; }
        public int calificacion { get; set; }
    }
}
