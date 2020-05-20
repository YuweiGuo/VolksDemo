using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using System.Threading;
using UnityEngine.UI;

public class TileManager : MonoBehaviour
{
    public int ClickStep = 0;
    public void DoEvent(Tile.TileTypes Type, GM inGM)
    {
        GameObject ca = GameObject.FindGameObjectWithTag("PopCanvas");
        switch (Type)
        {
            case Tile.TileTypes.TILE_START: // start
                // 不会发生任何事情
                break;
            case Tile.TileTypes.TILE_BOMB: // bomb
                GameObject ExplodePfb = Instantiate(Tools.FindPfbPerName("explode_pfb"), GameObject.FindGameObjectWithTag("mapcanvas").transform) as GameObject;
                ExplodePfb.transform.position = inGM.Niko.Position.TilePfb.transform.position;
                ExplodePfb.SetActive(true);
                ExplodePfb.transform.DOLocalMoveY(ExplodePfb.transform.localPosition.y + 100, 1).OnComplete(()=> {
                    GameObject.Destroy(ExplodePfb);
                });
                // StartCoroutine(PlayAnimation());
                inGM.PlayerGoBack();
                break;
            case Tile.TileTypes.TILE_DICE: // dice
                inGM.PlayerGoForward(UnityEngine.Random.Range(1, 4));
                break;
            case Tile.TileTypes.TILE_REWARD: // reward
                GameObject CpinsPfb = Instantiate(Tools.FindPfbPerName("coins_pfb"), GameObject.FindGameObjectWithTag("mapcanvas").transform) as GameObject;
                CpinsPfb.transform.position = inGM.Niko.Position.TilePfb.transform.position;
                CpinsPfb.SetActive(true);
                CpinsPfb.transform.DOLocalMoveY(CpinsPfb.transform.localPosition.y + 100, 1).OnComplete(() => {
                    GameObject.Destroy(CpinsPfb);
                });
                break;
            case Tile.TileTypes.TILE_SWITCH: // switch                
                break;
            case Tile.TileTypes.TILE_CATCHCOINS: // catchcoins
                GameObject CollectGame = Instantiate(Tools.FindPfbPerName("collectgame_pfb"), ca.transform) as GameObject;
                CollectGame.GetComponent<CollectGameClick>().TM = this;
                CollectGame.SetActive(true);
                break;
            case Tile.TileTypes.TILE_GUESSBOX: // guessbox
                
                GameObject GuessBox = Instantiate(Tools.FindPfbPerName("guessbox_pfb"), ca.transform) as GameObject;
                GuessBox.GetComponent<ChooseBoxClick>().TM = this;
                GuessBox.SetActive(true);
                break;
            case Tile.TileTypes.TILE_BOXREWARD_1: // box1                
            case Tile.TileTypes.TILE_BOXREWARD_2: // box2
                ca.transform.Find("GetReward").gameObject.SetActive(true);
                break;
            case Tile.TileTypes.TILE_CUP: // cup
                
                break;
            case Tile.TileTypes.TILE_BOSS: // boss
            case Tile.TileTypes.TILE_APPLE: // apple
            case Tile.TileTypes.TILE_BUG: // bugs
            case Tile.TileTypes.TILE_FOOD: // foods          
            case Tile.TileTypes.TILE_HISTORY: // history           
            case Tile.TileTypes.TILE_HUMAN: // human
                GameObject WordGame = Instantiate(Tools.FindPfbPerName("wordgame_pfb"), ca.transform) as GameObject;
                WordGame.GetComponent<WordGameClick>().TM = this;
                WordGame.GetComponent<WordGameClick>().inGM = inGM;
                WordGame.SetActive(true);
                break;
            default:
                break;
        }

        inGM.ResetDice();
        if(Type != Tile.TileTypes.TILE_BOMB)
            inGM.LocateCamera();

        if((int)Type == 4 || (int)Type >=11 && (int)Type <=15)
            inGM.RewardRecorder += 1;
        
    }

    
}
