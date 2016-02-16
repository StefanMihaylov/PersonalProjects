namespace TweeterBackup.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using System.Linq;
    using System.Web;

    using AutoMapper;
    using TweeterBackup.Data.Model;
    using TweeterBackup.Web.Infrastructure.Mapping;

    public class SavedTweetsViewModel : IMapFrom<Tweet>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [DisplayName(" ")]
        public string TwitterId { get; set; }

        public string Text { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}")]
        public DateTime CreatedAt { get; set; }

        [DisplayName("Re-tweeted")]
        public bool IsReTweeted { get; set; }

        public string AuthorName { get; set; }

        [DisplayName("Tweet Author")]
        public string AuthorImage { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Tweet, SavedTweetsViewModel>()
                .ForMember(m => m.AuthorName, opt => opt.MapFrom(u => u.Author.Name))
                .ForMember(m => m.AuthorImage, opt => opt.MapFrom(u => u.Author.Image));
        }
    }
}