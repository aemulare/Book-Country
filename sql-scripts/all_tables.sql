
#1 TABLE ADDRESSES
create table addresses(
`id` int unsigned not null auto_increment,
`addressLine1` nvarchar(64) not null,
`addressLine2` nvarchar(64) not null,
`city` nvarchar(64) not null,
`state` nvarchar(2) not null,
`zip` nvarchar(5) not null,

PRIMARY KEY (`id`)
);

load data local infile 'C:\\Users\\Maryika\\Documents\\GITHUB_CUNY\\BookCountry\\mock-data\\addresses.csv' 
into table addresses fields terminated by ',' lines terminated by '\r\n';


#2 TABLE PUBLISHERS
CREATE TABLE publishers(
`id` int unsigned not null auto_increment,
`name` nvarchar(128),

PRIMARY KEY(`id`));


load data local infile 'C:\\Users\\Maryika\\Documents\\GITHUB_CUNY\\BookCountry\\mock-data\\publishers.csv' 
into table publishers fields terminated by ',' lines terminated by '\r\n';


#3 TABLE AUTHORS
create table authors(
`id` int unsigned not null auto_increment,
`firstName` nvarchar(64) not null,
`middleName` nvarchar(64),
`lastName` nvarchar(64) not null,

PRIMARY KEY (`id`)
);


load data local infile 'C:\\Users\\Maryika\\Documents\\GITHUB_CUNY\\BookCountry\\mock-data\\authors.csv' 
into table authors fields terminated by ',' lines terminated by '\r\n';


#4 TABLE FORMATS
create table formats(
`id` int unsigned not null auto_increment,
`name` nvarchar(32),

PRIMARY KEY(`id`));


load data local infile 'C:\\Users\\Maryika\\Documents\\GITHUB_CUNY\\BookCountry\\mock-data\\formats.csv' 
into table formats fields terminated by ',' lines terminated by '\r\n';


#5 TABLE LANGUAGES
create table languages(
`id` int unsigned not null auto_increment,
`code` nvarchar(3) not null unique,
`name` nvarchar(64) not null unique,

PRIMARY KEY(`id`));

load data local infile 'C:\\Users\\Maryika\\Documents\\GITHUB_CUNY\\BookCountry\\mock-data\\languages.csv' 
into table languages fields terminated by ',' lines terminated by '\r\n';


#6 TABLE BORROWERS
create table borrowers(
`id` int unsigned not null auto_increment,
`email` nvarchar(255) not null unique,
`firstName` nvarchar(64) not null,
`lastName` nvarchar(64) not null,
`dob` date not null CHECK (`dob` between date '1900-01-01' and sysdate()),
`phone` nvarchar(10) not null,
`addressId` int unsigned not null,
`isLibrarian` int unsigned default 0,
`createdAt` datetime not null,

PRIMARY KEY (`id`),
FOREIGN KEY (`addressId`) REFERENCES addresses (`id`)
);


load data local infile 'C:\\Users\\Maryika\\Documents\\GITHUB_CUNY\\BookCountry\\mock-data\\borrowers.csv' 
into table borrowers fields terminated by ',' lines terminated by '\r\n';


#7 TABLE BOOKS
create table books(
`id` int unsigned not null auto_increment,
`title` nvarchar(255) not null,
`edition` nvarchar(32),
`publishedOn` date not null,
`publisherId` int unsigned not null,
`languageId` int unsigned not null default '1',
`formatId` int unsigned default '1',
`isbn` nvarchar(13) not null,
`deweyCode` nvarchar(15) not null,
`price` decimal(6,2),
`quantity` int unsigned,
`createdAt` DATETIME not null,

PRIMARY KEY (`id`),
FOREIGN KEY (`publisherId`) REFERENCES publishers(`id`),
FOREIGN KEY (`languageId`) REFERENCES languages(`id`),
FOREIGN KEY (`formatId`) REFERENCES formats(`id`)
);

load data local infile 'C:\\Users\\Maryika\\Documents\\GITHUB_CUNY\\BookCountry\\mock-data\\books.csv' 
into table books fields terminated by ',' lines terminated by '\r\n';


#8 BOOKS_AUTHORS
create table books_authors(
`id` int unsigned not null auto_increment,
`bookId` int unsigned not null,
`authorId` int unsigned not null,
`authorOrdinal` int unsigned,
`role` nvarchar(32) not null default 'author',

PRIMARY KEY (`id`),
FOREIGN KEY (`bookId`) REFERENCES books(`id`),
FOREIGN KEY (`authorId`) REFERENCES authors(`id`)
);

load data local infile 'C:\\Users\\Maryika\\Documents\\GITHUB_CUNY\\BookCountry\\mock-data\\books_authors.csv' 
into table books_authors fields terminated by ',' lines terminated by '\r\n';


#9 TABLE LOANS
create table loans(
`id` int unsigned not null,
`borrowerId` int unsigned not null,
`bookId` int unsigned not null,
`issueDate` datetime not null,
`returnDate` datetime not null,
`returnedOn` datetime,

PRIMARY KEY (`id`),
FOREIGN KEY (`borrowerId`) REFERENCES borrowers(`id`),
FOREIGN KEY (`bookId`) REFERENCES books(`id`)
);

load data local infile 'C:\\Users\\Maryika\\Documents\\GITHUB_CUNY\\BookCountry\\mock-data\\loans.csv' 
into table loans fields terminated by ',' lines terminated by '\r\n';
