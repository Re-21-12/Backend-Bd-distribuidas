1. ```
    dotnet add package Pomelo.EntityFrameworkCore.MySql
    dotnet add package Microsoft.EntityFrameworkCore.Design

    ```
2. Scaffolding a docker
 ```
dotnet ef dbcontext scaffold "Server=localhost;Port=3306;Database=vuelos;User=root;Password=rootpass;" Pomelo.EntityFrameworkCore.MySql \
-o Models --context-dir Data --context AppDbContext --use-database-names --no-onconfiguring

```    
3. correr script