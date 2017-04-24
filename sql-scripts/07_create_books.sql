create table books(
`id` int unsigned not null auto_increment,
`title` nvarchar(255) not null,
`edition` nvarchar(32),
`publishedOn` date not null,
`publisherId` int unsigned not null,
`languageId` int unsigned not null default 1,
`formatId` int unsigned default 1,
`isbn` nvarchar(13) unique not null,
`deweyCode` nvarchar(15) not null,
`price` decimal(6,2),
`quantity` int unsigned not null default 1,
`createdAt` DATETIME not null default current_timestamp,
`cover` nvarchar(255),
`totalPages` int unsigned,

PRIMARY KEY (`id`),
FOREIGN KEY (`publisherId`) REFERENCES publishers(`id`),
FOREIGN KEY (`languageId`) REFERENCES languages(`id`),
FOREIGN KEY (`formatId`) REFERENCES formats(`id`)
);


CREATE INDEX idx_books_title
ON books (`title`);

CREATE INDEX idx_books_publisherId
ON books (`publisherId`);

CREATE INDEX idx_books_languageId
ON books (`languageId`);

CREATE INDEX idx_books_deweyCode
ON books (`deweyCode`);

CREATE UNIQUE INDEX idx_books_isbn
ON books (`isbn`);
