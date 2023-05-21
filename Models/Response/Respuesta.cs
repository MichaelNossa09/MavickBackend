namespace MavickBackend.Models.Response
{
    public class Respuesta
    {
        public int Exito { get; set; }
        public string Err { get; set; } = string.Empty;
        public object? Data { get; set; }
    }
}
