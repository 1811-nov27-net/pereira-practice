drop table if exists company.employeeDetails;
drop table if exists company.employee;
drop table if exists company.department;
go
drop schema if exists company;
go
create schema company;
go

create table company.department
(
	id int identity(1,1) not null,
	name nvarchar(100) not null,
	location nvarchar(100) null
);

create table company.employee
(
	id int identity(1,1) not null,
	firstName nvarchar(50) not null,
	lastName nvarchar(50) not null,
	ssn nvarchar(15) null,
	deptId int null
)

create table company.employeeDetails
(
	id int identity(1,1) not null,
	employeeId int not null,
	salary money not null,
	address1 nvarchar(100) null,
	address2 nvarchar(100) null,
	city nvarchar(50) null,
	state nchar(2) null,
	country nvarchar(50) null
);

-- primary keys
alter table company.department
	add constraint PK_Department primary key (id);
alter table company.employee
	add constraint PK_Employee primary key (id);
alter table company.employeeDetails
	add constraint PK_EmployeeDetais primary key (id);

-- foreign keys
alter table company.employee
	add constraint FK_Employee_Company foreign key (deptId) references company.department (id)
	on delete set null
	on update cascade;

alter table company.employeeDetails
	add constraint FK_Employee_EmployeeDetails foreign key (employeeId) references company.employee (id)
	on delete cascade
	on update cascade;

-- inserts
insert into company.department (name, location) values
('Department 1','d1'),
('Department 2','d2'),
('Department 3','d3');

insert into company.employee (firstName, lastName, ssn, deptId) values
('First Name 1', 'Last Name 1', '111-11-1111', 1),
('First Name 2', 'Last Name 2', '222-22-2222', 2),
('First Name 3', 'Last Name 3', '333-33-3333', 3);

insert into company.employeeDetails (employeeId, salary, address1, city, state, country) values
(1, 10000, 'Address 1 emp 1', 'city 1', 's1', 'country 1'),
(2, 20000, 'Address 1 emp 2', 'city 2', 's2', 'country 2'),
(3, 30000, 'Address 1 emp 3', 'city 3', 's3', 'country 3');

--Exercices

insert into company.department (name) values
('Marketing')
insert into company.employee (firstName, lastName, deptId) values
('Tina', 'Smith', 4);
insert into company.employeeDetails (employeeId, salary) values
(4, 50000);

select e.*
from company.department d
	inner join company.employee e
		on d.id = e.deptId
where d.name = 'Marketing';

select sum(ed.salary) as MarketingSalarySum
from company.department d
	inner join company.employee e
		on d.id = e.deptId
	inner join company.employeeDetails ed
		on e.id = ed.employeeId
where d.name = 'Marketing'
group by e.id;


select count(*) as marketingEmployeesCount
from company.department d
where d.name = 'Marketing';

update company.employeeDetails
set salary = '90000'
where id in
(
	select id
	from company.employee
	where firstName = 'Tina' AND lastName = 'Smith'
);