using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Database : MonoBehaviour
{
    private static ItemDB item;
    private static MonsterDB monster;
    private static ClassDB charClass;
    private static DropPerDB dropPer;
    private static ShopDB shop;

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

    public static ClassDB Class
    {
        get
        {
            if(charClass == null)
                charClass = new ClassDB();

            return charClass;
        }
    }

    public static DropPerDB DropPer
    {
        get
        {
            if(dropPer == null)
                dropPer = new DropPerDB();

            return dropPer;
        }
    }

    public static ShopDB Shop
    {
        get
        {
            if (shop == null)
                shop = new ShopDB();

            return shop;
        }
    }
}
