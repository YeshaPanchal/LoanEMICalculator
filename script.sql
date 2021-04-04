IF DB_ID(N'LoanEMICalculator2') IS NULL
BEGIN
	DECLARE @Sql AS NVARCHAR(MAX) = 'CREATE DATABASE LoanEMICalculator2'
	Exec(@Sql)
END
GO

USE [LoanEMICalculator2]
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tblLoanCriteria')
BEGIN
		CREATE TABLE [dbo].[tblLoanCriteria](
		[LoanCriteriaID] [int] IDENTITY(1,1) NOT NULL,
		[LoanAmount] [int] NOT NULL,
		[LoanInterest] [float] NOT NULL,
		[NoOfYears] [float] NOT NULL,
	 CONSTRAINT [PK_tblLoanCriteria] PRIMARY KEY CLUSTERED 
	(
		[LoanCriteriaID] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tblLoanEMITransaction')
BEGIN
		CREATE TABLE [dbo].[tblLoanEMITransaction](
		[LoanEMITransID] [int] IDENTITY(1,1) NOT NULL,
		[LoanCriteriaID] [int] NOT NULL,
		[InstallmentNo] [int] NOT NULL,
		[Opening] [float] NOT NULL,
		[Principle] [float] NOT NULL,
		[Interest] [float] NOT NULL,
		[EMI] [float] NOT NULL,
		[Closing] [float] NOT NULL,
		[CummulativeInterest] [float] NOT NULL,
	 CONSTRAINT [PK_tblLoanEMITransaction] PRIMARY KEY CLUSTERED 
	(
		[LoanEMITransID] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	ALTER TABLE [dbo].[tblLoanEMITransaction]  WITH CHECK ADD  CONSTRAINT [FK_tblLoanEMITransaction_tblLoanCriteria] FOREIGN KEY([LoanCriteriaID])
	REFERENCES [dbo].[tblLoanCriteria] ([LoanCriteriaID])

	ALTER TABLE [dbo].[tblLoanEMITransaction] CHECK CONSTRAINT [FK_tblLoanEMITransaction_tblLoanCriteria]
END
GO