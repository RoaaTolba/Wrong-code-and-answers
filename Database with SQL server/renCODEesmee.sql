create database renCODEesmee

use renCODEesmee;

create table tbError 
(
	Id int identity(1,1) primary key ,
	Name varchar (50),
	ProgrammingLanguage varchar(60),
	Description varchar(300),
	Process varchar(1300),
	Image image
);
--drop database renCODEesmee
create table tbProgramming 
(
	Id int identity(1,1) primary key ,
	Name varchar (60)
);

insert into tbProgramming (Name)
values ('Html'),
('Css'),
('JavaScript'),
('C++'),
('C#'),
('Java'),
('Php'),
('Python'),
('Unknown');

/*************Procedures***********************/


create proc sp_selectAll
as
select* from tbError ;


create proc sp_selectProgrammingLanguage
as
select Name from tbProgramming;


create proc sp_addEntry
@name varchar(50),
@programmingLanguage varchar(60),
@description varchar(300),
@process varchar(1300),
@image image
as
insert into tbError(Name,ProgrammingLanguage,Description,Process,Image)
values (@name,@programmingLanguage,@description,@process,@image);  


create proc sp_selectId
@id int 
as
select 8 from tbError where Id =@id;


create proc sp_editEntry
@id int,
@name varchar(50),
@programmingLanguage varchar(60),
@description varchar(300),
@process varchar(1300),
@image image
as
update tbError 
set Name=@name,
ProgrammingLanguage=@programmingLanguage,
Description=@description,
Process=@process,
Image=@image
where Id = @id;


create proc sp_delEntry
@id int 
as
delete from tbError where Id=@id;


create proc sp_search
@para varchar(50)
as
select * from tbError where name like @para or ProgrammingLanguage like @para or Process like @para or Description like @para; 

create proc sp_insertLanguage
@para varchar(60)
as 
insert into tbProgramming(Name)
values (@para);

