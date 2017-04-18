create table authors(
`id` int unsigned not null auto_increment,
`first_name` nvarchar(64) not null,
`middle_name` nvarchar(64),
`last_name` nvarchar(64) not null,
PRIMARY KEY (`id`)
);