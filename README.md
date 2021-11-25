# Hanium_VR 개발 명세 
--------------

## 개발 환경
- 사용 언어: C#
- 게임 개발 프레임워크: Unity
    - Unity 버전: 2020.3.18f1 

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

## 디렉토리
- Script_CR
    - db:
    - Managers
    - Scenes
    - UI
    - Utils
- Script_HJ
    - AI
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
    - Controller
    - Managers
    - Scene

- VRKeyboard: VR 입력을 받기 위한 cs파일 폴더, VR Keyboard 패캐지 내의 파일
    - KeybaordManager.cs: input Text를 받기 위해 코드 수정

## vr 연동 방법 
- vr 연동을 위한 xr tool kit 패키지 다운 
- XR Interaction Manager Scene에 위치
- EventSystem에 XRUI Input Modules를 component로 추가
- XR Rig를 Scene에 위치: 이때 GazeRaycaster를 LeftHandController와 RightHandController 오브젝트 아래에 둬야 두개의 VR 핸들이 UI에서 동작 가능

## 시연 영상 Youtube URL
- https://www.youtube.com/watch?v=D9rMlxR0AeM