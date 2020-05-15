using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileResouces : MonoBehaviour
{
    public static void GetResources(Tile.TileTypes Type, GameObject MapStart, out GameObject ATile)
    {
        ATile = Instantiate(Tools.FindPfbPerName("tile_pfb"), MapStart.transform) as GameObject;
        ATile.SetActive(false);
        Sprite spr;
        switch ( Type ) 
        {
            case Tile.TileTypes.TILE_START: // start
                spr = Tools.FindSpritePerName("tile_start");
                break;
            case Tile.TileTypes.TILE_BOMB: // bomb
                spr = Tools.FindSpritePerName("tile_bomb");
                break;
            case Tile.TileTypes.TILE_DICE: // dice
                spr = Tools.FindSpritePerName("tile_dice");
                break;
            case Tile.TileTypes.TILE_REWARD: // reward
                spr = Tools.FindSpritePerName("tile_reward");
                break;
            case Tile.TileTypes.TILE_SWITCH: // switch
                spr = Tools.FindSpritePerName("tile_switch");
                break;
            case Tile.TileTypes.TILE_CATCHCOINS: // catchcoins
                spr = Tools.FindSpritePerName("tile_catchcoins");
                break;
            case Tile.TileTypes.TILE_GUESSBOX: // guessbox
                spr = Tools.FindSpritePerName("tile_guessbox");
                break;
            case Tile.TileTypes.TILE_BOXREWARD_1: // box1
                spr = Tools.FindSpritePerName("tile_boxreward_1");
                break;
            case Tile.TileTypes.TILE_BOXREWARD_2: // box2
                spr = Tools.FindSpritePerName("tile_boxreward_2");
                break;
            case Tile.TileTypes.TILE_CUP: // cup
                spr = Tools.FindSpritePerName("tile_cup");
                break;
            case Tile.TileTypes.TILE_BOSS: // boss
                spr = Tools.FindSpritePerName("tile_boss");
                break;
            case Tile.TileTypes.TILE_APPLE: // apple
                spr = Tools.FindSpritePerName("tile_apple");
                break;
            case Tile.TileTypes.TILE_BUG: // bugs
                spr = Tools.FindSpritePerName("tile_bug");
                break;
            case Tile.TileTypes.TILE_FOOD: // foods
                spr = Tools.FindSpritePerName("tile_food");
                break;
            case Tile.TileTypes.TILE_HISTORY: // history
                spr = Tools.FindSpritePerName("tile_history");
                break;
            case Tile.TileTypes.TILE_HUMAN: // human
                spr = Tools.FindSpritePerName("tile_human");
                break;
            default:
                spr = Tools.FindSpritePerName("tile_dice"); 
                break;
        }
        Tools.SetObjAndSprSameSize(ATile, spr);
        ATile.GetComponent<Image>().sprite = spr;
    }
}
