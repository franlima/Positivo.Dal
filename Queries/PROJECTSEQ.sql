USE [prodcelular ]
GO

ALTER TABLE [dbo].[PROJECTSEQ] DROP CONSTRAINT [DF__PROJECT_S__Proje__2CF2ADDF]
GO

/****** Object:  Table [dbo].[PROJECTSEQ]    Script Date: 21/12/2017 10:38:06 ******/
DROP TABLE [dbo].[PROJECTSEQ]
GO

/****** Object:  Table [dbo].[PROJECTSEQ]    Script Date: 21/12/2017 10:38:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PROJECTSEQ](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ID_Test_Seq] [int] NULL,
	[Project_Name] [text] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[PROJECTSEQ] ADD  DEFAULT ('Project 1') FOR [Project_Name]
GO


