# Hanium_VR 개발 명세 
--------------

## 개발 환경
- 사용 언어: C#
- 게임 개발 프레임워크: Unity
    - Unity 버전: 2020.3.18f1 

## 개발 프로젝트 내용
- 초보 운전자를 위해 사고가 많이 발생하는 상황을 VR 시뮬레이션으로 구현하여 운전 연습을 가능하도록 함.
- 일반 운전, 어린이 보호 구역, 산간 도로, 주차장 총 4가지의 상황을 각각 3단계의 난이도로 구현 
- 각 난이도 별 운전 맵에서 시간 또한 선택 가능: 낮과 밤 둘 중 하나 선택 
- 돌발 상황 대비를 위해 각 맵 별 이벤트는 랜덤으로 발생 
    - 각 맵 별 랜덤 이벤트는 다음과 같음 (** 추가 요망)

## 사용 Package
#### 계정별로 다운 받아야할 Asset 목록이 다름.
#### Unity Asset Store에서 다운 
- lhwool0915@gmail.com
    - Clean Vector Icons: navigation을 위한 패키지
    - VR Keyboard: VR을 통해 입력을 받기 위한 패키지
    - Logitech Gaming SDK: 로지텍 운전 기기 연결을 위한 패키지
    - Human Characters: 이벤트 처리에서 움직이는 사람을 위한 사람 패키지
    - City Builder: 전체적인 도로 Scene을 구성하기 위한 패캐지, * 주요 패키지
    - ARCADE: Free Racing Car: 이벤트 처리의 움직이는 자동차를 위한 자동차 패키지
    - UI Samples: UI를 위한 패키지
    - Unity-Chan! Model: 사람 애니메이션을 위한 패키지
- yhs06072@uos.ac.kr 
    - Mountain Road Pack: 산간 도로를 위한 패키지
    - Animating Traffic Lights: 신호등을 위한 패키지
    - White Rabbit: 동물 패키지
    - Toon Fox: 동물 패키지
    - Free chibi cat: 동물 패키지
    - Free Music Tracks For Games: 게임 배경 음악을 위한 패키지
    - Sport Car - 3D model: 자동차 빛을 위한 패키지
    - Whit & Black GUI by Gamertose
- 그 외의 Unity Package Manager 자체에서 바로 다운 
    - XR toolkit: VR 연동을 위한 패키지
    - TextMeshPro: UI의 TextMeshPro를 위한 패키지
    - 매인 자동차를 위한 패키지는 다음 공유된 패키지를 통해 다운: https://www.mediafire.com/file/fvv2dpakkih4mum/Assets.zip/file



## 서버 DB 연동

## 전체 디렉토리
- Img: VR UI 화면에서 사용되는 img 파일
- Scenes: 구현한 게임 맵
    - GameScene
        - MountainRoad: 산간 도로 구역, 3가지 난이도 + 낮과 밤 = 6가지 Scene
        - NormalMap: 일반 도로 구역, 3가지 난이도 + 낮과 밤 = 6가지 Scene
        - Park: 주차장 구역, 3가지 난이도 + 낮과 밤 = 6가지 Scene
        - SchoolZONE: 어린이 보호 구역(일반 도로 구역에서 코스를 바꾼 형태), 3가지 난이도 + 낮과 밤 = 6가지 Scene
    - UIScene_Complete: VR 사용자 UI 화면
        - Login: 로그인 화면
        - Menu: 사용자 메뉴 화면
        - SelectDif: 난이도 선택 화면
        - SelectMap: 맵 선택 화면
- Script: 게임 동작을 위한 c# 코드 파일
- resources: 게임 상황 속에서 실시간으로 객체를 Scene에 Spawn하기 위한 오브젝트 파일

