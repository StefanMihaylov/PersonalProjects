namespace TweeterBackup.Web.Models.InputModels
{
    using System.ComponentModel.DataAnnotations;

    public class FavouriteInputViewModel
    {
        [Required]
        [StringLength(20, MinimumLength = 3)] // According to Twitter documentation maximum 20 characters are allowed
        public string Name { get; set; }
    }
}