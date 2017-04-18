create table borrowers(
`id` int unsigned not null auto_increment,
`email` nvarchar(255) not null unique,
`first_name` nvarchar(64) not null,
`last_name` nvarchar(64) not null,
`dob` date not null CHECK (`dob` between date '1900-01-01' and sysdate()),
`phone` nvarchar(10) not null,
`address_line1` nvarchar(64) not null,
`address_line2` nvarchar(64) not null,
`city` nvarchar(64) not null,
`state` nvarchar(2) not null,
`zip` nvarchar(5) not null,
`created_at` datetime not null,
primary key (`id`)
);