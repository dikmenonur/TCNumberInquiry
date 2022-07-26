# TCNumberInquiry

Projede Kullanılan Teknolojiler.

DB olarak, PostgreSQL  Entity Framework Core kullanıldı.
Swagger UI kullıldı.
Redis Cache için kullanılacak.

Docker Container kullanılacak, bunun için ilk oluşturnada bu adım seçildi.

Net Core 5 api ve diğer katmanlar için kullanıldı.


DB Schame

| Id     | IdentyNumber | FirstName | LastName |BirthDate |IsActive |
| :---   |    :---:     |      ---: | ---:     |     ---: |    ---: |
| 1      | number       | string     | string     | datetime | boolen |   
| 2      | 23123123       | Osman     | Sağ     | 01-02-1984     | true  |       




![image](https://user-images.githubusercontent.com/3075597/180801000-ccecf74f-39c5-4cba-9761-98fc1a355f1b.png)


Swagger Services Method
![image](https://user-images.githubusercontent.com/3075597/180955492-5e4e2133-32bc-4bde-9308-b8a5f63eee5c.png)

User List Schema
```json
UserListApiResponse{
hasError	boolean
readOnly: true
message	string
nullable: true
readOnly: true
validationErrors	[
nullable: true
ValidationErrorMessage{
key	string
nullable: true
errors	[...]
}]
data	[
nullable: true
User{
id	integer($int64)
identyNumber	integer($int64)
firstName	string
nullable: true
lastName	string
nullable: true
birthDate	string($date-time)
}]
}
```
