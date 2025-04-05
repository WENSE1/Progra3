namespace LESApplication.Models
{
    public class Nodo
    {
        public string Informacion { get; set; }
        public Nodo Referencia { get; set; }  // Siguiente nodo
        public Nodo Anterior { get; set; }    // Nodo anterior

        public Nodo(string informacion)
        {
            Informacion = informacion;
            Referencia = null;
            Anterior = null;
        }
    }
}