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