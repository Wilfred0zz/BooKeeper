# BooKeeper

## Description
 This is a book store api where one can see books the authors and customers who have made purchases of the books.
 Please update the appsettings to match the password and user for your local database!!!
 LMK if you need me to add Database SQL here!
 
 ### Installation/Setup
 Please install the required packages to run API
 - Mircosoft.EntityFrameWorkCore NuGet Package
 - Microsoft.EntityFrameWorkCore.InMemory
 - Pomelo.EntityFrameWorkCore.MySql


## Schema 

### Models

#### POST/GET Author Table

| Property | Type | Description |
| :---: | :---: |  :---: | 
| AuthorId | *integer*| unique id for each author |
| FirstName | *string* | authors first name |
| LastName | *string* | authors last name |

#### POST/GET Books Table
| Property | Type | Description |
| :---: | :---: |  :---: | 
| BookId | *integer* | unique id assigned to each book in store |
| Title | *string* |  each books title |
| AuthorId | *integer* |  holds the author id that is linked with the book | 
| Price | *float* | the price the book is being sold at in this book store API |

#### POST/GET Customer Table

| Property | Type | Description |
| :---: | :---: |  :---: | 
| CustomerId | *integer*| unique id for each customer to identify them |
| FirstName | *string* | customers first name  |
| LastName | *string* | customers  last name |
| Email    | *string* | customers email for contacting |
| PhoneNumber | *string* | customers phone number for contacting |

#### POST/GET Purchases Table
| Property | Type | Description |
| :---: | :---: |  :---: | 
| PurchaseId | *integer* | unique id assigned to each purchase made |
| Date | *datetime* |  date when user placed a purchase |
| BookId | *integer* |  holds the unique book id that can be used to find book title and author | 
| CustomerId | *integer* | holds unique customer id that can be used to find customer that purchased book | 

### All End Points Available 
#### Books 
- GET: api/Books gets all books in api along with their information
- GET: api/Books/2 gets a book information by its id
- PUT: api/Books/5 update a certain book
- POST: api/Books used to add a book 
- DELETE: api/Books/2 used to delete a book from book store API
 
#### Purchase
- GET: api/Purchase gets all Purchases in api along with their information
- GET: api/Purchase/2 gets a Purchase information by its id
- PUT: api/Purchase/5 update a certain Purchase
- POST: api/Purchase used to add a Purchase 
- DELETE: api/Purchase/2 used to delete a Purchase from book store API

#### Customer
- GET: api/Customer gets all Customers in api along with their information
- GET: api/Customer/2 gets a Customer information by its id
- POST: api/Customer used to add a Customer 
- DELETE: api/Customer/2 used to delete a Customer from book store API

#### Author
- GET: api/Author gets all Authors in api along with their information
- GET: api/Author/2 gets a Author information by its id
- POST: api/Author used to add a Author 
- DELETE: api/Author/2 used to delete a Author from book store API

