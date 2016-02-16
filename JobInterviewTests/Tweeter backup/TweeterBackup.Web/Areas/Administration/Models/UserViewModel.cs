namespace TweeterBackup.Web.Areas.Administration.Models
{
    using System.ComponentModel;
    using System.Linq;
    using AutoMapper;
    using TweeterBackup.Data.Model;
    using TweeterBackup.Web.Infrastructure.Mapping;

    public class UserViewModel : IMapFrom<User>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        [DisplayName("Saved Tweets")]
        public int TweetsCount { get; set; }

        [DisplayName("Re-tweeted")]
        public int RetweetCount { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<User, UserViewModel>()
                .ForMember(m => m.TweetsCount, opt => opt.MapFrom(u => u.Tweets.Count))
                .ForMember(m => m.RetweetCount, opt => opt.MapFrom(u => u.Tweets.Where(t => t.IsReTweeted).Count()));
        }
    }
}