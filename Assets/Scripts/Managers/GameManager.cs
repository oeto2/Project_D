using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletoneBase<GameManager>
{
    public GameObject playerObject;

    //���߿� �����ϱ�
    private void Awake()
    {
        playerObject = GameObject.FindWithTag("Player");
    }
}
