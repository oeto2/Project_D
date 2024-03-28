using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Constants
{
    public enum MonsterMoveType
    {
        //������ Ÿ��
        Lock,
        //���� Ÿ��
        Scout,
        //Enum�� �� ����
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