## Scrit 디렉토리 
- Script_CR
    - db: 데이터베이스 및 서버와 unity 프로젝트를 연동
        - Database.cs: 로그인, 로그아웃, 게임 환경 설정 등의 정보를 데이터베이스와 연동
        - user_info.cs: 유저의 정보를 클래스 형태로 저장하고 get,set 함수로 사용할 수 있게 함 
    - Managers
        - Managers.cs: Manager 파일을 관리
        - InputManager.cs: 마우스 입력 등을 관리. 사용x
        - ResourceManager.cs: prefab 등의 자원을 관리
        - SceneManagerEx.cs: game scene 관리
        - UIManager.cs: UI 요소 관리
    - Scenes: 모든 게임 Scene 각각에 대한 파일
        - BaseScene.cs: 모든 Scene class에 대한 abstract class를 포함
        - GameScene.cs: 모든 게임 scene에 대한 scene class 포함
        - LoginScene.cs: 로그인 scene에 대한 scene class 포함
        - MenuScene.cs: 메뉴 선택 scene에 대한 scene class 포함
        - MyPageScene.cs: 마이페이지 scene에 대한 scene class 포함
        - SelectDifScene.cs: 난이도 선택 scene에 대한 scene class 포함
        - SelectMapScene.cs: 맵 선택 scene에 대한 scene class 포함
        - SignUpScene.cs: 회원가입 scene에 대한 scene class 포함
    - UI: 게임 UI 관리
        - Buttton: 각 Scene에 존재하는 버튼, 슬라이더 등 objects에 대한 기능 설정
        - UI_Base.cs: 모든 UI 요소에 대한 abstract class 포함
        - UI_EventHandler.cs: 마우스 클릭에 대한 event handler
        - UI_Popup.cs: 팝업 창 관리
        - UI_Scene.cs: UI Scene 초기화
    - Utils: 필요한 기능 
        - Define.cs: 게임 Scene, 소리 등을 정의
        - User.cs: 유저의 개인 정보 저장. 사용 x
        - Util.cs: get 함수 등 필요한 기능
- Script_HJ
    - AI: npc 차량, 야생동물 출현과 동작을 관리하는 Script
        - AnimalSpawner.cs: 지정된 위치 안에 야생동물을 랜덤하게 발생시킴
        - NpcCarSpawner.cs: 지정된 위치 안에 차량을 랜덤하게 발생시킴
        - NpcCarController.cs: 속도, 회전값 등 npc 차량 관련 움직임 설정
    - Score: 게임 플레이 관련 UI를 관리하는 Script
        - PauseUI.cs: 게임 중 정지 버튼을 눌렀을 때 나타나는 UI 동작 설정
        - ScoreUI_2.cs: 게임 종료 후 점수와 감점 항목을 보여주는 UI 동작 설정
    - WaypointManager: 오브젝트 이동 경로 관리하는 Script
        - WaypointEditor.cs: 이동 경로 지정하는 속성 설정
        - PedestriansWaypointManagerWindow.cs: Animal Waypoint 생성을 위한 설정
        - CarWaypointManagerWindow.cs: Car Waypoint 생성을 위한 설정
