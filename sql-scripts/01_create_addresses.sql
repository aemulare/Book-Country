create table addresses(
`id` int unsigned not null auto_increment,
`addressLine1` nvarchar(64) not null,
`addressLine2` nvarchar(64),
`city` nvarchar(64) not null,
`state` nvarchar(2) not null,
`zip` nvarchar(5) not null,

PRIMARY KEY (`id`)
);
