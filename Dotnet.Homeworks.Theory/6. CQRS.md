# Домашняя работа №6

## CQRS 

### Если вдруг проспал, на семинаре было
1) Что такое CQRS? 
2) Что такое Vertical Slices?
3) Как избавиться от спагетти-кода?
4) Обработка ошибок с помощью Railway oriented programming.

### Теория
1. [Быстрорастворимое проектирование](https://habr.com/ru/companies/jugru/articles/447308/) 
2. [CQRS. Факты и заблуждения](https://habr.com/ru/articles/347908/)
3. [Знакомство](https://www.youtube.com/watch?v=ykC3Ty-3U7g&t) с MediatR

### Вопросы к семинару
1) Что такое CQRS? Зачем он нужен?
2) В чем отличие Query от Command?
3) Как избавиться от спаггети кода и создать pipeline обработки реквеста, логирования и т.д.?
4) Что такое Vertical Slices? В чем плюсы и минусы Vertical Slices?

### Практика
1) Реализовать commands, queries и handlers в проекте Features от соответствующих интерфейсов.
2) Создать crud операции в контроллере ProductManagementController, который вызывает commands и queries.