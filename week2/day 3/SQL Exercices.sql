-- Show the first name and the email address of customer with CompanyName 'Bike World'
select top 1 FirstName, EmailAddress
from SalesLT.Customer 
where CompanyName = 'Bike World'

-- Show the CompanyName for all customers with an address in City 'Dallas'.
select c.CompanyName
from SalesLT.Customer as c
	inner join SalesLT.CustomerAddress as ca
		on c.CustomerID = ca.CustomerID
	inner join SalesLT.Address as a
		on ca.AddressID = a.AddressID
where a.City = 'Dallas'

-- How many items with ListPrice more than $1000 have been sold?
select count(sd.OrderQty)
from SalesLT.SalesOrderDetail sd
	inner join SalesLT.Product p
		on sd.ProductID = p.ProductID
where p.ListPrice > 1000

-- Give the CompanyName of those customers with orders over $100000. 
-- Include the subtotal plus tax plus freight.
select c.CompanyName, soh.SubTotal + soh.TaxAmt + soh.Freight as Total
from SalesLT.SalesOrderHeader soh
	inner join SalesLT.Customer c
		on soh.CustomerID = c.CustomerID
where soh.TotalDue > 100000

-- Find the number of left racing socks ('Racing Socks, L') ordered by CompanyName 'Riding Cycles'.
--*** Didn't understand number of left racing socks
select sum(saleDatails.OrderQty)
from SalesLT.SalesOrderHeader salesHeader
	inner join SalesLT.Customer customer
		on salesHeader.CustomerID = customer.CustomerID
	inner join SalesLT.SalesOrderDetail saleDatails
		on salesHeader.SalesOrderID = saleDatails.SalesOrderID
	inner join SalesLT.Product product
		on saleDatails.ProductID = product.ProductID
where customer.CompanyName = 'Riding Cycles' 
	and product.Name = ('Racing Socks, L')

-- Show the SalesOrderID and the UnitPrice for every customer order where only one item is ordered.
--****
select distinct sod.SalesOrderID, sod.UnitPrice
from SalesLT.SalesOrderDetail as sod
	inner join SalesLT.SalesOrderHeader soh
		on soh.SalesOrderID = sod.SalesOrderID
where sod.OrderQty = 1

-- List the product name and the CompanyName for all Customers who ordered ProductModel 'Racing Socks'.
select customer.CompanyName, product.Name
from SalesLT.SalesOrderHeader salesHeader
	inner join SalesLT.Customer customer
		on salesHeader.CustomerID = customer.CustomerID
	inner join SalesLT.SalesOrderDetail salesDetails
		on salesHeader.SalesOrderID = salesDetails.SalesOrderID
	inner join SalesLT.Product product
		on salesDetails.ProductID = product.ProductID
	inner join SalesLT.ProductModel productModel
		on product.ProductModelID = productModel.ProductModelID
where productModel.Name = 'Racing Socks'
order by customer.CompanyName

-- Show the product description for culture 'fr' for product with ProductID 736.
select pd.Description
from SalesLT.Product p
	inner join SalesLT.ProductModel pm
		on p.ProductModelID = pm.ProductModelID
	inner join SalesLT.ProductModelProductDescription pmd
		on pm.ProductModelID = pmd.ProductModelID
	inner join SalesLT.ProductDescription pd
		on pmd.ProductDescriptionID = pd.ProductDescriptionID
where pmd.Culture = 'fr'
	and p.ProductID = 736

--Use the SubTotal value in SalesOrderHeader to list orders from the largest to the smallest. 
--For each order show the CompanyName and the SubTotal and the total weight of the - order.
select salesHeader.SalesOrderID, customer.CompanyName, 
	salesHeader.SubTotal, 
	(salesDetail.OrderQty * product.Weight) as weight
from SalesLT.SalesOrderHeader salesHeader
	inner join SalesLT.Customer customer
		on salesHeader.CustomerID = customer.CustomerID
	inner join SalesLT.SalesOrderDetail salesDetail
		on salesHeader.SalesOrderID = salesDetail.SalesOrderID
	inner join SalesLT.Product product
		on salesDetail.ProductID = product.ProductID
order by salesHeader.SubTotal DESC

-- How many products in ProductCategory 'Cranksets' have been sold to an address in 'London'?
select count(salesDetail.OrderQty)
from SalesLT.SalesOrderHeader salesHeader
	inner join SalesLT.SalesOrderDetail salesDetail
		on salesHeader.SalesOrderID = salesDetail.SalesOrderID
	inner join SalesLT.Product product
		on salesDetail.ProductID = product.ProductID
	inner join SalesLT.ProductCategory productCategory
		on product.ProductCategoryID = productCategory.ProductCategoryID
	inner join SalesLT.Address address
		on salesHeader.ShipToAddressID = address.AddressID
where productCategory.Name = 'Cranksets' and address.City = 'London'

--For every customer with a 'Main Office' in Dallas show AddressLine1 of the 'Main Office' and 
--AddressLine1 of the 'Shipping' address - if there is no shipping address leave it - blank. 
--Use one row per customer.

-- Show the best selling item by value.

-- Show the total order value for each CountryRegion. List by value with the highest first.

-- Find the best customer in each region.