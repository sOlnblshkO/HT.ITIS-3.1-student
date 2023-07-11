# Домашняя работа №1

## Docker (dockerfile, слои, образ, контейнер, docker-compose)

### Если вдруг проспал, на семинаре было
1) Введение в работу с docker.
2) Рассказ про dockerfile.
3) Рассказ про образы, контейнеры, слои dockerfile.
4) Рассказ про docker-compose.

### Теория
1. [Про докер](https://learn.microsoft.com/ru-ru/dotnet/core/docker/build-container?tabs=windows) совсем с нуля
2. [Пример](https://learn.microsoft.com/ru-ru/aspnet/core/host-and-deploy/docker/building-net-docker-images?view=aspnetcore-7.0) многоэтапной сборки dockerfile
3. Про [настройку env variables](https://docs.docker.com/compose/environment-variables/set-environment-variables/)

### Вопросы к семинару
1) Что такое докерфайл, зачем он нужен?
2) Что такое слои, в чем их особенности?
3) Что такое образы, контейнеры?
4) Что такое docker-compose, зачем он нужен?
5) Что такое Environment, как разделить workflow приложения в зависимости от окружения?
6) Зачем нужен .env файл?

### Практика
1) Написать инструкции для создания образа в файле dockerfile в папке основного проекта.
2) Написать инструкции для создания контейнера образа основного проекта,
с помощью созданного ранее dockerfile, написать инструкции для создания контейнера с БД Postgres (все в docker-compose).
3) Вынести строки подключения из docker-compose в переменные окружения.


