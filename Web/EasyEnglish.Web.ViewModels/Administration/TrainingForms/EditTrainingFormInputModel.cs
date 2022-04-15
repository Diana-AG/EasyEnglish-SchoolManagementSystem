namespace EasyEnglish.Web.ViewModels.Administration.TrainingForms
{
    using EasyEnglish.Data.Models;
    using EasyEnglish.Services.Mapping;

    public class EditTrainingFormInputModel : TrainingFormInputModel, IMapFrom<TrainingForm>
    {
        public int Id { get; set; }
    }
}
