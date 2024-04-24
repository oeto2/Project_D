using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tutorial : MonoBehaviour
{
    private Player _player;
    public TextMeshProUGUI tutorialText;
    public GameObject NextStage;
    public EnemyInteration[] enemyInteration;

    private int _questNum = 0;
    private bool _textOn;
    private float _time = 0;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameManager.Instance.playerObject.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        Quest();
    }

    private void Quest()
    {
        switch(_questNum)
        {
            case 0:
                if(_textOn == false)
                {
                    SetTutorialText("3초간 움직여라. (WASD)");
                    _textOn = true;
                }
                if (_player.stateMachine.GetCurrentState() == _player.stateMachine.WalkState)
                {
                    _time += Time.deltaTime;
                    if (_time > 3f)
                    {
                        _questNum++;
                        _time = 0f;
                        _textOn = false;
                    }
                }
                else
                    _time = 0f;
                break;

            case 1:
                if (_textOn == false)
                {
                    SetTutorialText("3초간 달려라. (Shift)");
                    _textOn = true;
                }
                if (_player.stateMachine.GetCurrentState() == _player.stateMachine.RunState)
                {
                    _time += Time.deltaTime;
                    if (_time > 3f)
                    {
                        _questNum++;
                        _time = 0f;
                        _textOn = false;
                    }
                }
                else
                    _time = 0f;
                break;

            case 2:
                if (_textOn == false)
                {
                    SetTutorialText("점프해라. (Space)");
                    _textOn = true;
                }
                if (_player.stateMachine.GetCurrentState() == _player.stateMachine.JumpState)
                {
                    _questNum++;
                    _textOn = false;
                }
                break;
            case 3:
                if (_textOn == false)
                {
                    SetTutorialText("공격해라. (마우스 좌클릭)");
                    _textOn = true;
                }
                if (_player.stateMachine.GetCurrentState() == _player.stateMachine.ComboAttackState)
                {
                    _questNum++;
                    _textOn = false;
                }
                else
                    _time = 0f;
                break;
            case 4:
                if (_textOn == false)
                {
                    SetTutorialText("3초간 방어해라. (마우스 우클릭)");
                    _textOn = true;
                }
                if (_player.stateMachine.GetCurrentState() == _player.stateMachine.DefenseState)
                {
                    _time += Time.deltaTime;
                    if (_time > 3f)
                    {
                        _questNum++;
                        _time = 0f;
                        _textOn = false;
                    }
                }
                else
                    _time = 0f;
                break;
            case 5:
                if (_textOn == false)
                {
                    SetTutorialText("스킬은 1,2,3번을 누르면 사용할 수 있다.");
                    _textOn = true;
                }
                if (_player.stateMachine.GetCurrentState() == _player.stateMachine.SkillState)
                {
                    _questNum++;
                    _textOn = false;
                }
                break;
            case 6:
                if (_textOn == false)
                {
                    SetTutorialText("인벤토리는 I키를 누르면 열 수 있다.");
                    _textOn = true;
                }
                if (Input.GetKeyDown(KeyCode.I))
                {
                    _questNum++;
                    _textOn = false;
                }
                break;
            case 7:
                if (_textOn == false)
                {
                    SetTutorialText("장비창은 P키를 뉘르면 열 수 있다.");
                    _textOn = true;
                }
                if (Input.GetKeyDown(KeyCode.P))
                {
                    _questNum++;
                    _textOn = false;
                }
                break;
            case 8:
                if (_textOn == false)
                {
                    SetTutorialText("길이 열렸으니, 앞으로 나아갈 수 있다.");
                    _textOn = true;
                }
                NextStageOpen();
                break;
            case 9:
                if(_player.transform.position.x>-6)
                {
                    if (_textOn == false)
                    {
                        SetTutorialText("오른쪽과 앞에 함정이 있으니 조심하자.");
                        _textOn = true;
                    }
                    if(_player.transform.position.x > 1)
                    {
                        _questNum++;
                        _textOn = false;
                    }
                }
                break;
            case 10:
                if (_textOn == false)
                {
                    SetTutorialText("앞에 있는 상자를 열어보자.");
                    _textOn = true;
                }
                if (_player.transform.position.x > 15)
                {
                    _questNum++;
                    _textOn = false;
                }
                break;
            case 11:
                if (_textOn == false)
                {
                    SetTutorialText("몬스터 죽이면 아이템을 얻을 수 있다.");
                    _textOn = true;
                }
                if (enemyInteration[0]._isRoot || enemyInteration[1]._isRoot)
                {
                    _questNum++;
                    _textOn = false;
                }
                break;
            case 12:
                if (_textOn == false)
                {
                    SetTutorialText("포탈을 타면 탈출할 수 있다.");
                    _textOn = true;
                }
                break;
            default:
                break;
        }
    }
    private void NextStageOpen()
    {
        NextStage.transform.rotation = Quaternion.Slerp(
            NextStage.transform.rotation, Quaternion.Euler(100,90,0), Time.deltaTime);
        if(NextStage.transform.rotation.x> 0.5f)
        {
            NextStage.SetActive(false);
            _questNum++;
            _textOn = false;
        }
    }

    public void SetTutorialText(string text_)
    {
        tutorialText.text = text_;
    }
}
