create table authors(
`id` int unsigned not null auto_increment,
`firstName` nvarchar(64) not null,
`middleName` nvarchar(64),
`lastName` nvarchar(64) not null,

PRIMARY KEY (`id`)
);

CREATE INDEX idx_authors_lastName
ON authors (`lastName`);