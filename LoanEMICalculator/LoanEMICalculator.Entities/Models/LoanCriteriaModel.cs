using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LoanEMICalculator.Entities.Models
{
    public class LoanCriteriaModel
    {
       // [DataMember(Name = "LoanCriteriaID", Order = 1)]
        public int LoanCriteriaID { get; set; }

        [Required]
        [Range(0, Int32.MaxValue)]
       // [DataMember(Name = "Loan Amount", Order = 2)]
        public int LoanAmount { get; set; }

        [Required]
        [Range(1, 100)]
       // [DataMember(Name = "Loan Interest", Order = 3)]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public float LoanInterest { get; set; }

        [Required]
        //[DataMember(Name = "No Of Years", Order = 4)]
        [Range(0, 30)]
        public float NoOfYears { get; set; }

        public float MonthlyInterest { get; set; }

        public float EMI { get; set; }

        public tblLoanCriteria TotableLoanCriteria()
        {
            tblLoanCriteria record = new tblLoanCriteria();
            record.LoanAmount = this.LoanAmount;
            record.LoanInterest = Math.Round(this.LoanInterest,2);
            record.NoOfYears = Math.Round(this.NoOfYears,2);
            return record;
        }
    }
}
