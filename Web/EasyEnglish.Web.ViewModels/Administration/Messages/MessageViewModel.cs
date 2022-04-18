﻿namespace EasyEnglish.Web.ViewModels.Administration.Messages
{
    using System;

    using EasyEnglish.Data.Models;
    using EasyEnglish.Services.Mapping;

    public class MessageViewModel : IMapFrom<Message>
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
