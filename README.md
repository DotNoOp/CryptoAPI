# CryptoAPI
REST API backend for cryptography operations.

## Usage
To use, compile and run executable. You can now access the API over HTTP.

### Hashing
To hash a string, send JSON object `{"method":"sha256","contents":"aGVsbG8=","salt":"3q2+7w=="}`. Both contents and salt are base64 encoded byte array (in this case, `aGVsbG8=` is base64 encoded string `hello` and `3q2+7w==` is base64 encoded byte array `0xDEADBEEF`)
Server will reply with a JSON object `{"method":"sha256","contents":"fec5a7b4f37f65b902d803018e84cdcbea213fe7ec963db2ac5471a470845f6d","salt":"3q2+7w=="}`, where `method` will be the method you've chosen and `contents` will contain HEX string representation of a hash. `salt` field will contain base64 value of salt.

Salting is done by hashing "`contents``salt`", with no spaces or extra characters. If no salt is provided, no salt is used.

Supported methods are: md5, sha128, sha256, sha384, sha512

### Encryption
To encrypt a string, send JSON object `{"method":"tripledes","contents":"GLx1fqaOq8Q0DIHaKsLuTg==","key":"gxdgIWcYqrtoqRaFgxdgIWcYqrtoqRaF","operation":true}`
where method is one of supported methods: tripledes, aes256. Both contents and key are base64 encoded byte array, and `operation` should be `true` for encryption and `false` for decryption.
Server will reply with a JSON object `{"method": "tripledes","contents": "rQLz7KeFyhbtngQALm8kZQ==","salt": null,"key": "gxdgIWcYqrtoqRaFgxdgIWcYqrtoqRaF","operation": true,"dataLen": 16}`, where `method` will be the method you've chosen and `contents` will contain base64 string representation of encrypted content. Other fields will remain unchanged. `dataLen` shows the length of initial byte array passed as contents.

Supported methods are: aes256, tripledes, flawless

### Over TCP
Exactly the same as HTTP, but you connect to address defined in config and send your JSON object with `\r\n` appended to it. Server will reply accordingly, with a JSON object. Example request: `{"method":"sha256","contents":"aGVsbG8=","salt":"3q2+7w=="}\r\n` example response: `{"method":"sha256","contents":"fec5a7b4f37f65b902d803018e84cdcbea213fe7ec963db2ac5471a470845f6d","salt":"3q2+7w=="}\r\n`
