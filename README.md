# EasyEnglish School Management System
## How It Works
- Guest visitors: 
  - browse courses of Easy Eanglish
  - read messages
- Logged Student:
  - can review its portfolio
  - can review its active courses
- Teacher (user role):
  - creates/edits/delete course
  - add/removes students to/from course
  - add student's portfolio 
  - send emails to its students
  - add/download/delete resources
  - add messages 
  - can review his own courses
- Manager (user role):
  - can review all courses
  - can review all students
- Admin (user role):
  - reviews/creates/edits/deletes base data like language, level, training form, course-type

## Built with
- ASP.NET Core 6.0
- Entity Framework (EF) Core 6.0.3
- Microsoft SQL Server Express
- ASP.NET Identity System
- MVC Areas with Multiple Layouts
- Razor Pages, Sections, Partial Views
- View Components
- Repository Pattern
- Auto Мapping
- Dependency Injection
- Status Code Pages Middleware
- Exception Handling Middleware
- Sorting, Filtering, and Paging with EF Core
- Data Validation, both Client-side and Server-side
- Data Validation in the Models and Input View Models
- Custom Validation Attributes
- Seeding data
- Responsive Design
- Cloudinary
- SendGrid
- Tpastr
- Fontawesome
- Bootstrap
- jQuery
## Test Accounts
https://easylanguage.azurewebsites.net

**Admin:** admin@easyenglish.com 123123

**Manager:** manager@easyenglish.com 123123

**Teacher:** teacher1@easyenglish.com 123123

**Student:** student1@easyenglish.com 123123

**Additionally:**
in `appsettings.json` set your connection string, cloudinary and sendGrid keys, 
```json
"ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=EasyEnglish;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  "SendGrid": {
    "ApiKey": "SendGridApiKey"
  },
  "Cloudinary": {
    "ApiName": "cloudName",
    "ApiKey": "ApiKey",
    "ApiSecret": "ApiSecret"
  }
  ```
## Screenshots
![image](https://user-images.githubusercontent.com/81183265/166061256-649b83a7-95e1-458f-b016-3aca174094a8.png)
![image](https://user-images.githubusercontent.com/81183265/166007884-526761c7-f133-4779-b3e1-8df3d47b9220.png)
![image](https://user-images.githubusercontent.com/81183265/166061859-0b838b5d-b1d8-4a50-b3c1-0e089af7034e.png)
![image](https://user-images.githubusercontent.com/81183265/166061933-c8cd13dc-66e9-4ece-ab4a-34cea36482bf.png)
![image](https://user-images.githubusercontent.com/81183265/166062043-fcba943e-51e1-4f3b-8163-78b2b102e40b.png)
![image](https://user-images.githubusercontent.com/81183265/166009602-9a267fa6-a3b2-497d-94df-0ff2da6eb368.png)

## Credits
ASP.NET Core Template - [Nikolay Kostov](https://github.com/NikolayIT), [Stoyan Shopov](https://github.com/StoyanShopov), [Vladislav Karamfilov](https://github.com/vladislav-karamfilov)

Project images - [freepik.com](https://www.freepik.com/home)

Admin Area Template - [AdminLTE](https://adminlte.io/)
