# CollegeDB Repository Project (.NET Framework 4.8)

Консольное приложение на C# для работы с SQL Server LocalDB через Repository pattern и Dapper.

## Содержимое
- `Models/Student.cs`, `Models/Group.cs` — модели.
- `Data/StudentRepository.cs` — `GetAll`, `GetById`, `CreateStudent(Student)`.
- `Data/GroupRepository.cs` — CRUD для групп.
- `sql/CollegeDB.sql` — создание базы `CollegeDB` и таблиц `Students`, `Subjects`, `Grades`, `Teachers`, `Groups`.
- `packages.config` — NuGet-пакет Dapper 2.1.66.
- `CollegeDB_Repository_Project.csproj` — проект Console App (.NET Framework 4.8).

## Настройка и запуск
1. Откройте `.csproj` в Visual Studio 2022.
2. Убедитесь, что установлен пакет разработки **.NET Framework 4.8**.
3. Восстановите NuGet-пакеты (правой кнопкой по Solution → Restore NuGet Packages). Если пакеты не восстановились автоматически, выполните в Package Manager Console: `Update-Package -reinstall`.
4. Выполните скрипт `sql/CollegeDB.sql` в SQL Server Management Studio или Visual Studio, подключившись к `(localdb)\MSSQLLocalDB`.
5. Проверьте строку подключения в `Program.cs`.
6. Запустите проект. Программа попросит имя, фамилию и необязательный ID существующей группы.

## Как работает CreateStudent
`CreateStudent(Student student)` вставляет данные параметризованным запросом, получает созданный `StudentId` с помощью `QuerySingle<int>` и записывает его обратно в переданный объект `Student`.
