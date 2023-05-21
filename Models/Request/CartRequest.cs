namespace MavickBackend.Models.Request
{
    public class CartRequest
    {
        public long IdUser { get; set; }
        public int IdProduct { get; set; }
        public int Amount { get; set; }
    }
}
