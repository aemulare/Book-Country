create table loans(
`id` int unsigned not null auto_increment,
`borrowerId` int unsigned not null,
`bookId` int unsigned not null,
`reservedAt` datetime not null,
`issueDate` datetime null CHECK (`issueDate` > `reservedAt`),
`returnDate` datetime null CHECK (`returnDate` > `issueDate`),
`returnedOn` datetime null CHECK (`returnedOn` > `issueDate`),

PRIMARY KEY (`id`),
FOREIGN KEY (`borrowerId`) REFERENCES borrowers(`id`),
FOREIGN KEY (`bookId`) REFERENCES books(`id`)
);

CREATE INDEX idx_loans_borrowerId
ON loans (`borrowerId`);

CREATE INDEX idx_loans_bookId
ON loans (`bookId`);
