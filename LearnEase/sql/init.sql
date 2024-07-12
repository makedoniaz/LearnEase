create database LearnEase

use LearnEase

create table Courses (
	[Id] int primary key identity,
	[Name] nvarchar(100) not null,
	[Description] nvarchar(500) not null,
	AmountOfLectures int,
	CreationDate datetime2 not null
)

create table Feedbacks (
    [Id] int primary key identity,
	[Username] nvarchar(100) not null,
	[Text] nvarchar(500),
	[Rating] int,
	[CourseId] int foreign key references Courses(Id)
    CreationDate datetime2 not null
)