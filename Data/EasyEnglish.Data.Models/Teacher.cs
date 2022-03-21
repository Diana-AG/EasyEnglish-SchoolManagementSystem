namespace EasyEnglish.Data.Models
{
    using System;
    using System.Collections.Generic;

    using EasyEnglish.Data.Common.Models;

    public class Teacher : BaseDeletableModel<string>
    {
        public Teacher()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Courses = new HashSet<Course>();
            this.Languages = new HashSet<Language>();
        }

        public string Name { get; set; }

        public int AddressId { get; set; }

        public virtual Address Address { get; set; }

        public int TownId { get; set; }

        public virtual Town Town { get; set; }

        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

        public string PhoneNumber { get; set; }

        public string ProfilePicture { get; set; }

        public Gender Gender { get; set; }

        public DateTime BirthDate { get; set; }

        public string Note { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Course> Courses { get; set; }

        public virtual ICollection<Language> Languages { get; set; }
    }
}
