--ddl
--data definition language
--for defining columns and tables, altering the structure of our database
--does not have access to individual rows or their contents

-- CREATE, ALTER, DROP (in come databases truncate too, because drop the table and create again)

--create database Pizza;
create schema PS;
go;
--go separates "batches" of commands
--the red squigglies will complain of you don't put it
--even thought you're only going to send that command by itself with highlighting anyway
drop table PS.Pizza;

--columns ar nullable by default (aka null)
--but you should always be explicit because nullable columns are mor unusual
create table PS.Pizza
(
	PizzaID int not null,
	Name nvarchar(100) not null,
	CrustId int not null,
	ModifiedDate datetime2 not null,
	CreatorName nvarchar(100) null
);

-- we can add constraints withn create table
-- or afterwards in alter table
alter table PS.Pizza
	add constraint PK_Pizza_PizzaID primary key (PizzaID);

alter table PS.Pizza
	add constraint U_Pizza_Name unique (Name);

create table PS.Crust
(
	CrustID int not null primary key,
	Name nvarchar(100) not null unique
)

alter table PS.Pizza
	add constraint FK_Pizza_Crust_CrustID foreign key (CrustID) references PS.Crust (CrustID);

--the only thing that actually accomplishes is make the database throw an error
-- if you violate referential integrity
-- (either delete or update a referenced PK value, or, insert or update and
-- FK value that doesn't exist in the referenced table



alter table PS.Pizza
	drop constraint FK_Pizza_Crust_CrustID;

-- on delete cascade (and on update cascade) specify that if the referenced PK value is deleted/updated,
-- then, this table should automatically get corresponding changes
-- e.g - deleting a crust would automatically delete all pizzas that use it instead of throwing an erro
alter table PS.Pizza
	add constraint FK_Pizza_Crust_CrustID 
		on delete cascade
		on update cascade;

-- CONSTRAINTS
--	primary key
--	unique
--	foreign key
--	not null (and null, kind of a fake constraint)
--	default
--	check

--we can add columns with alter table also
--	(these columns must be nullable or have defaults if there's already rows in the table.)

--add an active boolean column to enable "deleting" a row without actually deleting it
alter table Ps.Crust
	add active bit not null default(1);

-- check constraint is for any condition you want on the value
alter table Ps.Pizza
	drop constraint CK_Pizza_DateNotInFuture;
alter table Ps.Pizza
	add constraint CK_Pizza_DateNotInFuture check (ModifiedDate <= getdate() + '00:00:01');
--added one second to avoid default value failing check

insert into PS.Crust (CrustId, Name) values
	(1, 'Plain');
insert into  Ps.Pizza (PizzaID, Name, CrustId, ModifiedDate) values
(1, 'standart', 1, '2018-01-01');


--demo cascade on delete - also deletes the pizza using this crust
select * from Ps.Crust;
delete from Ps.Crust;
select * from PS.Pizza;

alter table Ps.Pizza
	add constraint D_Default_Pizza_ModifiedDate default getutcdate() for ModifiedDate;

	
insert into PS.Crust (CrustId, Name) values
	(1, 'Plain');
insert into  Ps.Pizza (PizzaID, Name, CrustId) values
(1, 'standart', 1);


-- we have "compute columns" in SQL
alter table PS.Crust
	drop column internalName ;
alter table PS.Crust
	add internalName as (convert(varchar(20), CrustId)+ '_' + Name);
-- that one is recalculated on every query

alter table Ps.Pizza
	add internalName as (convert(varchar(20), PizzaID)+ '_' + Name) persisted;
-- that one (Pesisted) is calculated once and then stored until the row is updated