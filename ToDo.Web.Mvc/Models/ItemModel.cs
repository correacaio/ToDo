using System.ComponentModel.DataAnnotations;

namespace ToDo.Web.Mvc.Models
{
    public class ItemModel
    {
        public Guid Id { get; set; }
        
        public string Description { get; set; }
        
        public bool Done { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
