# chatRoomAPI

We're going to be building a basic chatroom

The concepts this project will cover are

1.SignalR Websocket Service

2..NET MVC

3.SqlSugar

#### System functional modules

Login module: Here, a simple and traditional Session method is used to maintain login status.

Chat management module: The core module of the system, which is mainly implemented using the SignalR WebSocket.

#### How To Use

1.git clone [GitHub - pizh-github/ChatRoomAPI](https://github.com/pizh-github/ChatRoomAPI.git)

2.modify DBConnect of appsettings.json

3.**Login**：https://localhost:7141/Home/LoginSubmit

    Parameters:userName,password

    After successful login, redirected to chat room(https://localhost:7141/Home/Index)

4.**Register**：https://localhost:7141/Home/Register

    Parameters:userName,password

     After successful Register, redirected to Login Page(https://localhost:7141/Home/Login

5.**LogOut**：https://localhost:7141/Home/LogOut

    clear Session and Cookies.
