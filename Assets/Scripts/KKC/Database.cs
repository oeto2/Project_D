using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Database : MonoBehaviour
{
    private static ItemDB item;
    private static MonsterDB monster;

    public static ItemDB Item
    {
        get
        {
            if (item == null)
                item = new ItemDB();

            return item;
        }
    }

    public static MonsterDB Monster
    {
        get
        {
            if (monster == null)
                monster = new MonsterDB();

            return monster;
        }
    }
}
