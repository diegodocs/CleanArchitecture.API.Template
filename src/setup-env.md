#Create Apps Folder
```csharp
cd c:
mkdir Api-apps
```

#Pull Code from your repo:


```csharp
cd Api-apps
git clone {Application-Repo-Url}
git clean -x -d -f 
git reset --hard
git pull origin develop

cd .\src
```
#Run Backend App:
```csharp
dotnet run --project Api.Template.WebApi --console --environment "Development" --urls "http://*:5200"
```

#Create EfCore Migrations:
```csharp
dotnet ef migrations add InitialCreate
```