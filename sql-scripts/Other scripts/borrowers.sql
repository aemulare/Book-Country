SELECT br.id, br.email, br.firstName, br.lastName, br.dob, br.phone, br.addressId, br.isLibrarian, br.createdAt,
a.addressLine1, a.addressLine2, a.city, a.state, a.zip, br.createdAt
FROM borrowers br
INNER JOIN addresses a ON a.id = br.addressId;