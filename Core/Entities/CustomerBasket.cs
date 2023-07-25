namespace Core.Entities
{
    public class CustomerBasket
    {
        public CustomerBasket()
        {
        }
        public CustomerBasket(string id)
        {
            Id = id;
        }
        public string Id { get; set; } //id will be attached to customer
        public List<BasketItem> Items { get; set; } = new List<BasketItem>();
    }
}
