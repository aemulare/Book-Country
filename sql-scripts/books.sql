SELECT 	b.id, 
		b.title, 
        b.edition, 
        b.published_on, 
        publishers.publisher,
        languages.lang,
        formats.format,
        b.isbn,
        b.dewey_code,
        b.price,
        b.quantity,
        b.created_at
FROM (((books b
INNER JOIN publishers ON b.publisher_id = publishers.id)
INNER JOIN languages ON b.language_id = languages.id)
INNER JOIN formats ON b.format_id = formats.id);


