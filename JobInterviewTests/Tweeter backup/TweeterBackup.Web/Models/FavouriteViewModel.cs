namespace TweeterBackup.Web.Models
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;
    using TweeterBackup.Data.Model;
    using TweeterBackup.Web.Infrastructure.Mapping;
    using TweeterBackup.Web.Models.JsonModels;

    public class FavouriteViewModel : IMapFrom<Favourite>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public long TwitterId { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)] // According to Twitter documentation maximum 20 characters are allowed
        [DisplayName("Twitter username")]        
        public string Name { get; set; }

        [UIHint("Favourite")]
        public string Image { get; set; }
    }
}