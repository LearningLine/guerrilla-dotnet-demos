//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EFStarter
{
    using System;
    using System.Collections.Generic;
    
    public partial class MusicType
    {
        public MusicType()
        {
            this.Tracks = new HashSet<Track>();
        }
    
        public int GenreId { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<Track> Tracks { get; set; }
    }
}
