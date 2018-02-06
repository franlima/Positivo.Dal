USE [prodcelular ]
GO

ALTER TABLE [dbo].[TESTHEADER] DROP CONSTRAINT [FK__TEST_HEAD__ID_Te__282DF8C2]
GO

/****** Object:  Table [dbo].[TESTHEADER]    Script Date: 21/12/2017 10:39:23 ******/
DROP TABLE [dbo].[TESTHEADER]
GO

/****** Object:  Table [dbo].[TESTHEADER]    Script Date: 21/12/2017 10:39:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TESTHEADER](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ID_Test_Seq] [int] NULL,
	[Operator] [text] NULL,
	[Line] [text] NULL,
	[Station] [text] NULL,
	[Stage] [text] NULL,
	[Version] [text] NULL,
	[StartTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
	[SN] [text] NULL,
	[Test_Result] [text] NULL,
	[Elapse_Time] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[TESTHEADER]  WITH CHECK ADD FOREIGN KEY([ID_Test_Seq])
REFERENCES [dbo].[TESTSEQ] ([ID])
GO


