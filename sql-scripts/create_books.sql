create table books(
`id` int unsigned not null auto_increment,
`author_id` int unsigned not null,
`title` nvarchar(255) not null,
`published_on` date not null,
`publisher` nvarchar(128) not null,
`language` nvarchar(30),
`binding_format` boolean default '0',
`isbn` varchar(13) not null,
`price` decimal(6,2),
`dewey_code` nvarchar(15),
`created_at` DATETIME,
PRIMARY KEY (`id`)
FOREIGN KEY (`author_id`) REFERENCES authors(`id`)
);




