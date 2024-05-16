create database LearnEase

use LearnEase

create table Courses (
	[Id] int primary key identity,
	[Name] nvarchar(100),
	[Description] nvarchar(500),
	[AmountOfLectures] int null,
	[CreationDate] datetime2
)

create table Feedbacks (
    [Id] int primary key identity,
	[Username] nvarchar(100),
	[Text] nvarchar(500),
	[Rating] int null,
	[CourseId] int foreign key references Courses(Id)
    [CreationDate] datetime2
)

create table Logs (
	[Id] int primary key identity,
	[Url] nvarchar(100),
	[RequestBody] nvarchar(max) null,
	[ResponseBody] nvarchar(max) null,
	[CreationDate] datetime2,
	[EndDate] datetime2,
	[StatusCode] int,
	[HttpMethod] nvarchar(50)
)