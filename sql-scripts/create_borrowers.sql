create table borrowers(
`id` int unsigned not null auto_increment,
`email` nvarchar(255) not null unique,
`first_name` nvarchar(64) not null,
`last_name` nvarchar(64) not null,
`dob` date not null CHECK (`dob` between date '1900-01-01' and sysdate()),
`phone` nvarchar(10) not null,
`address_id` int unsigned not null,
`is_librarian` boolean default 'false',
`created_at` datetime not null,

primary key (`id`),
foreign key (`address_id`) references addresses (`id`)
);