CREATE TABLE [dbo].[Film]
(
	[Id]			INT IDENTITY (1, 1) NOT NULL,
	[IdGenre]		INT					NOT NULL,
	[Name]			NVARCHAR (250)		NOT NULL,
	[Duration]		TIME				NULL,
	[ReleaseDate]	DATE				NULL,
	[Score]			FLOAT				NULL,
	[File]			NVARCHAR (250)		NOT NULL,
	[Tag]			NVARCHAR (250)		NULL,
	[Watched]		BIT					NULL,
	CONSTRAINT [PK_Film] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_Film_Genre] FOREIGN KEY ([IdGenre]) REFERENCES [dbo].[Genre] ([Id])
)
