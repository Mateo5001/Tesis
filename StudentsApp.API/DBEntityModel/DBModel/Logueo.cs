//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DBEntityModel.DBModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Logueo
    {
        public int logeoID { get; set; }
        public int TipoLoginID { get; set; }
        public Nullable<int> LoginKeyID { get; set; }
        public Nullable<int> IdUsuario { get; set; }
    
        public virtual BTipoLogin BTipoLogin { get; set; }
        public virtual LoginKey LoginKey { get; set; }
        public virtual Usurio Usurio { get; set; }
    }
}