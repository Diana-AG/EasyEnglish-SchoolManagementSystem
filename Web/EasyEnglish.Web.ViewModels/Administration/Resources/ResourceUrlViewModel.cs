namespace EasyEnglish.Web.ViewModels.Administration.Resources
{
    using EasyEnglish.Data.Models;
    using EasyEnglish.Services.Mapping;

    public class ResourceUrlViewModel : IMapFrom<Resource>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }
    }
}
