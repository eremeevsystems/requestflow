# RequestFlow Mini

Небольшая система регистрации и исполнения заявок.

## Демонстрационный сценарий

Единственный сценарий, которого достаточно, чтобы показать реальную backend-разработку:

1. Пользователь создаёт заявку.
2. Руководитель назначает исполнителя.
3. Исполнитель видит заявку.
4. Исполнитель меняет статус на «В работе».
5. После выполнения переводит её в статус «Выполнена».
6. Система сохраняет историю изменений.

## Технологии

### Backend

- Java
- Spring Boot
- Hibernate
- PostgreSQL
- Flyway
- REST API
- Авторизация
- Роли пользователей
- OpenAPI/Swagger
- Docker Compose
- Unit- и integration-тесты

### Клиент

Небольшой клиент на C#:

- экран авторизации;
- список заявок;
- карточка заявки;
- смена статуса.

## Модель данных

Всего три основные сущности: `User`, `Request`, `RequestHistory`.

### Роли

- `MANAGER`
- `EXECUTOR`

### Заявка (Request)

| Поле         | Описание                    |
|--------------|-----------------------------|
| `id`         | Идентификатор               |
| `title`      | Заголовок                   |
| `description`| Описание                    |
| `priority`   | Приоритет                   |
| `status`     | Статус                      |
| `createdBy`  | Автор заявки                |
| `assignedTo` | Назначенный исполнитель     |
| `createdAt`  | Дата создания               |
| `updatedAt`  | Дата последнего обновления  |

### Статусы заявки

- `NEW`
- `ASSIGNED`
- `IN_PROGRESS`
- `COMPLETED`

## API

```
POST   /auth/login
POST   /requests
GET    /requests
GET    /requests/{id}
PATCH  /requests/{id}/assignee
PATCH  /requests/{id}/status
GET    /requests/{id}/history
```
