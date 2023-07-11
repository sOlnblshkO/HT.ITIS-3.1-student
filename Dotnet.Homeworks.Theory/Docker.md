# Домашняя работа №1

## Docker (dockerfile, слои, образ, контейнер, docker-compose)

### Если вдруг проспал, на семинаре было
1) Введение в работу с docker.
2) Рассказ про dockerfile.
3) Рассказ про образы, контейнеры, слои dockerfile.
4) Рассказ про docker-compose.

### Теория
1. Про докер совсем [с нуля](https://habr.com/ru/articles/346634/)
2. Пример многоэтапной сборки [dockerfile](https://habr.com/ru/articles/713942/)
3. Про [docker-compose](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/multi-container-microservice-net-applications/multi-container-applications-docker-compose)

### Вопросы к семинару
1) Что такое докерфайл, зачем он нужен?
2) Что такое слои, в чем их особенности?
3) Что такое образы, контейнеры?
4) Что такое docker-compose, зачем он нужен?

### Практика
1) Написать инструкции для создания образа в файле dockerfile в папке основного проекта.
2) Написать инструкции для создания контейнера образа основного проекта,
с помощью созданного ранее dockerfile, написать инструкции для создания контейнера с БД Postgres (все в docker-compose).
3) Добиться успешной сборки БД и проекта внутри контейнеров.
4) Поменять строку подключения в соответствующем месте.


