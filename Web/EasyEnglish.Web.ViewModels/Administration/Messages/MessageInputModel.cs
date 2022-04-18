namespace EasyEnglish.Web.ViewModels.Administration.Messages
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class MessageInputModel
    {
        [Display(Name = "Content")]
        public string Description { get; set; }

        [DataType(DataType.Date)]

        [Display(Name = "Publish on")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Active until")]
        public DateTime? EndDate { get; set; }
    }
}
