//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Sabateur
{
    using System;
    using System.Collections.Generic;
    
    public partial class PlayerSet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PlayerSet()
        {
            this.CardSet = new HashSet<CardSet>();
        }
    
        public string Name { get; set; }
        public bool IsTurn { get; set; }
        public short Score { get; set; }
        public Nullable<BlockType> Blocks { get; set; }
        public bool IsSabateur { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CardSet> CardSet { get; set; }
    }
}
