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
	PizzaID int identity(1,1) not null, --and identity column that starts with 1, and increment by 1
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
insert into  Ps.Pizza (PizzaID, Name, CrustId, ModifiedDate) values --error, can't insert explicit values to identity column
(1,'standart', 1, '2018-01-01');
insert into  Ps.Pizza (Name, CrustId, ModifiedDate) values
('standart', 1, '2018-01-01');


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

--aggregate function
--	count
--	sum
--	avg(average)
--	min
--	max

-- views are not tables, but they can be treated like read-only tables.
-- they are like "computed columns" but for a whole table

select * from Ps.Pizza;
select * from Ps.Crust;
go

create view PS.ActivePizzas
as
select CrustId, Name, InternalName
from PS.Crust
where active = 1;

select * from Ps.ActivePizzas;
delete from Ps.ActivePizzas;

-- user-defined functions
go
create function PS.FirstPizzaName()
returns nvarchar(100)
as
begin
	declare @ret nvarchar(100);

	select top(1) @ret = Name
	from Ps.Pizza;

	return @ret;
end

select PS.FirstPizzaName();

--function to return the first crust with the given "active" status
go
create function PS.FirstCrustName(@status bit)
returns nvarchar(100)
as
begin
	declare @ret nvarchar(100);

	select top(1) @ret = Name
	from Ps.Crust
	where active = @status;

	return @ret;
end

select Ps.FirstCrustName(1);
select Ps.FirstCrustName(0);

-- user-define function can be table-valued (returns value in a whole table)
go
create function PS.AllNames()
returns table
as
	return
	select Name
	from PS.Pizza
	union select name from ps.Crust;

select * from PS.AllNames( -- reutrns all pizza and crust names in the DB as a 1-column table

--functions are not allowed to modify the database / insert rows, etc
-- they are read-only access

-- if we want to modify the database in this encapsulated kind of way,
--we use "stored procedures"

--functions can be used in SELECT clause and things like that, but procedures can't
go
drop procedure PS.ChangePizzaName;
go
create procedure PS.ChangePizzaName(@newname nvarchar(100))
as
begin
	begin try
		if (exists (select * from Ps.Pizza)) -- if there are any rows in the table
		begin
			update Ps.Pizza
			set Name = @newname
		end
		else
		begin
			raiserror('No Pizzas found', 16, 1)
		end
	end try
	begin catch
		print 'unable to change pizza names for reason:'
		print error_message();
	end catch
end

execute Ps.ChangePizzaName 'Great Pizza'

--you can use functions in select statements and where clauses etc
-- but you can't do that with pricedures
select * from ps.Crust where Name = ps.FirstCrustName();

--transactions
--a transactions is a set of statements wich follow the ACID properties
--	ACID
--		Atomic / Atomocity
--			A transaction must be all-or-nothing, must not be allowed to partially succeed and then fail.
--			if there is a failure, we must be returned to the original state
--		Consistent / Consistency
--			A transaction is not allowed to violate DB constraints or referencial integrity
--		Isolated / Isolation
--			the behavior of one transaction should not interfere with another transaction
--			each transaction should be able to thik of itself as the only one currently running
--			we compromise on "isolated" part of ACID heavily in practice
--		Durable / Durability
--			Effects/result must not be in "volatile memory", they must be persisted to disk
--			o some permanent storage

-- in SQL Server, we have four isolation levels to give us flexibility in isolation
-- why? because full isolation is lower performance (and higher possibility of deadlock)
--		read_uncommitted, read_committed,(default), repeatable, serializable

-- Problem	
--
--		Fixed with:
--			read_uncommitted
--
-- dirty read (see others transactions uncommitted changes
--			read_committed <- default SQL Server
--
-- non-repeatable read (see others's transactions new committed changes)
--		Fixed with:
--			repeatable
--
-- phantom read (see other's transactions new committed row insertions)
--		Fixed with:
--			Serializable

--Eveery SQL  statement by default is already a transaction in itself
-- and error part-way trhought will result in rolling back anyhing changed up to that point

-- with explicit "Begin Transaction" etc, we can have multi-statement transaction


-- Triggers

-- trigger that will update the "modified date" any time someone updates a row
go
create trigger PS.TR_Pizza on PS.Pizza
after update
as
begin
	--in a trigger, you have access to two special tables
	--called inserted and deleted
	--(like git, we think of updates as an insert and a delete
	update Ps.Pizza
	set ModifiedDate = GETDATE()
	where PizzaID in (select PizzaID from inserted)
end

update PS.Pizza
set name = 'new Pizza'

--triggers also support INSTEAD OF and BEFORE where i PUT AFTER


-- multiplicity
-- one-to-one (1-1)
--		One item  a columns (unique) in the table of the other
--		You can have foreign key back and forth, but one must be nullable "one-to-zero-or-one"
-- one-to-many (e.g. A-to-B) (1-n)
--		FK from B to A
-- many-to-many (e.g A-to-B) (n-n)
--		need junction table
--			ABId, AId, BId
--			PK	  FK   FK