SELECT 	b.id, 
		b.title, 
        b.edition, 
        b.publishedOn, 
        p.name,
        l.name,
        f.name,
        b.isbn,
        b.deweyCode,
        b.price,
        b.quantity,
        b.createdAt
FROM books b
INNER JOIN publishers p ON b.publisherId = p.id
INNER JOIN languages l ON b.languageId = l.id
INNER JOIN formats f ON b.formatId = f.id;

