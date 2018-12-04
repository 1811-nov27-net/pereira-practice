-- JOINS
--	inner join
--		no sides of join can be null
--	left join
--		right side of join can be null
--	right join
--		left side of join can be null
--	cross join
--		cross all tables with all data (all combinations of subways sandwich)
-- normal joins condiction
-- FK = PK
-- self join is any join with the same table

-- joins combine data from two tables in one select
-- inner, left, right, full outer and cross
-- (self for joining with same table)

-- explore tables to see what columns to use
select * from SalesLT.Customer
select * from SalesLT.CustomerAddress
select * from SalesLT.Address

-- get the name and address of all customers in Houston
select c.FirstName, c.LastName, 
	a.AddressLine1, a.AddressLine2, a.City, a.StateProvince
from SalesLT.Customer as c
	inner join SalesLT.CustomerAddress as ca
		on c.CustomerID = ca.CustomerID
	inner join SalesLT.Address as a
		on a.AddressID = ca.AddressID
where a.City = 'Houston' and a.StateProvince = 'Texas';

-- find all costumers with multiple address
select c.FirstName, c.LastName, 
	count(ca.AddressId) as AddressCount
from SalesLT.Customer as c
	inner join SalesLT.CustomerAddress as ca
		on c.CustomerID = ca.CustomerID
group by c.CustomerID, c.FirstName, c.LastName
having count(ca.AddressID) > 1

-- match up all customers and address, including
-- addresses with no customer and customer with no address
select c.FirstName, c.LastName, 
	a.AddressLine1, a.AddressLine2, a.City, a.StateProvince
from SalesLT.Customer as c
	full outer join SalesLT.CustomerAddress as ca
		on c.CustomerID = ca.CustomerID
	full outer join SalesLT.Address as a
		on a.AddressID = ca.AddressID

-- subqueries are an alternative way to accomplish what joins do

-- we have operators like IN (checking membership in a list), NOT IN, EXISTS
-- ANY or ALL (checking for all true ir any true in boolean list
-- (in SQL, booleans is "BIT" 1,0

-- get the name of all customers in Houston, the subquery way
select FirstName, LastName
from SalesLT.Customer
where CustomerID IN
(
	select CustomerID
	from SalesLT.CustomerAddress
	where AddressID IN
	(
		select AddressID
		from SalesLT.Address
		where City = 'Houston' and 
			StateProvince = 'Texas'
	)
)

-- every first name that is also the last name with joins
-- every first name that is also the last name with subquery

select distinct c1.FirstName
from SalesLT.Customer c1
	inner join SalesLT.Customer c2
		on c1.FirstName = c2.LastName

select distinct FirstName
from SalesLT.Customer
where FirstName in
(
	select LastName
	from SalesLT.Customer
)

-- we also have set operators in sql
-- union, intersect, and except (set difference)
-- these don't go "inside" a select statement, they COMBINE two select statements

--all first name and last name, startin with A
select FirstName from SalesLT.Customer Where FirstName like 'A%'
union
select LastName from SalesLT.Customer Where LastName like 'A%'

-- all the set operators "by default" remove duplicates.
-- but they all have "all" versions - union all, intersect all, and except all

-- with union all, we see the duplicates. the all versions are always faster
-- (no loop to remove the duplicates)
select FirstName from SalesLT.Customer Where FirstName like 'A%'
union all
select LastName from SalesLT.Customer Where LastName like 'A%'

-- UNION give all records in EITHER of the two result sets
-- INTERSECT gives all records in BOTH result sets
-- EXCEPT gives all records in the FIRST BUT NOT in the SECOND result sets

--every first name that is also a last name with set operations
SELECT FirstName from SalesLT.Customer
intersect
SELECT LastName from SalesLT.Customer

-- people with the same first name and last name
SELECT *
FROM SalesLT.Customer WHERE FirstName = LastName


-- null represents a missing piece of data
-- all comparisons with null come out false, even = NULL
-- all things combines with null come out null

-- use "IS NULL" to check for something being null

-- with have coalesce function wich takes in a list, returns a list
-- with all the NULLS converted to something else

select  
	CustomerID, 
	Title, 
	COALESCE(FirstName, '') +' '+ COALESCE(MiddleName, '') +' '+ COALESCE(LastName, '') as [Full Name], 
	Suffix
from SalesLt.Customer;