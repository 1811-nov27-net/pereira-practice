-- DML
-- Database Manupulations Language
-- The subset of SQL wich accessess and modifies individual rows

-- SELECT, INSERT, UPDATE, DELETE, TRUNCATE TABLE

-- select is by far the more complicated

select * from SalesLT.Address

-- insert
-- simple insert with all columns except the identity column
insert into SalesLT.Address values
('123 Mains St', NULL, 'Dallas', 'TX', 'United States', '12345', '268AF621-76D7-4C78-9441-144FD139821B', GETUTCDATE())

--better insert more readable - be explicit about column names
-- allow relaying on defaults for nullable columns, for rowguids, etc
insert into SalesLT.Address (AddressLine1, City, StateProvince, CountryRegion, PostalCode)
values
('1234 Mains St', 'Dallas', 'TX', 'United States', '12345'),
(replace('1234 Mains St', '123', '456'), 'Dallas', 'TX', 'United States', '12345');

-- there are ways to do bulk inserts from things like CSV files
bulk insert into SalesLT.Address from 'data.csv' with (fieldterminator = ',', rowterminator = '\n')

-- 
insert into SalesLT.Address (AddressLine1, City, StateProvince, CountryRegion, PostalCode)
	select AddressLine1, City, StateProvince, CountryRegion, reverse(PostalCode)
	from SalesLT.Address
	where ModifiedDate > '2018' -- Year(ModifiedDate) >= 2018

-- we have temporary variables we can use with #
select AddressLine1, City, StateProvince, CountryRegion, PostalCode
into #my_table
from SalesLT.Address
where ModifiedDate > '2018'

insert into SalesLT.Address (AddressLine1, City, StateProvince, CountryRegion, PostalCode)
	select * from #my_table

-- UPDATE
select * from SalesLT.Address  where YEAR(ModifiedDate) >= 2018;


-- for every recently modified row, update the address line 2 and set the postal code
--equal to the most recently modified row's postal code
update SalesLT.Address
set AddressLine2 = 'apt 45', 
	PostalCode = 
	( 
		select top(1) PostalCode from SalesLT.Address order by ModifiedDate DESC
	)
where YEAR(ModifiedDate) >= 2018;

--DELETE

-- delete every row in the table (slow way, one by one)
-- delete from SalesLT.Address

delete from SalesLT.Address
where ModifiedDate > '2018'


-- truncate
--truncate delete every row in the table all at once, fast way
--(table itself still exists, but empty)
--truncate table SalesLT.Address;