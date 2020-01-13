namespace Sulmar.Shopping.Domain
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
