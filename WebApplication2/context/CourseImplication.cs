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
    
    public partial class CourseImplication
    {
        public Nullable<int> course_id { get; set; }
        public int id { get; set; }
        public int implication_id { get; set; }
        public string implication_value { get; set; }
    
        public virtual cours cours { get; set; }
        public virtual implication implication { get; set; }
    }
}
