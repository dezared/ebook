# eBook - интернет ресурс о книгах
## Back End
Серверная часть приложения выполнена на .NET Core 7.0.
Для запуска приложения воспользуйтесь следующими командами:
    
    ../ebook-backend:$ docker-compose up -d
    
Данная команда запустит ваш проект в docker-compose системе. Сайт будет доуступен на 5001 порту, а так же по ссылке `http://localhost:5001/swagger` будет доступен Swagger Open API.

* В качестве СУБД выбрана `PostgreSQL`
* ORM: `Entity Framework`

## Fron End
Клиентская часть выполнена на ReactJS, Typescript, TailwindCSS, react-router-dom, Redux/RTK Query, MUI, PostCSS.

* Сборщик: `Vite`
* Линтер: `ESLent with Prettier`
* Авторизация: `JWT`
* Пакентный менеджер: `NPM`


Для запуска приложения вопсользуетесь следующими командами:

    # Для запуска:
    ../ebook-frontend:$ npm run dev
    # Линтировка и проверка типов TS:
    ../ebook-frontend:$ npm run lint
  
## TODO:
     
* FrontEnd:
    1. Доделать общий refresh-token когда сервер отдает `401`. ( на бэкенде система авторизации полностью готова )
    2. Унифицировать систему ошибок ( на бэкенде уже есть )
    3. Добавить систему авторов книг, их создания и редактирования ( на бэкенде уже готово )
* BackEnd:
    1. Сделать систему закладок, а так же возможность выставлять рейтинг.
    2. Починить `_api_annotation.xml` для большей документации в `swagger open api`.
    3. Добавить .env и .gitignore

## Authors

@DezareD / dezare3232@gmail.com