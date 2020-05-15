using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public GameObject PlayerPfb;
    public Tile Position;
    public float YShift = 120f;


    public void InitPlayer(Tile StartPosition)
    {
        // 初始化
        Position = StartPosition;
        PlayerPfb = Instantiate(Tools.FindPfbPerName("tile_pfb"), GameObject.FindGameObjectWithTag("mapcanvas").transform) as GameObject;
        PlayerPfb.SetActive(false);
        Sprite spr = Tools.FindSpritePerName("player");
        Tools.SetObjAndSprSameSize(PlayerPfb, spr);
        PlayerPfb.GetComponent<Image>().sprite = spr;

        // 设置位置
        PlayerPfb.GetComponent<Transform>().position =
            new Vector2(Position.TilePfb.GetComponent<Transform>().position.x,
            Position.TilePfb.GetComponent<Transform>().position.y + YShift);
    }

    public void ShowPlayer()
    {
        PlayerPfb.SetActive(true);
    }
    public void GoForward()
    {
        Position = Position.Left;
    }
    public void GoBack()
    {
        Position = Position.Pre;
    }
}
