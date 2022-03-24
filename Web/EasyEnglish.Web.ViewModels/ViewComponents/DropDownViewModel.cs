namespace EasyEnglish.Web.ViewModels.ViewComponents
{
    using System.Collections.Generic;

    public class DropDownViewModel
    {
        public IEnumerable<KeyValuePair<string, string>> Items { get; set; }
    }
}
