-- a comment

--first step: make sure the dropdown is set to the right DB not "master"
-- "use adventureworks;" is usually how you switch DBs, but Azure SQl DB doesn't support it

--your basic select query, all collumn and all rows from a given tables.
--Sales Li is the schema name, Costumer is the table name.
select * 
from SalesLT.Customer;

-- Highligh a command and press F5 (execute / play button) to run that command

--Select query/statement/command - read some info from the DB
--or, do *anything* that should return print a value

--Don't even need to acces the BD
select 1;
select 1+1;

--Two clauses in the select statement so far - SELECT clause and FROM clause
-- SELECT clause especifies the columns of our "result set"
-- FROM clause especifies what table or tables we are looking inside

select  CustomerID, Title, FirstName, MiddleName, LastName, Suffix
from SalesLt.Customer
--only get columns you ask for

--we can do calculations from the table's columns
-- and name our result columns with AS, even with spaces, using "" or []
select  CustomerID, Title, FirstName +' '+ MiddleName +' '+ LastName as [Name], Suffix
from SalesLt.Customer;

select * 
from SalesLT.Product;

select ProductId, name, ProductNumber, ListPrice * 1.08 as [finalPrice], ListPrice - StandardCost as [Profit]
from SalesLT.Product;

-- filter duplicate rows out with DISTINCT in the SELECT clause
select distinct Color, Size
from SalesLT.Product;

--table aliases
--important when we start selecting from multiple tables, have to dintinguish
-- would rather do so with short name
select p.Color, p.Size
from SalesLT.Product as p;

--where clause
--allows filtering of rows bases on a condition (before any calculated columns in the select clause
-- in SQL, string literals must be with single quote. Double quote is for names of things with spaces in them
select * 
from SalesLT.Customer
where FirstName = 'Brian';

-- we have boolean operators and, or
-- we can group then with parentheses
-- we have comparisons like = for equals
select * 
from SalesLT.Customer
where FirstName = 'Brian' and LastName = 'Goldstein';

--not-equals is <> or !=
select * 
from SalesLT.Customer
where FirstName = 'Brian' and LastName <> 'Goldstein';

-- SQL support, not regex, but some amount of its own pattern matching with LIKE operator
-- % - any number of characters
-- [abc] - either a,b or c
-- theres others
select *
from SalesLT.Customer
Where FirstName LIKE 'B[ar]%'

--we have ordered comparison with < <= > >=
--of numbers, dates, and also string ("dictionary"/ lexicographic ordering)

--all costumers with first names that starts with B
select * 
from SalesLT.Customer
where FirstName >= 'B' AND FirstName < 'C';

--some string functions
select LEFT('123456789', 4); -- return '1234'
select right('123456789', 4); -- return '6789'
select LEN('123456789'); -- returns 9, the length
select SUBSTRING('123456789', 3, 5); -- start index and length of substring
--string indexes are 1-based is SQL unfortanatelly
select REPLACE('Hello World', 'World', 'rogerio'); --return hello rogerio
select CHARINDEX(' ', 'Hello World'); --return 6 (the first index of that character in the string)

--data types
--	numeric
--		tinyint, smallint, int, bigint
--        1			2		4		8	(byte)
--	float, real, decimal
--		Use decimal for all floating-point stuff
--			Decimal as a type, accepts parameters
--				decimal (10,3) means 10 digits, with 3 of then after the decimal place
--					0000000.000
--		for currency values, we have money type

--	string
--		char(10) (fixed lenght character array)
--		varchar(10) variable lenght up to 10 character array
--		nchar(10) ("national" aka Unicode char array)
--		nvarchar(10) (unicode variable lenght)
--	for the varchars we can also pass "MAX" as parameter e.g NVARCHAR(MAX).
--		no reaosn to not use nvarchar all the times

--	string literals are technically VARCHAR('Hello')
--	we can make Unicode string literals (NVARCHAR) like: N'Hello'

-- we also hae binary type and VARBINARY for storing binary data directly in the DB
-- e.g ThumbNailPhoto column in Product Table
SELECT * 
FROM SalesLT.Product

-- date and time data types
-- date, time, datetime
-- don't use datetime, use datetime2 becaus it has more range
-- datetimeoffset (for intervals of time like "5 minutes"

-- we-ve seen SELECT, FROM and WHERE
-- the other clauses are GROUP BY, HAVING and ORDER BY
-- group all rows by first name, and count the number of duplicates per name
select FirstName, COUNT(FirstName) as Count
from SalesLT.Customer
group by FirstName

-- WHERE clause runs before the group by clause
-- so we can't filter on any conditions that depend on the grouping
-- e.g if i whant only the FirstName that *have* duplicates, i can't use Where,
-- because the grouping hasn't happened yet, there's nothing to compare
select FirstName, COUNT(FirstName) as Count
from SalesLT.Customer
where COUNT(FirstName) > 1
group by FirstName

-- count as an aggergate function, this means, it operates on many values and returns just on value.
-- (unlike LEN for example, takes one string and returns a number)
-- you use COUNT with GROUP BY

--HAVING is for conditions that run AFTER the GROUP BY

-- all first names besides Andrew that have any duplicate, and how many there are
select FirstName, COUNT(FirstName) as Count
from SalesLT.Customer
Where FirstName != 'Andrew'
group by FirstName
having COUNT(FirstName) > 1

-- ORDER BY sorts the result set and it's the last thing that runs

--All products sort by cheapest first
select *
from SalesLT.Product
order by StandardCost ASC

-- ascending order (ASC) is the default so you can skipt it
-- descending order is DESC

--all products sorted by coler alphabetically and by most expensive cost
select *
from SalesLT.Product
order by Color, StandardCost DESC

-- execution order of a select statement
-- from
-- where
-- group by
-- having
-- order by
-- select
-- (in order of writing it, except for select)
