using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FriendWithDB.Models
{
    public class Friend
    {
        [Key]
        [Required]
        [Display(Name= "ID")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Picture URL")]
        public string ImageUrl { get; set; }
    }
}
