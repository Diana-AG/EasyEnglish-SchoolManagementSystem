namespace EasyEnglish.Web.ViewModels.Administration.CourseTypes
{
    using System.Collections.Generic;

    public class CourseTypeInputModel
    {
        public int LanguageId { get; set; }

        public int LevelId { get; set; }

        public string Description { get; set; }

        public IEnumerable<KeyValuePair<string, string>> LevelsItems { get; set; }

        public IEnumerable<KeyValuePair<string, string>> LanguagesItems { get; set; }
    }
}
