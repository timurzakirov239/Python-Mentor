# 🐍 Python Mentor

**Python Mentor** — образовательная веб-платформа для обучения школьников программированию на Python. Практические задания, теория, моментальная проверка кода и отслеживание прогресса — всё в одном месте.

## ✨ Возможности

- 📚 **Теория и уроки** — структурированные учебные материалы с поддержкой Markdown
- ⚡ **Практические задания** — реальные задачи с автоматической проверкой кода через sandbox (Judge0/Piston API)
- 📝 **Экзамены и тесты** — система тестирования с вопросами и вариантами ответов
- 📊 **Отслеживание прогресса** — индивидуальный прогресс, баллы и достижения
- 🏆 **Сертификаты** — именной сертификат за успешное прохождение курса (PDF)
- 👤 **Система аккаунтов** — регистрация, авторизация, верификация email, смена пароля

## 🛠 Технологический стек

| Компонент | Технология |
|---|---|
| Фреймворк | ASP.NET Core 8 (MVC) |
| Язык | C# |
| ORM | Entity Framework Core 9 |
| База данных | PostgreSQL |
| Аутентификация | ASP.NET Core Identity |
| Email | MailKit |
| Markdown-рендеринг | Markdig |
| Генерация PDF | Rotativa |
| Выполнение кода | Judge0 / Piston API |

## 📂 Структура проекта

```
EasyGram/
├── Controllers/       # Контроллеры (Account, Home, Lessons, Theory, Materials, Tasks, Exam)
├── Models/            # Модели данных (Users, Topic, Question, Lesson, Exam и др.)
├── Views/             # Представления (Razor Pages)
├── ViewModels/        # Модели представлений
├── Services/          # Сервисы (Email, Markdown, Judge0)
├── Data/              # Контекст базы данных (AppDbContext)
├── Migrations/        # Миграции Entity Framework
└── wwwroot/           # Статические файлы (CSS, JS, изображения)
```

## 🚀 Запуск проекта

### Требования

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [PostgreSQL](https://www.postgresql.org/download/)

### Установка и запуск

1. **Клонируйте репозиторий:**
   ```bash
   git clone https://github.com/timurzakirov239/Python-Mentor
   cd primer
   ```

2. **Настройте строку подключения к БД** в файле `EasyGram/appsettings.json`:
   ```json
   "ConnectionStrings": {
     "Default": "Host=localhost;Port=5432;Database=EasyGramDb;Username=postgres;Password=ваш_пароль"
   }
   ```

3. **Примените миграции:**
   ```bash
   dotnet ef database update --project EasyGram
   ```

4. **Запустите приложение:**
   ```bash
   dotnet run --project EasyGram
   ```

5. Откройте в браузере: `https://localhost:5001` или `http://localhost:5000`

## 📧 Настройка Email (опционально)

Для работы верификации email заполните секцию `Email` в `appsettings.json`:

```json
"Email": {
  "FromAddress": "your-email@example.com",
  "SmtpHost": "smtp.example.com",
  "SmtpPort": 465,
  "Username": "your-email@example.com",
  "Password": "your-password"
}
```

## 📄 Лицензия

Данный проект является учебным.
