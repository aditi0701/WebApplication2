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
    
    public partial class user
    {
        public user()
        {
            this.usercourses = new HashSet<usercours>();
        }
    
        public string name { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string gender { get; set; }
        public string qualification { get; set; }
        public string rank { get; set; }
        public decimal age { get; set; }
        public string branch { get; set; }
        public string medical { get; set; }
        public int user_id { get; set; }
        public Nullable<int> yearsOfServices { get; set; }
        public string location { get; set; }
        public string posting { get; set; }
        public Nullable<int> acrMarks { get; set; }
    
        public virtual ICollection<usercours> usercourses { get; set; }
    }
}
