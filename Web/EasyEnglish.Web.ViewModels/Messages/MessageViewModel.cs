namespace EasyEnglish.Web.ViewModels.Messages
{
    using System;

    using EasyEnglish.Data.Models;
    using EasyEnglish.Services.Mapping;

    public class MessageViewModel : IMapFrom<Message>
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }
    }
}
