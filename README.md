# CryptoAPI
REST API backend for cryptography.

## Usage
To use, compile and run executable. You can now access the API over HTTP.
To hash a string, send JSON object `{"method":"sha256","contents":"aGVsbG8="}`
where method is one of supported methods: sha256, sha1, md5, sha512. Contents are base64 encoded byte array (in this case, `aGVsbG8=` is base64 encoded string `hello`)
Server reply will contain hashed string.