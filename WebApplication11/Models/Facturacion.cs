namespace WebApplication11.Models
{
    public class Facturacion
    {
        public int id { get; set; }
        public string Descripcion { get; set; }
        public decimal Total { get; set; }
        public string medico {  get; set; }
        public string paciente {  get; set; }
    }
}
