using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonBase<GameManager>
{
    public GameObject playerObject;

    //���߿� �����ϱ�
    private void Awake()
    {
        playerObject = GameObject.FindWithTag("Player");
    }
}
