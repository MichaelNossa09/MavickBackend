namespace MavickBackend.Models.Request
{
    public class ConceptRequest
    {
        public long Id { get; set; }
        public int IdSale { get; set; }
        public int Amount { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Import { get; set; }
        public int IdProduct { get; set; }
    }
}
