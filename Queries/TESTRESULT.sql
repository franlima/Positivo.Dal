USE [prodcelular ]
GO

ALTER TABLE [dbo].[TESTRESULT] DROP CONSTRAINT [FK__TEST_RESU__ID_He__2A164134]
GO

/****** Object:  Table [dbo].[TESTRESULT]    Script Date: 21/12/2017 10:42:15 ******/
DROP TABLE [dbo].[TESTRESULT]
GO

/****** Object:  Table [dbo].[TESTRESULT]    Script Date: 21/12/2017 10:42:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TESTRESULT](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ID_Header] [int] NULL,
	[ID_TestStep] [int] NULL,
	[IdTp] [char](10) NULL,
	[Result] [real] NULL,
	[Elapse_time] [float] NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[TESTRESULT]  WITH CHECK ADD FOREIGN KEY([ID_Header])
REFERENCES [dbo].[TESTHEADER] ([ID])
GO


