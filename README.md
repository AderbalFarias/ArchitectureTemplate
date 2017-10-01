# ArchitectureTemplate

This repository implement a template of architecture with Technologies like:
- C#
- ASP.NET MVC 5
- Entity Framework 6 CodeFirst
- Dapper 
- FluentApi
- Inversion of control (IoC)
- Dependency Injection (DI) 
- Application in layers
- Simple Injector as container of DI
- AutoMapper
- Linq Lambda Expressions
- Layered Architecture
- WEB Api
- Windows Service
- WCF

## Configuration

For this project to work on your machine, you have to replace the line below on file Web.config at project ArchitectureTemplate.Mvc.

```xml
<add name="EntityContext" connectionString="Data Source=yourserver; initial catalog=DbName;user id=youruser;password=yourpassword;" providerName="System.Data.SqlClient" />
```

You have to replace these words:<br />
**yourserver:** server name where the application will create the database<br />
**youruser:** your username to access the server<br />
**yourpassword:** your password to access the server<br />

The application contains an initialization file that will create the default user.<br />
**User: admin**<br />
**Password: 123456**<br />

If you want, you can configure SMTP server to send email on file Web.config as well in these keys.

```xml
<add key="SMTPServer" value="server" />
<add key="SMTPPort" value="587" />
<add key="SMTPUser" value="user" />
<add key="SMTPPassword" value="pass" />
```
