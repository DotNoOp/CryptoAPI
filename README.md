# CryptoAPI
REST API backend for cryptography.

## Usage
To use, compile and run executable. You can now access the API over HTTP.
To hash a string, send JSON object `{"method":"sha256","contents":"aGVsbG8="}`
where method is one of supported methods: sha256, sha1, md5, sha512. Contents are base64 encoded byte array (in this case, `aGVsbG8=` is base64 encoded string `hello`)
Server will reply with a JSON object `{"method":"sha256","contents":"2cf24dba5fb0a30e26e83b2ac5b9e29e1b161e5c1fa7425e73043362938b9824"}`, where `method` will be the method you've chosen and `contents` will contain HEX string representation of a hash.