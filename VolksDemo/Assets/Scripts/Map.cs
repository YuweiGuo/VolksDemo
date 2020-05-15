using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map
{
    // map的开始节点
    public Tile StartTile;

    // 两者之间的距离
    public float TileDistance = 20f;

    public void BuildMap( )
    {
        // 得到map开始位置
        GameObject MapStart = GameObject.FindGameObjectWithTag("mapstart");
        // 块个数
        int TileCount = 0;
        // 开始节点
        StartTile = new Tile();
        TileCount++;
        StartTile.InitTile(Tile.TileTypes.TILE_START, MapStart, TileCount);
        // 内容填充1，5个随机普通问题
        Tile PreTile = StartTile;
        float YShift;
        for (int tile_num = 1; tile_num <= 5; tile_num++)
        {
            // 新的tile
            Tile NewTile = new Tile();  
            TileCount++;
            NewTile.InitTile((Tile.TileTypes)Random.Range(11, 16), MapStart, TileCount);
            // 移动
            YShift = PreTile.TilePfb.GetComponent<RectTransform>().sizeDelta.y / 2.0f + 
                NewTile.TilePfb.GetComponent<RectTransform>().sizeDelta.y / 2.0f + TileDistance;
            NewTile.TilePfb.GetComponent<RectTransform>().localPosition = 
                new Vector2(PreTile.TilePfb.GetComponent<RectTransform>().localPosition.x, 
                PreTile.TilePfb.GetComponent<RectTransform>().localPosition.y + YShift);
            // 连接+转移
            PreTile.Left = NewTile; 
            NewTile.Pre = PreTile;
            PreTile = NewTile; 
        }
        // boss节点
        Tile BossTile = new Tile();
        TileCount++;
        BossTile.InitTile(Tile.TileTypes.TILE_BOSS, MapStart, TileCount);
        YShift = PreTile.TilePfb.GetComponent<RectTransform>().sizeDelta.y / 2.0f +
                BossTile.TilePfb.GetComponent<RectTransform>().sizeDelta.y / 2.0f + TileDistance;
        BossTile.TilePfb.GetComponent<RectTransform>().localPosition =
            new Vector2(PreTile.TilePfb.GetComponent<RectTransform>().localPosition.x,
            PreTile.TilePfb.GetComponent<RectTransform>().localPosition.y + YShift);
        // 连接+转移
        PreTile.Left = BossTile;
        BossTile.Pre = PreTile;
        PreTile = BossTile;

        // 奖励节点 
        Tile BoxRewardTile = new Tile();
        TileCount++;
        BoxRewardTile.InitTile(Tile.TileTypes.TILE_BOXREWARD_1, MapStart, TileCount);
        YShift = PreTile.TilePfb.GetComponent<RectTransform>().sizeDelta.y / 2.0f +
                BoxRewardTile.TilePfb.GetComponent<RectTransform>().sizeDelta.y / 2.0f + TileDistance;
        BoxRewardTile.TilePfb.GetComponent<RectTransform>().localPosition =
            new Vector2(PreTile.TilePfb.GetComponent<RectTransform>().localPosition.x,
            PreTile.TilePfb.GetComponent<RectTransform>().localPosition.y + YShift);
        // 连接+转移
        PreTile.Left = BoxRewardTile;
        BoxRewardTile.Pre = PreTile;
    }

    public void ShowMap()
    {
        Tile iterator = StartTile;
        while (iterator != null)
        {
            iterator.TilePfb.SetActive(true);
            iterator = iterator.Left;
        }
    }
}
