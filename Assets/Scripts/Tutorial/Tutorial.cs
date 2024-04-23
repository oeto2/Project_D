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
                    SetTutorialText("3�ʰ� ��������. (WASD)");
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
                    SetTutorialText("3�ʰ� �޷���. (Shift)");
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
                    SetTutorialText("�����ض�. (Space)");
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
                    SetTutorialText("�����ض�. (���콺 ��Ŭ��)");
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
                    SetTutorialText("3�ʰ� ����ض�. (���콺 ��Ŭ��)");
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
                    SetTutorialText("��ų�� 1,2,3���� ������ ����� �� �ִ�.");
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
                    SetTutorialText("�κ��丮�� IŰ�� ������ �� �� �ִ�.");
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
                    SetTutorialText("���â�� PŰ�� ������ �� �� �ִ�.");
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
                    SetTutorialText("���� ��������, ������ ���ư� �� �ִ�.");
                    _textOn = true;
                }
                NextStageOpen();
                break;
            case 9:
                if(_player.transform.position.x>-6)
                {
                    if (_textOn == false)
                    {
                        SetTutorialText("�����ʰ� �տ� ������ ������ ��������.");
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
                    SetTutorialText("�տ� �ִ� ���ڸ� �����.");
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
                    SetTutorialText("���� ���̸� �������� ���� �� �ִ�.");
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
                    SetTutorialText("��Ż�� Ÿ�� Ż���� �� �ִ�.");
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
