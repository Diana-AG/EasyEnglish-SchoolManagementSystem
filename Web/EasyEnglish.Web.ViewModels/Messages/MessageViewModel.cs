namespace EasyEnglish.Web.ViewModels.Messages
{
    using System;

    using AutoMapper;
    using EasyEnglish.Data.Models;
    using EasyEnglish.Services.Mapping;

    public class MessageViewModel : IMapFrom<Message>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string AddedByUserId { get; set; }

        public ApplicationUser AddedByUser { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Message, MessageViewModel>()
                  .ForMember(x => x.EndDate, options =>
                      options.MapFrom(x => x.EndDate ?? DateTime.UtcNow.AddYears(10)));
        }
    }
}
