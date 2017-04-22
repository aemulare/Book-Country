select b.id as book_id, isbn, title, lastName, firstName, middleName, ba.authorId, authorOrdinal
from books b
LEFT JOIN books_authors ba ON (b.id = ba.bookId)
LEFT JOIN authors a ON (ba.authorId = a.id)
order by isbn, authorOrdinal;



SELECT 	ba.id, 
		ba.bookId, 
        b.title, 
        b.isbn, 
        ba.authorId,
        #a.firstName,
        #a.middleName,
        #a.lastName,
        CONCAT_WS(' ', a.lastName, a.firstName, a.middleName) AS author_last_first_middle,
        ba.authorOrdinal
FROM books_authors ba
INNER JOIN books b ON ba.bookId = b.id
INNER JOIN authors a ON ba.authorId = a.id
ORDER BY isbn, authorOrdinal;


SELECT a.id, 
	   a.firstName, 
       a.middleName, 
       a.lastName,
	   ba.authorOrdinal,
       ba.authorId,
       ba.bookId,
       ba.role
FROM authors a
INNER JOIN books_authors ba ON a.id = ba.authorId
INNER JOIN books b ON b.id = ba.bookId;







