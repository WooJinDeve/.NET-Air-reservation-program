## 소개
- `항공 API`를 활용한 항공예매프로그램입니다.
  - `C#` + `WinForm`
  - `Sokect`
- **프로젝트 기간**
  - `2022-05-10 ~ 2022-05-12`

## 프로젝트 설정
- **Visual Studio 2022** 
  - **Download**
    - [https://visualstudio.microsoft.com/ko/vs/whatsnew/](https://visualstudio.microsoft.com/ko/vs/whatsnew/)
  - **Project Template**
    - `Client` : `WinForm`
       - `새 프로젝트 만들기` -> `Windows Form 앱(.NET Framwork)` 
    - `Server` : `Console App`
       - `새 프로젝트 만들기` -> `콘솔 앱(.NET Framwork)` 
  - **Open API**
    - [항공 API URL](https://www.data.go.kr/data/15000126/openapi.do)
 - **MSSQL DataBase**
   - **Download**
     - [https://www.microsoft.com/ko-kr/sql-server/sql-server-downloads](https://www.microsoft.com/ko-kr/sql-server/sql-server-downloads)

<details>
<summary><h2>파일 구조 </h2></summary>

``` 
C-Sharp-Air-reservation-program-main
│  DB.txt
│  README.md
├─client(manager)
│  │  MainForm.cs
│  │  MainForm.Designer.cs
│  │  MainForm.resx
│  │  Member.cs
│  │  Program.cs
│  ├─Control
│  │      WbControl.cs
│  ├─Data
│  │      Airport.cs
│  ├─Database
│  │      WbDB.cs
│  └─network
│          Packet.cs
│          WbClient.cs
├─client(user)
│  │  Program.cs
│  ├─Control
│  │      WbControl.cs
│  ├─Data
│  │      Airport.cs
│  │      Member.cs
│  ├─Form
│  │      CautionForm.cs
│  │      Confirm.cs
│  │      MainForm.cs
│  │      ProcessForm.cs
│  │      Reservation.cs
│  │      WbCrossThread.cs
│  ├─NetWork
│  │      Packet.cs
│  │      WbClient.cs
│  └─Resources
│          images.png
└─server
    │  Program.cs
    ├─Control
    │      Control.cs
    │      WbDocument.cs
    ├─Data
    │      AirPort.cs
    │      AirPortPasing.cs
    │      Member.cs
    ├─DataBase
    │      WbDB.cs
    │      WbQuery.cs
    └─Network
            Packet.cs
            WbServer.cs
```
</details>

<details>
<summary><h2>ERD </h2></summary>

![db](https://user-images.githubusercontent.com/106054507/183291416-510afd9c-e337-403c-8201-afa4670812de.PNG)
</details>

<details>
<summary><h2>프로그램</h2></summary>

#### 클라이언트(사용자)
![Main](https://user-images.githubusercontent.com/106054507/183290648-7a145831-da29-4e13-a811-dd6a86c3eba9.PNG)
#### 예약하기
![예약하기](https://user-images.githubusercontent.com/106054507/183290690-96681495-60a7-4b34-b11e-9708b0b9d52b.PNG)
#### 결제하기
![결제하기](https://user-images.githubusercontent.com/106054507/183290702-ef7a1483-9c10-47f1-818a-199e1221bed6.PNG)
#### 예약확인
![예약확인](https://user-images.githubusercontent.com/106054507/183290712-a398a4f6-9b4e-477c-bfec-8fb6ee2ce7a6.PNG)

#### 클라이언트(관리자)
![관리자 Main](https://user-images.githubusercontent.com/106054507/183290715-c8c40f99-2685-4f47-aacd-14e01fb47d75.PNG)

</details>
