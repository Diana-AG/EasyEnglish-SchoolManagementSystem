namespace EasyEnglish.Web.ViewModels.Administration.TrainingForms
{
    using EasyEnglish.Data.Models;
    using EasyEnglish.Services.Mapping;

    public class TrainingFormInputModel : IMapTo<TrainingForm>
    {
        public string Name { get; set; }
    }
}
