namespace EasyEnglish.Web.Areas.Administration.Controllers
{
    using EasyEnglish.Common;
    using EasyEnglish.Web.Controllers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.TeacherRoleName)]
    [Area("Administration")]
    public class TeacherController : BaseController
    {
    }
}
