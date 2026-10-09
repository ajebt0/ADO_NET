Задание 8 — ADO.NET, паттерн Repository
=======================================

Содержимое:
- Models/Group.cs — модель группы с переопределённым ToString().
- Data/GroupRepository.cs — получение списка групп, добавление и удаление.
- Models/Student.cs и Data/StudentRepository.cs — пример модели и репозитория студентов.
- Program.cs — консольное меню для работы с группами.
- ADO_NET_Lesson08.csproj — проект .NET 9 с пакетом Microsoft.Data.SqlClient.

Как открыть:
1. Распакуйте архив.
2. Откройте ADO_NET_Lesson08.csproj в Visual Studio 2022.
3. Дождитесь восстановления NuGet-пакетов.
4. Проверьте строку подключения в Program.cs.
5. Запустите проект (Ctrl+F5).

Строка подключения по умолчанию:
Server=localhost;Database=CollegeDB;Trusted_Connection=True;TrustServerCertificate=True;

Если SQL Server использует другое имя экземпляра, замените localhost на имя из вашей
строки подключения, например .\\SQLEXPRESS или имя сервера из Visual Studio.

Важно:
- Предполагается, что dbo.Groups содержит столбцы Id (int, identity) и Name (nvarchar).
- В StudentRepository предполагаются столбцы Id, FirstName и LastName. Если в вашей
  таблице Students другие названия столбцов, этот пример нужно подстроить.
- Если таблица Groups связана внешними ключами с другими таблицами, SQL Server может
  не разрешить удаление группы, пока на неё ссылаются записи.
