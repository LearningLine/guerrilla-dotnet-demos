//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MvcAsync
{
    using System;
    using System.Collections.Generic;
    
    public partial class author
    {
        public author()
        {
            this.titleauthors = new HashSet<titleauthor>();
        }
    
        public string au_id { get; set; }
        public string au_lname { get; set; }
        public string au_fname { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public bool contract { get; set; }
    
        public virtual ICollection<titleauthor> titleauthors { get; set; }
    }
}
