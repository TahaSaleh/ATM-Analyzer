//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ATM.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ATM
    {
        public int id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string longtude { get; set; }
        public string latitude { get; set; }
        public Nullable<decimal> balance { get; set; }
        public Nullable<bool> crowded { get; set; }
        public Nullable<bool> status { get; set; }
        public Nullable<int> bank_id { get; set; }
        public Nullable<int> T_N { get; set; }
        public Nullable<int> T_N_last_hour { get; set; }
    
        public virtual Bank Bank { get; set; }
    }
}
