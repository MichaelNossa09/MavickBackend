namespace MavickBackend.Models.Request
{
    public class AddressRequest
    {
        public long Id { get; set; }
        public string Direction { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string State { get; set; } = null!;
        public int Postalcode { get; set; }
        public long IdUser { get; set; }
    }
}
