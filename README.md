# FATE 
![image](https://github.com/user-attachments/assets/1f29b387-c7f1-46af-8f22-7d1d85fc7ce7)




## 🖥️ 프로젝트 소개
Fate는 1인칭 소울라이크 RPG 게임으로 던전에 있는 보스 몬스터를 처치하는 것이 목적인 게임입니다.
몬스터를 처치한 후 몬스터의 시체를 탐색해 아이템을 획득 및 장착할 수 있으며, 상점에서 아이템을 구매하거나
판매를 통해 플레이어를 성장시킬 수 있습니다.
<br>

## 📜 기획의도
유니티 3D를 활용한 소울라이크 게임 개발

## 🕰️ 개발 기간
* 2024.03 ~ 2024.04

### 🧑‍🤝‍🧑 맴버구성
 - 팀장  : 최철환 - 게임매니저, 사운드, 플레이어
 - 부팀장 : 이상민 - 몬스터, UI, 맵, 아이템 루팅
 - 팀원1 : 박지훈 - 포탈, 함정, 인벤토리
 - 팀원2 : 김관철 - 데이터 테이블, 상점

### ⚙️ 개발 환경
- Unity 2022.3.2f1
- Visual Studio 2022

## 📌 핵심 기술
#### FSM(유한상태머신) - <a href="https://github.com/oeto2/Project_D/wiki/FSM-(%EC%9C%A0%ED%95%9C%EC%83%81%ED%83%9C%EB%A8%B8%EC%8B%A0" >상세보기 - WIKI 이동</a>
- FSM을 사용한 이유
- FSM 스크립트 (몬스터)
- FSM 구조

#### UIManager - <a href="https://github.com/oeto2/Project_D/wiki/UIManager" >상세보기 - WIKI 이동</a>
- 동적로딩
- 싱글톤패턴
- Stack을 활용한 팝업관리
## 🛠️ 트러블 슈팅
#### UI 팝업 관리 문제 - <a href="https://github.com/oeto2/Project_D/wiki/%ED%8A%B8%EB%9F%AC%EB%B8%94-%EC%8A%88%ED%8C%85" >상세보기 - WIKI 이동</a>
- UI들을 용도에 따라 Type을 분류하여 관리함으로써, UI 관리를 기존보다 용이하게함
