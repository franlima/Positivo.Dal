USE [prodcelular ]
GO

ALTER TABLE [dbo].[TESTSTEP] DROP CONSTRAINT [FK__TEST_STEP__ID_Te__14270015]
GO

/****** Object:  Table [dbo].[TESTSTEP]    Script Date: 21/12/2017 10:43:57 ******/
DROP TABLE [dbo].[TESTSTEP]
GO

/****** Object:  Table [dbo].[TESTSTEP]    Script Date: 21/12/2017 10:43:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TESTSTEP](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ID_Test_Seq] [int] NULL,
	[IdTp] [char](10) NOT NULL,
	[Description] [text] NULL,
	[LowLimit] [real] NOT NULL DEFAULT ((0)),
	[HighLimit] [real] NOT NULL DEFAULT ((0)),
	[Unit] [char](5) NOT NULL DEFAULT ('dBm')
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[TESTSTEP]  WITH CHECK ADD FOREIGN KEY([ID_Test_Seq])
REFERENCES [dbo].[TESTSEQ] ([ID])
GO


