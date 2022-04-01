namespace EasyEnglish.Web.Areas.Administration.Controllers
{
    using EasyEnglish.Common;
    using Microsoft.AspNetCore.Authorization;

    [Authorize(Roles = GlobalConstants.ManagerRoleName)]
    public class ManagerController : TeacherController
    {
    }
}
