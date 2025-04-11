dotnet ef migrations add AutoMigration_$(Get-Date -Format "yyyyMMdd_HHmmss")
dotnet ef database update