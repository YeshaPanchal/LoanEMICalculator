using LoanEMICalculator.Entities;
using LoanEMICalculator.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LoanEMICalculator.Services.Services
{
    public class LoanEMITransactionService
    {
        /// <summary>
        /// calculate EMI value
        /// </summary>
        /// <param name="p">Loan amount</param>
        /// <param name="r">Loan Interest</param>
        /// <param name="t">No of years</param>
        /// <returns></returns>
        public static double EMICalculation(float p,
                              float r, float t)
        {
            double emi;
            r = r / (12 * 100); // one month interest
            t = t * 12; // one month period
            emi = (p * r * (float)Math.Pow(1 + r, t))
                   / (float)(Math.Pow(1 + r, t) - 1);

            return (emi);
        }

        /// <summary>
        /// Add record of LoanCriteria and LoanEMITransaction
        /// </summary>
        /// <param name="record">LoanCriteriaModel</param>
        /// <returns></returns>
        public LoanCriteriaModel AddLoanCriteria(LoanCriteriaModel record)
        {
            tblLoanCriteria loanCriteria = new tblLoanCriteria();
            try
            {
                using (var dbContext = new LoanEMICalculatorEntities())
                {
                    loanCriteria = record.TotableLoanCriteria();
                    dbContext.tblLoanCriterias.Add(loanCriteria);
                    dbContext.SaveChanges();

                    //Get primary key value
                    int loanCriteriaID = loanCriteria.LoanCriteriaID;

                    List<tblLoanEMITransaction> lstLoanTrans = SessionUtils.LoanEMITransactionList;
                    if (lstLoanTrans != null)
                    {
                        lstLoanTrans.ForEach(n =>
                        {
                            n.LoanCriteriaID = loanCriteriaID;
                            dbContext.tblLoanEMITransactions.Add(n);

                        });
                        dbContext.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return record;
        }

        /// <summary>
        /// calculates transaction values
        /// </summary>
        /// <param name="record">LoanCriteriaModel</param>
        /// <returns></returns>
        public List<tblLoanEMITransaction> CalculateTransaction(LoanCriteriaModel record)
        {
            List<tblLoanEMITransaction> lstEMITrans = new List<tblLoanEMITransaction>();
            try
            {
                int installNo = 0;
                double interest = 0; double principle = 0; double closing = 0;
                List<double> allInterest = new List<double>();
                float noOfMonths = record.NoOfYears * 12;
                int months = (int)Math.Ceiling(noOfMonths);

                float openningAmount = record.LoanAmount;
                float interestRate = record.LoanInterest;
                float noOfYears = record.NoOfYears;
                float monthlyInterest = (interestRate / (12 * 100));
                double EMI = EMICalculation(openningAmount, interestRate, noOfYears);

                for (int i = 0; i < months; i++)
                {
                    tblLoanEMITransaction emiTrans = new tblLoanEMITransaction();
                    installNo = installNo + 1;
                    interest = (openningAmount * monthlyInterest);
                    principle = (EMI - interest);
                    closing = (openningAmount - principle);
                    //for calculating CummulativeInterest (sum of all interest)
                    allInterest.Add(Math.Round(interest, 2));

                    emiTrans.InstallmentNo = installNo;
                    emiTrans.Opening = Math.Round(openningAmount, 2);
                    emiTrans.EMI = Math.Round(EMI, 2);
                    emiTrans.Interest = Math.Round(interest, 2);
                    emiTrans.Principle = Math.Round(principle, 2);
                    emiTrans.Closing = Math.Round(closing, 2);
                    emiTrans.CummulativeInterest = allInterest.AsQueryable().Sum();

                    // Add into list
                    lstEMITrans.Add(emiTrans);

                    //Set variables for next iteration
                    openningAmount = (float)closing;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return lstEMITrans;
        }

        /// <summary>
        /// Check Loan criteria details already exists or not
        /// </summary>
        /// <param name="record">LoanCriteriaModel</param>
        /// <returns></returns>
        public tblLoanCriteria CheckLoanCriteriaExist(int loanAmount, float loanInterest, float noOfYears)
        {
            List<tblLoanCriteria> lst = new List<tblLoanCriteria>();
            tblLoanCriteria model = new tblLoanCriteria();
            try
            {
                var dbContext = new LoanEMICalculatorEntities();                
                model = dbContext.tblLoanCriterias.Where(s => s.LoanAmount == loanAmount && s.LoanInterest == Math.Round(loanInterest,2) && s.NoOfYears == Math.Round(noOfYears, 2)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return model;
        }
    }
}
