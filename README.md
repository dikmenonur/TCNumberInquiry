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
{
  "hasError": true,
  "message": "string",
  "validationErrors": [
    {
      "key": "string",
      "errors": [
        "string"
      ]
    }
  ],
  "data": [
    {
      "id": 0,
      "identyNumber": 0,
      "firstName": "string",
      "lastName": "string",
      "birthDate": "2022-07-26T08:20:18.323Z"
    }
  ]
}
```


Yeni kullanıcı oluşturma sayfası
![image](https://user-images.githubusercontent.com/3075597/180967968-0432c79e-906b-42e4-b052-7f7f1bf5c7d8.png)

Kullanıcı Güncelleme sayfası
![image](https://user-images.githubusercontent.com/3075597/180968230-c67169de-9f9f-4b1b-b6aa-4b25205ff1b2.png)


