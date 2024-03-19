using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonBase<GameManager>
{
    public GameObject playerObject;

    //나중에 수정하기
    private void Awake()
    {
        playerObject = GameObject.FindWithTag("Player");
    }
}
