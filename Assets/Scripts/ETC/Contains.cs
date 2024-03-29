using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Constants
{
    public enum MonsterMoveType
    {
        //°íÁ¤µÈ Å¸ÀÔ
        Lock,
        //Á¤Âû Å¸ÀÔ
        Scout,
        //EnumÀÇ ÃÑ °¹¼ö
        Count
    }

   public enum DungeonLevel
    {
        Esay,
        Normal,
        Hard,
        Count
    }

    public enum SceneType
    {
        LobbyScene,
        DungeonScene
    }

    public enum LobbyType
    {
        Main,
        Option,
        Shop,
        EnterDungeon,
        Status,
        Inventory,
        Storage
    }
}

