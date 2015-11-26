using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UnviersityEdu.Objects
{
	public class Course
	{
		[Key]
		public int CourseID { get; set; }
		public string Title { get; set; }
		public int Credits { get; set; }

		public virtual ICollection<Enrollment> Enrollments { get; set; }
	}
}