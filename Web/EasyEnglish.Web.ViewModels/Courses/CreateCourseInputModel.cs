namespace EasyEnglish.Web.ViewModels.Courses
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CreateCourseInputModel
    {
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [Display(Name="Teacher")]
        public string TeacherId { get; set; }

        [Display(Name = "Language")]
        public int LanguageId { get; set; }

        [Display(Name = "Level")]
        public int LevelId { get; set; }

        [Range(typeof(decimal), "0.01", "10000000")]
        public decimal Price { get; set; }

        [Display(Name = "Currency")]
        public int CurrencyId { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(100)]
        public string Description { get; set; }

        public IEnumerable<KeyValuePair<string, string>> LevelsItems { get; set; }

        public IEnumerable<KeyValuePair<string, string>> TeachersItems { get; set; }

        public IEnumerable<KeyValuePair<string, string>> LanguagesItems { get; set; }

        public IEnumerable<KeyValuePair<string, string>> CurrenciesItems { get; set; }
    }
}