- Script_HU
    - AI: npc 차량, 사람 출현과 동작을 관리하는 Script
        - CharacterNavigationController.cs: 보행자npc의 동작(애니메이션, 속도, 방향)을 설정
        - LightController.cs: 자동차npc의 깜빡이 설정 -> 사용x
        - NpcCarController.cs: 자동차npc의 동작(바퀴, 속도, 방향)을 설정
        - NpcCarSpawn.cs: 자동차npc 스폰 위치 설정
        - PedestrianSpawner.cs:보행자npc 스폰 위치 설정
    - Car
        - Controller.cs: 플레이어 자동차를 로지텍 g29의 변수값에 따라 작동하게 설정
        - SDKInputManager.cs: 로지텍 g29에서 핸들, 페달에 대한 값을 변수로 설정
        - SpeedCalculate.cs: 플레이어 자동차의 스피드를 계산      
    - Logitech
        - LogitechInput.cs: 로지텍 g29에 변수에 대한 기본 설정
        - LogitechKeyCode.cs: 로지텍 g29의 키코드
    - MapandDifficultyselect: 
        - Moutain: 난이도, 시간대에 대한 산간도로 Scene의 6개 스크립트
        - Normal: 난이도, 시간대에 대한 일반 Scene의 6개 스크립트
        - Park: 난이도, 시간대에 대한 주차장 Scene의 6개 스크립트
        - School: 난이도, 시간대에 대한 어린이보호구역 Scene의 6개 스크립트
    - Navigation
        - navi_straight.cs: 직진 구간에서 직진 화살표 띄워줌
        - navi_turn_left.cs: 좌회전 구간에서 직진 화살표 띄워줌
        - navi_turn_right.cs: 우회전 구간에서 직진 화살표 띄워줌
        - navi_uturn.cs: 유턴 구간에서 직진 화살표 띄워줌
    - SceneManager
        - Days.cs: 일반도로/ 어린이보호구역/ 주차장 맵에서 낮 표현(에셋에서 제공해줌)
        - Nights.cs: 일반도로/ 어린이보호구역/ 주차장 맵에서 밤 표현(에셋에서 제공해줌)
        - FindLingts.cs: 일반도로/ 어린이보호구역에 있는 light를 가져오는 스크립트 -> 사용안함
    - Score
        - overspeed.cs: 과속에 대한 채점
        - suddenstop_score.cs: 급정거에 대한 채점
        - trafficsing_score.cs: 신호체계에 대한 채점
    - TrafficSystem
        - CarTrafficManager.cs: 신호에 따라 cube와 cubestart에 닿은 npc자동차들의 속도를 0으로 바꿈
        - CubestartTrigger.cs: cubestart에 닿은 npc자동차를 저장
        - CubeTrigger.cs: cube에 닿는 npc자동차 리스트를 저장
        - PedestrianTrafficManger.cs: 신호에따라 보행자의 waypoint 조절
    - UI
        - CountDown.cs: 주행 시작에 대한 Countdown UI
        - GameTime.cs: 주행 시간에 대한 UI -> 사용안함
        - mirrorInvisible.cs: 로지텍 g29 핸들의 버튼을 누르면 백, 사이드 미러의 UI를 띄워줌
        - puasescreen.cs: 로지텍 g29 핸들의 버튼을 누르면 잠시멈춤/ 홈화면/ 다시 시작에 대한 UI를 띄워줌
    - WaypointManager
        - CarWaypointManagerWindow.cs: npc 자동차 waypoint를 위한 커스텀 창 설정
        - PedestriansWaypointManagerWindow.cs: npc 보행자 waypoint를 위한 커스텀 창 설정
        - WaypointEditor.cs: waypoint 설정(크기, 색)에 대한 스크립트
        - Waypointer.cs: waypoint 설정(이전, 다음 waypoint, branchratio)에 대한 스크립트
        - WaypointNavigator.cs: waypoint를 따라 일정한 방향으로 이동하게 하는 스크립트
- Script_MH
    - Controller: 게임 상황에서 게임 상태와 점수를 동작하는 Script
        - PlayState.cs: 게임의 각각 상태에 따라 Event를 발생
        - PlayScore.cs: 게임 점수 감점을 위한 Collision 및 Trigger 감지
        - Timer.cs: VR 화면 UI의 타이머 조작
    - Managers: 전체 게임을 관리하는 Script
        - PlayStateManager.cs: 게임 상태 관리 Script
        - ScoreManager.cs: 전체 게임 점수 관리
        - SoundManager.cs: 게임의 사운드 관리
    - Scene
        - Player_Spawn.cs: 사용자 객체 Scene에 Spawn하기 위한 Script -> 사용 x

- VRKeyboard: VR 입력을 받기 위한 cs파일 폴더, VR Keyboard 패캐지 내의 파일
    - KeybaordManager.cs: input Text를 받기 위해 코드 수정

## vr 연동 방법 
- vr 연동을 위한 xr tool kit 패키지 다운 
- XR Interaction Manager Scene에 위치
- EventSystem에 XRUI Input Modules를 component로 추가
- XR Rig를 Scene에 위치: 이때 GazeRaycaster를 LeftHandController와 RightHandController 오브젝트 아래에 둬야 두개의 VR 핸들이 UI에서 동작 가능

## 시연 영상 Youtube URL
- https://www.youtube.com/watch?v=D9rMlxR0AeM

## 참고 자료
[C#과 유니티로 만드는 MMORPG 게임 개발 시리즈] Part3: 유니티 엔진 - 인프런
[Building a Traffic System in Unity] Game Dev Guide - 유튜브
[Unity Logitech Steering Wheel Using Logitech Gaming SDK -Tutorial] Z Studio - 유튜브
