#Create Apps Folder
cd c:
mkdir Api-apps

#Pull Code from Azure DevOps:
cd Api-apps

git clone {Application-Repo-Url}
git clean -x -d -f 
git reset --hard
git pull origin develop

cd .\src

#Run Backend App:
dotnet run --project Api.Template.WebApi --console --environment "Development" --urls "http://*:4201"

#Create EfCore Migrations:
dotnet ef migrations add InitialCreate