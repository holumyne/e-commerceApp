namespace API.Dtos
{
    public class OrderDto
    {
        public string BasketId { get; set; }
        public int DeliveryMethodId { get; set; }
        public AddressDto ShipToAddress { get; set; } //note- the AddressDto use here is for the Identity.New AddressDto was not created for the order since they are the same
    }
}
