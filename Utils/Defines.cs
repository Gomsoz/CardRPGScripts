using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defines
{
    public struct Position
    {
        public string MapIdx;
        public float posX;
        public float posY;

        public Position(float x, float y)
        {
            posX = x;
            posY = y;
            MapIdx = Managers.World.CurMapIdx;
        }

        public Position(float x, float y, string mapIdx)
        {
            posX = x;
            posY = y;
            MapIdx = mapIdx;
        }

        public Position(Defines.Position pos)
        {
            posX = pos.posX;
            posY = pos.posY;
            MapIdx = Managers.World.CurMapIdx;
        }

        public Position(Defines.Position pos, string mapIdx)
        {
            posX = pos.posX;
            posY = pos.posY;
            MapIdx = mapIdx;
        }

        public static Position operator- (Position left, Position right)
        {
            return new Position(left.posX - right.posX, left.posY - right.posY);
        }

        public static Position operator+ (Position left, Position right)
        {
            return new Position(left.posX + right.posX, left.posY + right.posY);
        }

        public Position PositionForDir(Direction dir, int dis = 1)
        {
            switch (dir)
            {
                case Direction.Left:
                    return new Position(posX - dis, posY);
                case Direction.Right:
                    return new Position(posX + dis, posY);
                case Direction.Up:
                    return new Position(posX, posY + dis);
                case Direction.Down:
                    return new Position(posX, posY - dis);
                default:
                    return new Position();
            }
        }
    }
    public enum SceneType
    {
        LoadingScene,
        StartScene,
        GameScene,
        HouseScene,
    }
    public enum Direction
    {
        Left,
        Right,
        Up,
        Down,
        Count,
    }
    public enum CardType
    {
        None,
        Deck,
        Drawed,
        Enrolled,
        Count,
    }
    public enum ObjectType
    {
        Player,
        Enemy,
        Item,
        Mark,
        Count,
    }
    public enum TreasureType
    {
        StatsType,
        TrunType,
        ActionType,
        Count,
    }
    public enum UIEvent
    {
        Click,
    }
    public enum CardTimeState
    {
        SelectingCard,
        FirstCard,
        SecondCard,
        ThirdCard,
        Count,
    }
    public enum CharacterStats
    {
        HP,
        MP,
        Attack,
        Armor,
    }
    public enum EnemyGradeType
    {
        Normal,
        Elite,
        Boss,
    }
    public enum ItemType
    {
        Coin,
        ExpPotion,
        HpPotion,
        MpPotion,
        RandomReineforcementScroll,
        SelectReinforecementScroll,
        NormalTreasure,
        MonsterTreasure,
        Count,
    }

    public enum MapType
    {
        MainWorld,
        SubMap,
        Count,
    }

    public enum CameraType
    {
        Main,
        UI,
    }

    public enum CharacterState
    {
        Idle = 0,
        Ready = 1,
        Walk = 2,
        Run = 3,
        Jump = 4,
        Climb = 5,
        Death = 9,
        ShieldBlock = 10,
        WeaponBlock = 11,
        Evasion = 12,
        Dance = 13
    }

    public enum MonsterState
    {
        Idle = 0,
        Ready = 1,
        Walk = 2,
        Run = 3,
        Jump = 4,
        Climb = 5,
        Death = 9
    }
}
