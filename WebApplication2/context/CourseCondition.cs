//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication2.context
{
    using System;
    using System.Collections.Generic;
    
    public partial class CourseCondition
    {
        public int course_id { get; set; }
        public int condition_id { get; set; }
        public string condition_value { get; set; }
        public int id { get; set; }
    
        public virtual condition condition { get; set; }
        public virtual cours cours { get; set; }
    }
}
