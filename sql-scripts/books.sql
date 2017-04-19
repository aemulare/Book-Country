SELECT 	b.id, 
		b.title, 
        b.edition, 
        b.publishedOn, 
        publishers.name,
        languages.name,
        formats.name,
        b.isbn,
        b.deweyCode,
        b.price,
        b.quantity,
        b.createdAt
FROM books b
INNER JOIN publishers ON b.publisherId = publishers.id
INNER JOIN languages ON b.languageId = languages.id
INNER JOIN formats ON b.formatId = formats.id;

