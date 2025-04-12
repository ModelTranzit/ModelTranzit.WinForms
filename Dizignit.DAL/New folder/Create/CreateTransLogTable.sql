CREATE TABLE TransLog (
    id SERIAL PRIMARY KEY,
	UTCInsert TIMESTAMP,
	TransDateTime TIMESTAMP,
    data BYTEA
);

