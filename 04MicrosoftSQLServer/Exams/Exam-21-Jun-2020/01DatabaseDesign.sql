--1. Database design
CREATE TABLE Cities(
	Id INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(20) NOT NULL,
	CountryCode CHAR(2) NOT NULL)

CREATE TABLE Hotels(
	Id INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(30) NOT NULL,
	CityId INT NOT NULL REFERENCES Cities(Id),
	EmployeeCount INT NOT NULL,
	BaseRate DECIMAL(18,2))

CREATE TABLE Rooms(
	Id INT PRIMARY KEY IDENTITY,
	Price DECIMAL(18,2)  NOT NULL,
	[Type] NVARCHAR(20) NOT NULL,
	Beds INT NOT NULL,
	HotelId INT NOT NULL REFERENCES Hotels(Id))

CREATE TABLE Trips(
	Id INT PRIMARY KEY IDENTITY,
	RoomId INT NOT NULL REFERENCES Rooms(Id),
	BookDate DATE NOT NULL,
	ArrivalDate DATE NOT NULL,
	ReturnDate DATE NOT NULL,
	CancelDate DATE,
	CONSTRAINT ck_validation_bookdate CHECK(BookDate < ArrivalDate),
	CONSTRAINT ck_validation_arrivaldate CHECK(ArrivalDate < ReturnDate)
	)

CREATE TABLE Accounts(
	Id INT PRIMARY KEY IDENTITY,
	FirstName NVARCHAR(50) NOT NULL,
	MiddleName NVARCHAR(20),
	LastName NVARCHAR(50) NOT NULL,
	CityId INT NOT NULL REFERENCES Cities(Id),
	BirthDate DATE NOT NULL,
	Email VARCHAR(100) NOT NULL UNIQUE)

CREATE TABLE AccountsTrips(
	AccountId INT NOT NULL REFERENCES Accounts(Id),
	TripId INT NOT NULL REFERENCES Trips(Id),
	Luggage INT NOT NULL CHECK(Luggage >= 0)
	CONSTRAINT PK_AccountsTrips PRIMARY KEY (AccountId, TripId))