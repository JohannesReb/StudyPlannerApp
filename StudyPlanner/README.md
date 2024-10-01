## Useful commands in .net console CLI

Install tooling

~~~bash
dotnet tool update -g dotnet-ef
dotnet tool update -g dotnet-aspnet-codegenerator
~~~

## EF Core migrations

Run from solution folder  

~~~bash
dotnet ef migrations --project App.DAL.EF --startup-project WebApp add InitialCreate
dotnet ef database   --project App.DAL.EF --startup-project WebApp update
dotnet ef database   --project App.DAL.EF --startup-project WebApp drop
~~~


## MVC controllers

Install from nuget:  
- Microsoft.VisualStudio.Web.CodeGeneration.Design
- Microsoft.EntityFrameworkCore.SqlServer


Run from WebApp folder!  
Admin controllers
~~~bash
cd WebApp

dotnet aspnet-codegenerator controller -name CurriculumsController        -actions -m  App.Domain.DbEntities.Curriculum        -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name EwentsController        -actions -m  App.Domain.DbEntities.Ewent        -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name SubjectController        -actions -m  App.Domain.DbEntities.Subject        -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name TimeWindowsController        -actions -m  App.Domain.DbEntities.TimeWindow        -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name WorkTasksController        -actions -m  App.Domain.DbEntities.WorkTask        -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name EwentRolesController        -actions -m  App.Domain.ManyToMany.EwentRole        -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name SubjectRolesController        -actions -m  App.Domain.ManyToMany.SubjectRole        -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name UserCurriculumsController        -actions -m  App.Domain.ManyToMany.UserCurriculum        -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name UserEwentsController        -actions -m  App.Domain.ManyToMany.UserEwent        -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name UserFieldsController        -actions -m  App.Domain.ManyToMany.UserField        -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name UserSubjectsController        -actions -m  App.Domain.ManyToMany.UserSubject        -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name UserWorkTasksController        -actions -m  App.Domain.ManyToMany.UserWorkTask        -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name WorkTaskRolesController        -actions -m  App.Domain.ManyToMany.WorkTaskRole        -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name WorkTaskTimeWindowsController        -actions -m  App.Domain.ManyToMany.WorkTaskTimeWindow        -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ModulesController        -actions -m  App.Domain.DbEntities.Module        -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

cd ..

dotnet aspnet-codegenerator controller -name UsersController        -actions -m  App.Domain.Identity.User        -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

~~~

Public controllers
~~~bash
cd WebApp

dotnet aspnet-codegenerator controller -name CalendarController        -actions -m  App.Domain.DbEntities.Ewent        -dc AppDbContext -outDir Areas/Public/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name SubjectsController        -actions -m  App.Domain.DbEntities.Subject        -dc AppDbContext -outDir Areas/Public/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name TimeTableController        -actions -m  App.Domain.DbEntities.TimeWindow        -dc AppDbContext -outDir Areas/Public/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name StatisticsController        -actions -m  App.Domain.DbEntities.WorkTask        -dc AppDbContext -outDir Areas/Public/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

cd ..
~~~


Api controllers
~~~bash
dotnet aspnet-codegenerator controller -name SubjectsController  -m  App.Domain.DbEntities.Subject -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name WorkTasksController  -m  App.Domain.DbEntities.WorkTask -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name CurriculumsController  -m  App.Domain.DbEntities.Curriculum -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name TimeWindowsController  -m  App.Domain.DbEntities.TimeWindow -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name EwentsController  -m  App.Domain.DbEntities.Ewent -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name ModulesController  -m  App.Domain.DbEntities.Module -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f

~~~

## Docker

~~~bash
docker build -t webapp:latest .

docker buildx build --progress=plain --force-rm --push -t johannes2410/webapp:latest .

# multiplatform build on apple silicon
# https://docs.docker.com/build/building/multi-platform/
docker buildx create --name mybuilder --bootstrap --use
docker buildx build --platform linux/amd64 -t akaver/webapp:latest --push .


~~~

