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
    - db:
    - Managers
    - Scenes
    - UI
    - Utils
- Script_HJ
    - AI: npc 차량, 야생동물 출현과 동작을 관리하는 Script
        - AnimalSpawner.cs: 지정된 위치 안에 야생동물을 랜덤하게 발생시킴
        - NpcCarSpawner.cs: 지정된 위치 안에 차량을 랜덤하게 발생시킴
        - NpcCarController.cs: 속도, 회전값 등 npc 차량 관련 움직임 설정
    - Score
    - WaypointManager
- Script_HU
    - AI
    - Car
    - Logitech
    - MapandDifficultyselect
    - Navigation
    - SceneManager
    - Score
    - TrafficSystem
    - UI
    - WaypointManager
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
