namespace EasyEnglish.Data.Models
{
    using System;
    using System.Collections.Generic;

    using EasyEnglish.Data.Common.Models;

    public class Student : BaseDeletableModel<string>
    {
        public Student()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Courses = new HashSet<Course>();
            this.Images = new HashSet<Image>();
            this.Payments = new HashSet<Payment>();
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

        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }
    }
}
