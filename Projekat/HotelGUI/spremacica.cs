//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HotelGUI
{
    using System;
    using System.Collections.Generic;
    
    public partial class spremacica
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public spremacica()
        {
            this.soba = new HashSet<soba>();
        }
    
        public string jmbgradnika { get; set; }
        public bool ima_opremu { get; set; }
    
        public virtual radnik radnik { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<soba> soba { get; set; }
    }
}
