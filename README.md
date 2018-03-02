# EncryptedMessageBoard
Simple C# ASP.NET encrypted message board web API for Devlift job application. When a message is created or updated it is automatically encrypted with its own initialization vector. For simplicity's sake the encryption key is hardcoded into the program- ideally the encryption key should be isolated from source control in a secrets file or using a service like AWS KMS.

## Routes

**Create**:

Route: `http://localhost:56581/api/encryptedmessage`

JSON Params: `Author, Message`

HTTP Verb: `POST`

**List (Encrypted)**:

Route: `http://localhost:56581/api/encryptedmessage`

HTTP Verb: `GET`

**Read (Encrypted)**:

Route: `http://localhost:56581/api/encryptedmessage/{id}`

HTTP Verb: `GET`

**Read (Decrypted)**:

Route: `http://localhost:56581/api/encryptedmessage/{id}/decrypt`

HTTP Verb: `GET`

**Update**:

Route: `http://localhost:56581/api/encryptedmessage`

JSON Params: `Author, Message`

HTTP Verb: `PUT`

**Delete**:

Route: `http://localhost:56581/api/encryptedmessage/{id}`

HTTP Verb: `DELETE`
