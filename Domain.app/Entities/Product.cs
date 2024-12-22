namespace Domain.app.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public static Product Instance(string name,
                                       string description,
                                       decimal price,
                                       int categoryId)
        {
            return new Product
            {
                Name = name,
                Description = description,
                CategoryId = categoryId,
                Price = price
            };
        }
    }
}
