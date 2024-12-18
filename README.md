# Journal


Send all request here ```http://svalmazchecks1-001-site1.ntempurl.com```

### 1. Backend for Journal app  (C#)

#### Dependency
- **.NET 6 or higher**
  


#### Url Method: POST/api/register

Request
```json
//Request body
{
   "email": "string",
  "password": "string"
}
```
Response

```json
//Json Result 200 (successful)
{
   "Message" : "Success"
  
}

//Json Result 200 (failed)
{
  "status": "failed",
  "message": "err message."
}
```
#### Url Method: POST/api/login

Request
```json
//Request body
{
   "email": "string",
  "password": "string"
}
```
Response

```json
//Json Result 200 (successful)
{
   "Message" : "Success"
  
}

//Json Result 200 (failed)
{
  "status": "failed",
  "message": "err message."
}
```
#### Url Method: POST/api/createEntry

Request
```json
// Request body
{
  "id": 0,
  "title": "string",
  "text": "string",
  "createdAt": "2024-12-13T14:54:22.441Z",
  "lastEditedDate": "2024-12-13T14:54:22.441Z"
}
```
Response

```json
//Json Result 200 (successful)
{
   "Message" : "Success"
  
}

//Json Result 200 (failed)
{
  "status": "failed",
  "message": "err message."
}
```
#### Url Method: PATCH/api/updateEntry

Request
```json
// Request body
{
  "id": 0,
  "title": "string",
  "text": "string",
  "createdAt": "2024-12-13T14:54:22.441Z",
  "lastEditedDate": "2024-12-13T14:54:22.441Z"
}
```
Response

```json
//Json Result 200 (successful)
{
   "Message" : "Entry updated successfully"
  
}

//Json Result 200 (failed)
{
  "status": "failed",
  "message": "err message."
}
```
#### Url Method: DELETE /api/deleteEntry

Request
```json
// Request body
{
  "entryId": 0
}
```
Response

```json
// Json Result 200 (successful)
{
   "message": "Entry deleted successfully."
}

// Json Result 400 (failed)
{
  "status": "failed",
  "message": "Error deleting entry."
}
```
#### Url Method: POST /api/upload

```json
// Form-data
{
  "image": "file" // binary data
}

// Request body
{
  "entryId": 0
}
```
Response

```json
// Json Result 200 (successful)
{
  "message": "Image uploaded successfully.",
}

// Json Result 400 (failed)
{
  "status": "failed",
  
}
```
#### Url Method: GET /api/{id}

```
api/yourId (example api/1)
```
Response

```json
// Json Result 200 (successful)
{
   "imageUrl": "string"
}

// Json Result 400 (failed)
{
  "status": "failed",

}
```


