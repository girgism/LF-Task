namespace Domain.app.Entities
{
    public class Category
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public List<Product> Products { get; private set; }


        public static Category Instance(string name, string description)
        {
            return new Category
            {
                Name = name,
                Description = description
            };
        }

        public void Update(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }
    }
}
