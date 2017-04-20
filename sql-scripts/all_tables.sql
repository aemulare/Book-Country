
#1 ADDRESSES
create table addresses(
`id` int unsigned not null auto_increment,
`addressLine1` nvarchar(64) not null,
`addressLine2` nvarchar(64),
`city` nvarchar(64) not null,
`state` nvarchar(2) not null,
`zip` nvarchar(5) not null,

PRIMARY KEY (`id`)
);


#2 PUBLISHERS
CREATE TABLE publishers(
`id` int unsigned not null auto_increment,
`name` nvarchar(128) not null,

PRIMARY KEY(`id`));



#3 AUTHORS
create table authors(
`id` int unsigned not null auto_increment,
`firstName` nvarchar(64) not null,
`middleName` nvarchar(64),
`lastName` nvarchar(64) not null,

PRIMARY KEY (`id`)
);



#4 FORMATS
create table formats(
`id` int unsigned not null auto_increment,
`name` nvarchar(32),

PRIMARY KEY(`id`));



#5 LANGUAGES
create table languages(
`id` int unsigned not null auto_increment,
`code` nvarchar(3) not null unique,
`name` nvarchar(64) not null unique,

PRIMARY KEY(`id`));



#6 BORROWERS
create table borrowers(
`id` int unsigned not null auto_increment,
`email` nvarchar(255) not null unique,
`firstName` nvarchar(64) not null,
`lastName` nvarchar(64) not null,
`dob` date not null CHECK (`dob` between date '1900-01-01' and sysdate()),
`phone` nvarchar(15) not null,
`addressId` int unsigned not null,
`isLibrarian` boolean default false,
`createdAt` datetime not null,

PRIMARY KEY (`id`),
FOREIGN KEY (`addressId`) REFERENCES addresses (`id`)
);



#7 BOOKS
create table books(
`id` int unsigned not null auto_increment,
`title` nvarchar(255) not null,
`edition` nvarchar(32),
`publishedOn` date not null,
`publisherId` int unsigned not null,
`languageId` int unsigned not null default 1,
`formatId` int unsigned default 1,
`isbn` nvarchar(13) not null,
`deweyCode` nvarchar(15) not null,
`price` decimal(6,2),
`quantity` int unsigned not null default 1,
`createdAt` DATETIME not null default current_timestamp,

PRIMARY KEY (`id`),
FOREIGN KEY (`publisherId`) REFERENCES publishers(`id`),
FOREIGN KEY (`languageId`) REFERENCES languages(`id`),
FOREIGN KEY (`formatId`) REFERENCES formats(`id`)
);



#8 BOOKS_AUTHORS
create table books_authors(
`id` int unsigned not null auto_increment,
`bookId` int unsigned not null,
`authorId` int unsigned not null,
`authorOrdinal` int unsigned not null default 1,
`role` nvarchar(32) not null default 'author',

PRIMARY KEY (`id`),
FOREIGN KEY (`bookId`) REFERENCES books(`id`),
FOREIGN KEY (`authorId`) REFERENCES authors(`id`)
);




#9 LOANS
create table loans(
`id` int unsigned not null,
`borrowerId` int unsigned not null,
`bookId` int unsigned not null,
`issueDate` datetime not null,
`returnDate` datetime not null CHECK (`returnDate` >= `issueDate`),
`returnedOn` datetime CHECK (`returnedOn` >= `issueDate`),

PRIMARY KEY (`id`),
FOREIGN KEY (`borrowerId`) REFERENCES borrowers(`id`),
FOREIGN KEY (`bookId`) REFERENCES books(`id`)
);

