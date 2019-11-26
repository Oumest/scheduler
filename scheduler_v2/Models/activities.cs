//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace scheduler_v2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class activities
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public activities()
        {
            this.timesheet = new HashSet<timesheet>();
        }
    
        public int id { get; set; }
        public int project_id { get; set; }
        public string name { get; set; }
        public string comment { get; set; }
        public short visible { get; set; }
        public Nullable<double> fixed_rate { get; set; }
        public Nullable<double> hourly_rate { get; set; }
        public Nullable<double> budget { get; set; }
        public Nullable<int> time_budget { get; set; }
        public string color { get; set; }
    
        public virtual projects projects { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<timesheet> timesheet { get; set; }
    }
}
