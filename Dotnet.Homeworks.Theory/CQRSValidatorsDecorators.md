# Домашняя работа №7

## CQRS Validation

### Если вдруг проспал, на семинаре было
1) Для чего нужны декораторы?
2) Применяем декораторы для Railway-oriented programming в ООП языке.
3) Библиотека FluentValidation.

### Теория
1. [Паттерн декоратор](https://metanit.com/sharp/patterns/4.1.php).
2. [Документация](https://docs.fluentvalidation.net) по FluentValidation.
3. [О декораторах, сквозной функциональности, CQRS и слоеной архитектуре](https://habr.com/ru/articles/353258/)

### Практика
1. Реализовать все классы в папках **Users** и **UsersManagement** в проекте **Features**.
2. **Создать свой медиатор** в проекте **Dotnet.Homeworks.Mediator**, который реализует **IMediator**, и использовать его вместо MediatR в контроллерах. **Регистрировать медиатор через Helpers/ServiceCollectionExtensions**
3. Подключить **FluentValidation** и написать валидацию для реквестов, где это необходимо. Валидаторы хранить вместе с фичами.
4. Реализовать PermissionCheck, в котором метод CheckPermission будет проверять доступ к операции. Проверка проходит с помощью **IHttpContextAccessor**, проверяя Claims пользователя из HttpContext. Также необходимо регистрировать сервис через **PermissionDependencyInjection**. Классы проверки, наподобие с валидаторами, хранить вместе с фичами.
5. Создать декораторы, которые будут проверять права доступа к операции, а также проводить валидацию данных только для фичи **Users**, которая представляет операции пользователя к своему аккаунту. Нужно проверять ClaimTypes.NameIdentifier. Построить цепочку из декораторов с помощью наследования, вызов которых начинается с **CqrsDecorator**. Реквесты фичи Users наследуют IClientRequest.
6. Создать свой PipelineBehavior в директории *Dotnet.Homeworks.Features/Behaviors*, наподобие такого же от MediatR, который будет делать то же самое, что и декоратор, но для операций **UserManagement**. Доступ к операциям этой фичи имеют только администраторы. Необходимо проверять ClaimTypes.Role. Для проверки использовать **enum Roles** в *Dotnet.Homeworks.Infrastructure/Validation/PermissionChecker/Enums*
7. Убрать эндпоинт вызова RegistrationService и перенести вызов в хэндлер создания пользователя.  
8. Реализовать эндпоинты для UserManagementController.
