create table loans(
`id` int unsigned not null,
`borrower_id` int unsigned not null,
`book_id` int unsigned not null,
`issue_date` datetime not null,
`return_date` datetime not null,
`returned_on` datetime,
primary key (`id`)
);