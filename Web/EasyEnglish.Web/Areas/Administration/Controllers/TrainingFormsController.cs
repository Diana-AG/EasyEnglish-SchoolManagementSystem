namespace EasyEnglish.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using EasyEnglish.Services.Data;
    using EasyEnglish.Web.Constants;
    using EasyEnglish.Web.ViewModels.Administration.TrainingForms;
    using Microsoft.AspNetCore.Mvc;

    [Area("Administration")]
    public class TrainingFormsController : AdministratorController
    {
        private readonly ITrainingFormsService trainingFormsService;

        public TrainingFormsController(
            ITrainingFormsService trainingFormsService)
        {
            this.trainingFormsService = trainingFormsService;
        }

        public async Task<IActionResult> Index()
        {
            var trainingFormViewModels = await this.trainingFormsService.GetAllAsync<TrainingFormViewModel>();

            return this.View(trainingFormViewModels);
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TrainingFormInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.trainingFormsService.CreateAsync(input);
            return this.RedirectToAction(nameof(this.Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var trainingForm = await this.trainingFormsService.GetByIdAsync<EditTrainingFormInputModel>(id);
            if (trainingForm == null)
            {
                return this.NotFound();
            }

            return this.View(trainingForm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditTrainingFormInputModel input)
        {
            if (id != input.Id)
            {
                return this.NotFound();
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.trainingFormsService.UpdateAsync(id, input);
            return this.RedirectToAction(nameof(this.Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var trainingForm = await this.trainingFormsService.GetByIdAsync<TrainingFormViewModel>(id);
            if (trainingForm == null)
            {
                return this.NotFound();
            }

            this.ViewData[MessageConstant.WarningMessage] = "Please, be careful! Deletion may result in data loss";

            return this.View(trainingForm);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.trainingFormsService.DeleteAsync(id);
            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
