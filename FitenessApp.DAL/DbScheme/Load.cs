//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FitenessApp.DAL.DbScheme
{
    using System;
    using System.Collections.Generic;
    
    public partial class Load
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Load()
        {
            this.CustomLoads = new HashSet<CustomLoad>();
        }
    
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Nullable<int> Series { get; set; }
        public string Iteration { get; set; }
        public string Icon { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomLoad> CustomLoads { get; set; }
    }
}
