## UI 팝업 관리 문제
### **핵심 요약**
UI들을 용도에 따라 Type을 분류하여 관리함으로써, UI 관리를 기존보다 용이하게함.



### 해결 과정
ESC키를 눌렀을 경우, 순차적으로 종료해야하는 UI들을 이름으로 판단하여 관리했는데, 이는 추후에 팝업의 이름이 바뀌거나 추가될 경우 매번 수정이 필요하기 때문에 코드가 유연하지 않고 비효율적이라고 생각했습니다.  

![image](https://github.com/user-attachments/assets/2905cc0a-55d6-4730-acd1-aa8bb7b111f7)\
(UIManager.cs 수정 전)



따라서, 팝업의 종류를 구분할 수 있는 Enum 값을 통해 모든 UI들이 각자의 UI TYPE을 갖고 있게하여, ESC키를 눌렀을 때 종료되어야하는 팝업들만 따로 분류를 했습니다. 


![UipopupType](https://github.com/user-attachments/assets/54a8ad29-f296-4174-85dc-bf9a5c6882b5)\
(인벤토리 팝업)

![image](https://github.com/user-attachments/assets/8c53efce-001d-4885-a9d6-6c8562179eaa)\
(UIManager.cs 수정 후)

<br><br><br><br>

## 플레이어의 위치에 따른 몬스터 스폰 시스템
### **핵심 요약**
프레임 드랍 및 성능 개선을 위해 몬스터를 전부 소환하는 방식이 아닌, 플레이어의 위치를 감지해 필요한 몬스터만 소환하는 방식으로 스폰 시스템을 변경했습니다.

### 해결 과정
기존에는 스폰 포인트에 몬스터 종류만 선택하면, 던전 진입시 몬스터가 소환되었습니다.
이 방식은 구현은 간단하다는 장점이 존재했지만, 3D 소울라이크 게임 특성상 많은 몬스터를 소환하고, 소환된
몬스터의 비용이 크기 때문에 문제가 되었습니다. 

![image](https://github.com/user-attachments/assets/e4463da2-96c0-467b-a791-9ca92134d8a6)
![스폰시스템 종류](https://github.com/user-attachments/assets/3703844b-425c-4919-bdb6-6c879efa8d6b)


<br>

