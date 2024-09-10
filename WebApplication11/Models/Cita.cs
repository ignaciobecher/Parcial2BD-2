using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication11.Models
{
    public class Cita
    {

        public int id { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public string medico { get; set; }
        public string paciente { get; set; }


    }
}
