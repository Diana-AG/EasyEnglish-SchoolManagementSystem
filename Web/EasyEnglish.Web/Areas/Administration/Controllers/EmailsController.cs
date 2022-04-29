namespace EasyEnglish.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using EasyEnglish.Common;
    using EasyEnglish.Services.Data;
    using EasyEnglish.Services.Messaging;
    using EasyEnglish.Web.Controllers;
    using EasyEnglish.Web.ViewModels.Administration.Emails;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Area("Administration")]
    [Authorize(Roles = $"{GlobalConstants.AdministratorRoleName}, {GlobalConstants.ManagerRoleName}, {GlobalConstants.TeacherRoleName} ")]
    public class EmailsController : BaseController
    {
        private readonly IEmailSender emailSender;
        private readonly IStudentsService studentsService;

        public EmailsController(
            IEmailSender emailSender,
            IStudentsService studentsService)
        {
            this.emailSender = emailSender;
            this.studentsService = studentsService;
        }

        public IActionResult Send()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Send(EmailInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input); 
            }

            var emails = await this.studentsService.GetAllEmailsAsync();
            var html = $"<p>{input.Content}</p>";
            var attachments = new List<EmailAttachment>();
            if (input.Attachments?.Any() == true)
            {
                foreach (var attachment in input.Attachments)
                {
                    byte[] fileBytes;
                    using (var memoryStream = new MemoryStream())
                    {
                        attachment.CopyTo(memoryStream);
                        fileBytes = memoryStream.ToArray();
                        memoryStream.Close();
                    }

                    attachments.Add(new EmailAttachment
                    {
                        FileName = attachment.FileName,
                        MimeType = attachment.ContentType,
                        Content = fileBytes,
                    });
                }
            }

            foreach (var email in emails)
            {
                await this.emailSender.SendEmailAsync(
                    GlobalConstants.EmailFrom,
                    GlobalConstants.SystemName,
                    email,
                    input.Subject,
                    html.ToString(),
                    attachments);
            }

            return this.Redirect("/");
        }
    }
}
