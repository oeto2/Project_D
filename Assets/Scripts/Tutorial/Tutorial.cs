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
        _questNum = 8;
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
                    SetTutorialText("WASD로 움직일 수 있어 3초동안 움직여보자");
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
                    SetTutorialText("Shift키를 누르면 달리기가 가능해 3초간 달려봐");
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
                    SetTutorialText("\r\nspace키를 누르면 점프할 수 있어 점프해보자");
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
                    SetTutorialText("마우스 좌클릭을 누르면 공격이 가능해 공격해보자");
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
                    SetTutorialText("마우스 우클릭을 누르면 방어할 수 있어 3초간 방어를 해보자");
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
                    SetTutorialText("1, 2, 3 중 하나를 누르면 스킬을 사용할 수 있어 골라봐");
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
                    SetTutorialText("I키 누르면 인벤토리가 나오지 확인해봐");
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
                    SetTutorialText("P키 누르면 장비창이 나와 확인해봐");
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
                    SetTutorialText("길이 열렸어 앞으로 나아가자");
                    _textOn = true;
                }
                NextStageOpen();
                break;
            case 9:
                if(_player.transform.position.x>-6)
                {
                    if (_textOn == false)
                    {
                        SetTutorialText("함정이 있네 조심해");
                        _textOn = true;
                    }
                    if(_player.transform.position.x > 2)
                    {
                        _questNum++;
                        _textOn = false;
                    }
                }
                break;
            case 10:
                if (_textOn == false)
                {
                    SetTutorialText("상자 열어 뭐가 들었는지 확인해");
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
                    SetTutorialText("몬스터 죽이고 아이템 주워");
                    _textOn = true;
                }
                if (true)
                {
                    _questNum++;
                    _textOn = false;
                }
                break;
            case 12:
                if (_textOn == false)
                {
                    SetTutorialText("포탈을 타고 탈출해");
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
