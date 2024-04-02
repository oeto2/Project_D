using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTest : MonoBehaviour
{

    private void Start()
    {
        Database.DropPer.ItemEnum();

        int rand = Random.Range(1, Database.Monster.Get(11000001).monsterMaxRoot);
        for (int i = 0; i < rand; i++)
        {
            //GetItem(Database.Monster.Get(11000001).dropId);
            Debug.Log(Database.DropPer.GetItem(Database.Monster.Get(11000001).dropId).itemName);
        }
    }
}
