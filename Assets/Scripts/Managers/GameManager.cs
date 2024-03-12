using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletoneBase<GameManager>
{
    public GameObject playerObject;

    //나중에 수정하기
    private void Awake()
    {
        playerObject = GameObject.FindWithTag("Player");
    }
}
