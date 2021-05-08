
You can configure maximum file length limit to upload in
/UploadFilesServer/UploadFilesServer/appsettings.json "fileUploadRestrictions" \ "maxFileSizeInBytes"

You can configure allowed file types to upload in 
/UploadFilesServer/UploadFilesServer/appsettings.json "fileUploadRestrictions" \ "allowedFileTypes"

1- Open UploadFilesServer solution.
2- Run "Update-Database" command Visual Studio \ Nuget Package Manager Console. 
   (before that, configure mssql connection information in appsettings.json)
3- Run UploadFilesServer
4- Open UploadFilesClient
5- Run "npm i" in terminal
6- Run "npm install -g @angular/cli@latest" if angular does not exist in your system.
7- Run "ng serve -o" in terminal
8- Also UI can be tested via running "ng test" command
