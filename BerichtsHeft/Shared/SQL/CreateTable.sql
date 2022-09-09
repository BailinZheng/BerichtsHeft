
CREATE TABLE [dbo].[Activity](
	[ID] [varchar](40) NOT NULL,
	[HauptText] [nvarchar](255) NULL,
	[WochenTag] [int] NULL,
	[Name] [nvarchar](255) NULL,
	[Fach] [nvarchar](255) NULL,
	[AbgabeType] [nvarchar](255) NULL,
	[DateBlock] [int] NULL,
	[Dauertmin] [int] NULL,
	[DateOfReport] [datetime] NULL,
	CONSTRAINT [PK_Activity] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)
)


