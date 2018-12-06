create database MoviesDB;
go

create schema Movies;
go

create table Movies.Movie
(
	ID int identity not null,
	Name nvarchar(100) not null,
	GenreID int not null,

	constraint PK_Movies_ID primary key (ID)
);

--drop table Movies.Genre;
create table Movies.Genre
(
	ID int identity not null,
	Name nvarchar(100) not null,

	constraint PK_Genre_ID primary key (ID)
);

alter table Movies.Movie
	add constraint FK_Movies_Genre_GenreID foreign KEY (GenreID) references Movies.Genre (ID);

alter table Movies.Movie
	add ReleaseDate datetime2 null;

insert into Movies.Genre (Name) values
('Action'),	--1
('Drama');	--2

insert into Movies.Movie (Name, GenreID) values
('Indiana Jones', (select id from Movies.Genre where Name = 'Action')),
('Star Wars', 1); --Or this way just assuming what i know the id to be