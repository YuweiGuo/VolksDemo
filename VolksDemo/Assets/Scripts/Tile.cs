using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    public enum TileTypes
    {
        TILE_START = 0,
        TILE_BOXREWARD_1 = 1,
        TILE_BOXREWARD_2 = 2,
        TILE_CUP = 3,
        TILE_BOSS = 4,
        TILE_SWITCH = 5,

        TILE_BOMB = 6,
        TILE_DICE = 7,
        TILE_REWARD = 8,

        TILE_CATCHCOINS = 9,
        TILE_GUESSBOX = 10,

        TILE_APPLE = 11,
        TILE_BUG = 12,
        TILE_FOOD = 13,
        TILE_HISTORY = 14,
        TILE_HUMAN = 15
    }

    public TileTypes Type;
    public int Stage;
    public GameObject TilePfb;
    public Tile Pre;
    public Tile Left; // 当没有其他的时，其为下一个
    public Tile Right;

    public GM inGM;

   
    public void InitTile(TileTypes TypeIn, GameObject MapStart, int StageIn, GM GMIn)
    {
        Type = TypeIn;
        Stage = StageIn;
        TileResouces.GetResources(Type, MapStart, out TilePfb);
        Pre = null;
        Left = null;
        Right = null;

        inGM = GMIn;
    }

}
