create database library
use library

create table login(staffID int IDENTITY(1,1) Primary Key,username nvarchar(50) not null,password nvarchar(50),Role nvarchar(50) not null,email nvarchar(50) not null unique,address nvarchar(80) not null)

create table books(book_ID int IDENTITY(1,1) primary key, title nvarchar(150) not null,author nvarchar(50) not null,ISBN nvarchar(50) unique not null,publisher nvarchar(50),Yearpublished int,quantity int not null)
create table members(memberID int IDENTITY(1,1) primary key,fullname nvarchar(50) not null,email nvarchar(50) unique not null,address nvarchar(50),JoinDate Date not null DEFAULT GETDATE())
CREATE TABLE BorrowedBooks (BorrowID INT IDENTITY(1,1) PRIMARY KEY,memberID INT NOT NULL,book_ID INT NOT NULL,BorrowDate DATE NOT NULL DEFAULT GETDATE(),DueDate DATE NOT NULL,ReturnDate DATE NULL,Status NVARCHAR(50) NOT NULL DEFAULT 'Borrowed',FOREIGN KEY (memberID) REFERENCES members(memberID) ON DELETE CASCADE,FOREIGN KEY (book_ID) REFERENCES books(book_ID) ON DELETE CASCADE)
Alter table books ADD AvailableCopies int not null DEFAULT 1
Alter table books ADD TotalCopies int not null DEFAULT 1
Alter table books ADD Genre nvarchar(20)
ALTER TABLE  BorrowedBooks ADD BorrowedDate DATE NOT NULL DEFAULT GETDATE()
Alter table books

Alter table login Alter column Role nvarchar(50) null
Alter table login Alter column password nvarchar(100)
ALTER TABLE login ADD FailedAttempts INT NOT NULL DEFAULT 0;
ALTER TABLE login ADD IsLocked BIT NOT NULL DEFAULT 0;
ALTER TABLE login ADD LastFailedAttempt DATETIME NULL;

update books set AvailableCopies = TotalCopies

select*from login
delete from login where staffID = 20
update login set email = 'abdihakiim@gmail.com' where staffID = 23
delete from login where staffID=23


