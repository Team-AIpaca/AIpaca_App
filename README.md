# AIpaca_App
AIpaca App 저장소에 오신 것을 환영합니다. 

이 저장소에는 AIpaca 프로젝트의 앱 버전 코드가 포함되어있습니다. 

AIpaca 앱은 .NET MAUI로 작성되었습니다.

.NET MAUI(.NET 다중 플랫폼 앱 UI)은 C#과 XAML을 사용하여 네이티브 모바일 및 데스크톱 앱을 만들기 위한 플랫폼 간 프레임워크입니다.

.NET MAUI를 사용하여 단일 공유 코드 베이스에서 Android, iOS, macOS 및 Windows에서 실행할 수 있는 앱을 개발할 수 있습니다.

# 프로젝트 이름 : AIpaca_App
AIpaca 프로젝트의 앱 프론트엔드입니다.

# 프로젝트 구조
.NET MAUI로 작성되었으며, .NET 8.0버전을 사용했습니다.

- 📁 Alpaca_App (솔루션)
  - 📁 Properties
  - 📁 Platforms
    - 📁 Android
      - 📄 AndroidManifest.xml (앱 버전 관리 위치)
      - 📄 MainActivity.cs
      - 📄 MainApplication.cs
    - 📁 iOS
    - 📁 MacCatalyst
    - 📁 Tizen
    - 📁 Windows
  - 📁 Resources
    - 📁 AppIcon
    - 📁 Fonts
    - 📁 Images
    - 📁 Localization (다국어를 지원하기 위한 지역화)
    - 📁 Raw
    - 📁 Splash (앱 시작 시 스플래쉬 화면)
    - 📁 Styles
  - 📁 Views (앱의 페이지들)
    - 📄 ChallengePage.xaml
    - 📄 MainPage.xaml
    - 📄 RecordPage.xaml
    - 📄 RobotPage.xaml
    - 📄 SettingsPage.xaml
  - 📄 App.xaml (앱이 실행될때 필요한 설정들을 적용( ex) 다크모드 ))
  - 📄 AppShell.xaml (하단 네비게이션 바)
  - 📄 MauiProgram.cs

# 프로젝트 실행방법
Visual Studio 2022 .NET 8.0 이상을 지원하는 버전이 필요합니다.

프로젝트를 실행시키는 방법은 2가지가 있습니다.

> Visual Studio 에뮬레이터(Android)로 실행 :
- Visual Studio 상단 메뉴 - 도구 - Android - Android 디바이스 관리자 - 기기 설정
- 디버깅을 Android 기기로 실행 

> 앱을 빌드하여 직접 기기에 설치 :
- 앱을 빌드 후 해당 파일을 직접 기기에 설치


## License
이 프로젝트는 MIT License에 따라 라이센스가 부여됩니다. 자세한 내용은 License 파일을 참조하십시오.
