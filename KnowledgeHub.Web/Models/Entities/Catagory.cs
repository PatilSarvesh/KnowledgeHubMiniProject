using System.ComponentModel.DataAnnotations;

namespace KnowledgeHub.Web.Models.Entities
{
    public class Catagory
    {
        public int CatagoryId { get; set; }
        [Required,MaxLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
