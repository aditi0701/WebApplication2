using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.context;

namespace WebApplication2.ViewModel
{
    public class AdminViewModel
    {
        public IEnumerable<cours> Course { get; set; }
        public IEnumerable<CourseCondition> CourseCondition { get; set; }
        public IEnumerable<CourseImplication> CourseImplication{ get; set; }

        public IEnumerable<condition> Condition { get; set; }

        public IEnumerable<user> Users { get; set; }

        public IEnumerable<admin> Admins { get; set; }

        public IEnumerable<usercours> UserCourses { get; set; }

        public IEnumerable<implication> Implication { get; set; }

        public IEnumerable<Remark> Remarks { get; set; }

        public IEnumerable<cor> cors { get; set; }
    }
}