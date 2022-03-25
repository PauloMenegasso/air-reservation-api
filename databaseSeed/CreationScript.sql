CREATE DATABASE [NetacticaCase]

USE DATABASE [NetacticaCase]

GO
CREATE TABLE [dbo].[Airports](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Code] [varchar](3) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

GO
CREATE TABLE [dbo].[AirReservations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OriginAirport] [nvarchar](max) NOT NULL,
	[TargetAirport] [nvarchar](max) NOT NULL,
	[DepartureDatetime] [datetime2](7) NOT NULL,
	[ArrivalDatetime] [datetime2](7) NOT NULL,
	[Airline] [nvarchar](max) NOT NULL,
	[FlightNumber] [bigint] NOT NULL,
	[NumberOfAdultPassagers] [int] NOT NULL,
	[NumberOfChieldPassagers] [int] NOT NULL,
 CONSTRAINT [PK_AirReservations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

INSERT INTO [dbo].[Airports]([Name], [Code])
VALUES  ('Guarulhos', 'GRU'),
        ('Buenos Aires', 'EZE'),
        ('La Paz', 'LPB'),
        ('Santiago', 'SCL'),
        ('Bogota', 'BOG'),
        ('Cidade do México', 'MEX'),
        ('Los Angeles', 'LAX')