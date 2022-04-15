namespace EasyEnglish.Web.ViewModels.Administration.TrainingForms
{
    using EasyEnglish.Data.Models;
    using EasyEnglish.Services.Mapping;

    public class TrainingFormViewModel : IMapFrom<TrainingForm>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
