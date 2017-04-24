create table loans(
`id` int unsigned not null,
`borrowerId` int unsigned not null,
`bookId` int unsigned not null,
`issueDate` datetime not null,
`returnDate` datetime not null CHECK (`returnDate` > `issueDate`),
`returnedOn` datetime CHECK (`returnedOn` > `issueDate`),

PRIMARY KEY (`id`),
FOREIGN KEY (`borrowerId`) REFERENCES borrowers(`id`),
FOREIGN KEY (`bookId`) REFERENCES books(`id`)
);

CREATE INDEX idx_loans_borrowerId
ON loans (`borrowerId`);

CREATE INDEX idx_loans_bookId
ON loans (`bookId`);