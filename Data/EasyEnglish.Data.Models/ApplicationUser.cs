// ReSharper disable VirtualMemberCallInConstructor
namespace EasyEnglish.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using EasyEnglish.Data.Common.Models;

    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Images = new HashSet<Image>();
            this.Payments = new HashSet<Payment>();
            this.Languages = new HashSet<Language>();
            this.StudentCourses = new HashSet<Course>();
            this.TeacherCourses = new HashSet<Course>();
            this.TeacherRequests = new HashSet<TeacherRequest>();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
        }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public string FullName { get; set; }

        public string AddressText { get; set; }

        public int? AddressId { get; set; }

        public Address Address { get; set; }

        public int? TownId { get; set; }

        public virtual Town Town { get; set; }

        public int? CountryId { get; set; }

        public virtual Country Country { get; set; }

        public string ProfilePicture { get; set; }

        public Gender? Gender { get; set; }

        public DateTime? BirthDate { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }

        public virtual ICollection<Language> Languages { get; set; }

        public virtual ICollection<Course> StudentCourses { get; set; }

        [InverseProperty(nameof(Course.Teacher))]
        public virtual ICollection<Course> TeacherCourses { get; set; }

        public virtual ICollection<TeacherRequest> TeacherRequests { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
    }
}
