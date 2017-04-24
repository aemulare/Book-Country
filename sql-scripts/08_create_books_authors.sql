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

CREATE INDEX idx_books_authors_authorId
ON books_authors (`authorId`);

CREATE INDEX idx_books_authors_bookId
ON books_authors (`bookId`);