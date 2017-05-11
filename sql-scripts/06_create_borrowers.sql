create table borrowers(
`id` int unsigned not null auto_increment,
`email` nvarchar(255) not null unique,
`firstName` nvarchar(64),
`lastName` nvarchar(64),
`dob` date CHECK (`dob` between date '1900-01-01' and sysdate()),
`phone` nvarchar(15),
`addressId` int unsigned,
`isLibrarian` boolean default false,
`createdAt` datetime not null,

`passwordDigest` nvarchar(255) not null,
`activationToken` nvarchar(255),
`active` boolean not null default false,

PRIMARY KEY (`id`),
FOREIGN KEY (`addressId`) REFERENCES addresses (`id`)
);


CREATE INDEX idx_borrowers_email
ON borrowers (`email`);

CREATE INDEX idx_borrowers_lastName
ON borrowers (`lastName`);

