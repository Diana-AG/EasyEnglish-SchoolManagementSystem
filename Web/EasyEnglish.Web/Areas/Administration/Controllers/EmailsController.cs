namespace EasyEnglish.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using EasyEnglish.Common;
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

        public EmailsController(IEmailSender emailSender)
        {
            this.emailSender = emailSender;
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

            var html = $"<p>{input.Content}</p>";
            var attachments = new List<EmailAttachment>();
            if (input.Attachments != null)
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

            await this.emailSender.SendEmailAsync("deedee.ag@gmail.com", GlobalConstants.SystemName, "kostadin.2g@gmail.com", input.Subject, html.ToString(), attachments);
            return this.Redirect("/");
        }
    }
}
