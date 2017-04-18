create table books(
`id` int unsigned not null auto_increment,
`title` nvarchar(255) not null,
`edition` nvarchar(32),
`published_on` date not null,
`publisher_id` int unsigned not null,
`language_id` int unsigned not null default '1',
`format_id` int unsigned default '1',
`isbn` nvarchar(13) not null,
`dewey_code` nvarchar(15) not null,
`price` decimal(6,2),
`quantity` int unsigned,
`created_at` DATETIME not null,

PRIMARY KEY (`id`),
FOREIGN KEY (`publisher_id`) REFERENCES publishers(`id`),
FOREIGN KEY (`language_id`) REFERENCES languages(`id`),
FOREIGN KEY (`format_id`) REFERENCES formats(`id`)
);




