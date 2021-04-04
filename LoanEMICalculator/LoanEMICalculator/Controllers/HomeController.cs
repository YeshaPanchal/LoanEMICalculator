using LoanEMICalculator.Entities;
using LoanEMICalculator.Entities.Models;
using LoanEMICalculator.Services;
using LoanEMICalculator.Services.Services;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace LoanEMICalculator.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Calculate EMI transactions values
        /// </summary>
        /// <param name="model">LoanCriteriaModel</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CalculateEMI(LoanCriteriaModel model)
        {
            List<tblLoanEMITransaction> lstTrans = new List<tblLoanEMITransaction>();
            if (ModelState.IsValid)
            {
                try
                {
                    LoanEMITransactionService service = new LoanEMITransactionService();
                    lstTrans = service.CalculateTransaction(model);
                    SessionUtils.LoanEMITransactionList = lstTrans;
                }
                catch (Exception ex)
                {
                    return Json(new { Result = "Error", Message = ex.Message });
                }
                return PartialView("CalculateEMIView", lstTrans);
            }
            return View(model);
        }

        /// <summary>
        /// Store EMI values to DB
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult StoreEMI(LoanCriteriaModel model)
        {
            List<tblLoanEMITransaction> lstTrans = new List<tblLoanEMITransaction>();
            if (ModelState.IsValid)
            {
                try
                {
                    LoanEMITransactionService service = new LoanEMITransactionService();
                    service.AddLoanCriteria(model);
                    return Json(new { Result = "OK" });
                }
                catch (Exception ex)
                {
                    return Json(new { Result = "Error" ,Message = ex.Message});
                }
            }
            return Json(new { Result = "Error"});
        }

        /// <summary>
        /// Check LoanDetails alreay Exists or not
        /// </summary>
        /// <param name="loanAmount"></param>
        /// <param name="loanInterest"></param>
        /// <param name="noOfYears"></param>
        /// <returns></returns>
        public string CheckLoanDetailsExists(float loanAmount, float loanInterest, float noOfYears)
        {
            LoanEMITransactionService service = new LoanEMITransactionService();
            tblLoanCriteria loanRecord = service.CheckLoanCriteriaExist((int)loanAmount, loanInterest, noOfYears);
            if (loanRecord != null)
               return "Exists";

            return "NoTExists";
        }

        /// <summary>
        /// Get EMI value
        /// </summary>
        /// <param name="loanAmount"></param>
        /// <param name="loanInterest"></param>
        /// <param name="noOfYears"></param>
        /// <returns></returns>
        public double GetEMIValue(float loanAmount, float loanInterest, float noOfYears)
        {
            double EMI = LoanEMITransactionService.EMICalculation(loanAmount, loanInterest, noOfYears);
            return Math.Round(EMI, 2);
        }

        /// <summary>
        /// Get Monthly Interest value
        /// </summary>
        /// <param name="loanInterest"></param>
        /// <returns></returns>
        public double GetMonthlyInterest(float loanInterest)
        {
            double monthlyInterest = loanInterest / (12 * 100);
            return monthlyInterest;
        }
    }
}