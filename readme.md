# eBook - интернет ресурс о книгах
## Back End
Серверная часть приложения выполнена на .NET Core 7.0.
Для запуска приложения воспользуйтесь следующими командами:
    
    ../ebook-backend:$ docker-compose up -d
    
Внимание! Не забудьте изначально сгенерировать автора в Swagger UI:

![image](https://user-images.githubusercontent.com/54406333/216990233-e9baf571-4512-45e9-8c4c-5f7dabd1e8cd.png)

И изменить это значение во всех файлах front-end проекта:

![image](https://user-images.githubusercontent.com/54406333/216990383-8da7e624-3e2f-448c-8477-596cb908fff9.png)

Т.к в далнейшем предполагается связь с авторами проекта, без этой переменной backEnd часть не даст создать книгу.

Данная команда запустит ваш проект в docker-compose системе. Сайт будет доуступен на 5001 порту, а так же по ссылке `http://localhost:5001/swagger` будет доступен Swagger Open API.

* В качестве СУБД выбрана `PostgreSQL`
* ORM: `Entity Framework`

## Front End
Клиентская часть выполнена на ReactJS, Typescript, TailwindCSS, react-router-dom, Redux/RTK Query, MUI, PostCSS.

Для входа воспользуйтесь следующими данными:

    email: admin@ebook.com
    password: root

* Сборщик: `Vite`
* Линтер: `ESLent with Prettier`
* Авторизация: `JWT`
* Пакентный менеджер: `NPM`


Для запуска приложения вопсользуетесь следующими командами:

    # Для запуска:
    ../ebook-frontend:$ npm i
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

## Images:

![image](https://user-images.githubusercontent.com/54406333/216989087-d28747b2-7c12-4b99-a324-7338cf7d0622.png)
![image](https://user-images.githubusercontent.com/54406333/216989177-a2d21fef-de95-4ba4-a661-19d5fbb84443.png)
![image](https://user-images.githubusercontent.com/54406333/216989235-b6699cb4-0aec-483c-b619-e4356cba298e.png)
![image](https://user-images.githubusercontent.com/54406333/216989292-50561b1b-61e8-46c5-b1d8-aa1bf9730f7b.png)
![image](https://user-images.githubusercontent.com/54406333/216989802-61b29d7b-fd18-4512-bceb-f55828151b5f.png)

