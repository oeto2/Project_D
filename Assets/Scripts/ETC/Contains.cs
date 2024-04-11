using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Constants
{
    static class Constant
    {
        public const int InvenSize = 30;
        public const int StorageSize = 70;
    }

    public enum MonsterMoveType
    {
        //������ Ÿ��
        Lock,
        //���� Ÿ��
        Scout,
        //Enum�� �� ����
        Count
    }

    public enum MonsterName
    {
        Skeleton,
        Goblin,
        OrkWarrior
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

