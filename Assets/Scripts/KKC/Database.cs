using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Database : MonoBehaviour
{
    private static ItemDB item;

    public static ItemDB Item
    {
        get
        {
            if (item == null)
                item = new ItemDB();

            return item;
        }
    }
}
