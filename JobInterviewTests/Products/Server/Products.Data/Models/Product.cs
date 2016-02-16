namespace Products.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Product
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(30)]
        [Required]
        public string Description { get; set; }

        public int UserId { get; set; }

        public int TypeId { get; set; }

        public virtual ProductType Type { get; set; }
    }
}
