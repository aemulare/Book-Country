create table books_authors(
`id` int unsigned not null auto_increment,
`book_id` int unsigned not null,
`author_id` int unsigned not null,
`author_ordinal` int unsigned,
`role` nvarchar(32) not null default 'author',
primary key (`id`)
);