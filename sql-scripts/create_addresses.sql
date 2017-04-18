create table addresses(
`id` int unsigned not null auto_increment,
`address_line1` nvarchar(64) not null,
`address_line2` nvarchar(64) not null,
`city` nvarchar(64) not null,
`state` nvarchar(2) not null,
`zip` nvarchar(5) not null,
primary key (`id`)
);