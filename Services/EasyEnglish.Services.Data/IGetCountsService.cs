namespace EasyEnglish.Services.Data
{
    using EasyEnglish.Services.Data.Models;
    using EasyEnglish.Web.ViewModels.Home;

    public interface IGetCountsService
    {
        CountsDto GetCounts();
    }
}
