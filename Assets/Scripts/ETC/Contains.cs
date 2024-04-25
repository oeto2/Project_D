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

    public enum MonsterType
    {
        Normal,
        Boss
    }

    public enum MonsterName
    {
        Skeleton,
        Goblin,
        OrkWarrior,
        Mimic
    }

   public enum DungeonType
    {
        Farming,
        OrkWarrior,
        OrkAssasin,
        Necromancer,
        Count
    }

    public enum SceneType
    {
        TutorialScene,
        LobbyScene,
        DungeonScene,
        OrkWarriorScene,
        OrkOrkAssasinScene,
        NecromancerScene
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

