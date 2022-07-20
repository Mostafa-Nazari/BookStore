using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1,100,ErrorMessage ="Display Ordr Should be in range of 1 to 100 only!")]
        public int DisplayOrder { get; set; }
        public DateTime CreatedDataTime { get; set; } = DateTime.Now;
    }
}
