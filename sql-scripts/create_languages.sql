create table languages(
`id` int unsigned not null auto_increment,
`code` nvarchar(3) not null unique,
`language` nvarchar(64) not null unique,
primary key(`id`));