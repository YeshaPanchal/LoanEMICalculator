//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LoanEMICalculator.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblLoanCriteria
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblLoanCriteria()
        {
            this.tblLoanEMITransactions = new HashSet<tblLoanEMITransaction>();
        }
    
        public int LoanCriteriaID { get; set; }
        public int LoanAmount { get; set; }
        public double LoanInterest { get; set; }
        public double NoOfYears { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblLoanEMITransaction> tblLoanEMITransactions { get; set; }
    }
}
