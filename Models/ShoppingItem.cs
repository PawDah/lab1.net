using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace lab1.net.Models
{
    public class ShoppingItem
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsChecked { get; set; }
        public int? ShoppingListId { get; set; }
        [ForeignKey("ShoppingListId")]
        [ValidateNever]
        public ShoppingList ShoppingList { get; set; }
    }
}
