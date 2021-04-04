using LoanEMICalculator.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LoanEMICalculator.Services
{
    public static class SessionUtils
    {
        public static List<tblLoanEMITransaction> LoanEMITransactionList
        {
            get
            {
                if (HttpContext.Current.Session["LoanEMITransactionList"] != null)
                    return ((List<tblLoanEMITransaction>)HttpContext.Current.Session["LoanEMITransactionList"]);
                else
                    return null;

            }
            set
            {
                HttpContext.Current.Session["LoanEMITransactionList"] = (List<tblLoanEMITransaction>)value;
            }
        }

    }
}
