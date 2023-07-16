# Домашнее задание №5

## RabbitMQ

### Если вдруг проспал, на семинаре было
1. Рассказ про Message Broker, для чего он нужен.
2. Рассказ про то, что такое RabbitMQ.
3. Рассказ про Masstransit и про то, зачем его использовать.

### Теория
1. [RabbitMQ для докера: как скачать, запустить, настроить](https://habr.com/ru/companies/southbridge/articles/704208/)
1. [Простая статья о том, как быстро запустить RabbitMQ (без Masstransit)](https://habr.com/ru/articles/649915/)
2. [Как настроить Masstransit в ASP .NET проекте](https://www.youtube.com/watch?v=CTKWFMZVIWA&ab_channel=MilanJovanovi%C4%87)
3. [Крутое видео, на примере демонстрирующее всю мощь и пластичность абстракций Masstransit](https://www.youtube.com/watch?v=4FFYefcx4Bg&ab_channel=NickChapsas)

### Вопросы к семинару
1. Зачем нужны Message Broker'ы, если они только усложняют структуру проекта? Разве нельзя вызывать весь код просто напрямую?
2. Чем полезна библиотека Masstransit? Зачем её использовать, если можно работать с RabbitMQ напрямую?
3. Какие ещё Message Broker'ы существуют помимо RabbitMQ? Насколько сложнее/проще их использовать с Masstransit?

### Практика
1. Запустить RabbitMQ в докере.
2. Настроить систему pub/sub между проектом Main и Mailing.API с помощью библиотеки Masstransit и RabbitMQ. Main проект должен отправлять сообщения (через RabbitMQ) проекту Mailing.API, а он в свою очередь слушать их и при получении отправлять email с полученными данными.
