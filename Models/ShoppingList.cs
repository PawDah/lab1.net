namespace lab1.net.Models
{
    public class ShoppingList
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Planned { get; set; }
        public ICollection<ShoppingItem> ShoppingItems { get; set; } = new List<ShoppingItem>();
    }
}
