create table authors(
`id` int unsigned not null auto_increment,
`first_name` nvarchar(64) not null,
`last_name` nvarchar(64) not null,
PRIMARY KEY (`id`)
);