select isbn, title, first_name, last_name, author_ordinal
from books b
LEFT JOIN books_authors ba ON (b.id = ba.book_id)
LEFT JOIN authors a ON (ba.author_id = a.id)
order by isbn;







