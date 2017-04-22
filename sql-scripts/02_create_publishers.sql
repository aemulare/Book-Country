CREATE TABLE publishers(
`id` int unsigned not null auto_increment,
`name` nvarchar(128) not null,

PRIMARY KEY(`id`));

CREATE INDEX idx_publishers_name
ON publishers(`name`);