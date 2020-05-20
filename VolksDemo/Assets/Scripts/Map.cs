using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map
{
    // map的开始节点
    public Tile StartTile;

    // 两者之间的距离
    public float TileDistance = 20f;

    // GM
    public GM inGM;

    public void BuildMap( )
    {
        // 得到map开始位置
        GameObject MapStart = GameObject.FindGameObjectWithTag("mapstart");
        // 块个数
        int TileCount = 0;
        // 开始节点
        StartTile = new Tile();
        TileCount++;
        StartTile.InitTile(Tile.TileTypes.TILE_START, MapStart, TileCount, inGM);
        // 内容填充1，5个随机普通问题
        Tile PreTile = StartTile;
        float YShift;
        for (int tile_num = 1; tile_num <= 100; tile_num++)
        {
            // 新的tile
            Tile NewTile = new Tile();  
            TileCount++;
            NewTile.InitTile((Tile.TileTypes)Random.Range(6, 16), MapStart, TileCount,inGM);
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
        BossTile.InitTile(Tile.TileTypes.TILE_BOSS, MapStart, TileCount,inGM);
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
        BoxRewardTile.InitTile(Tile.TileTypes.TILE_BOXREWARD_1, MapStart, TileCount,inGM);
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

    public void BuildMapWithSubMapIds(List<int> SubMapIds)
    {
        if (SubMapIds.Count == 0) return;
        int TileCount = 0;
        // 创建起点
        TileCount++;
        GameObject MapStart = GameObject.FindGameObjectWithTag("mapstart");
        StartTile = new Tile();
        StartTile.InitTile(Tile.TileTypes.TILE_START, MapStart, TileCount, inGM);

        // 创建当前Tile节点
        Tile TileNow = StartTile;
        
        // 开始根据开始节点配置
        for (int Num = 0; Num < SubMapIds.Count; Num++)
        {    
            int IDNow = SubMapIds[Num];
            switch (IDNow)
            {
                case 0: // 3 random questions
                    Tile NewTile;
                    for (int tile_num = 1; tile_num <= 3; tile_num++)
                    {
                        // 新的tile
                        NewTile = new Tile();
                        TileCount++;
                        NewTile.InitTile((Tile.TileTypes)Random.Range(11, 16), MapStart, TileCount, inGM);
                        // 移动
                        SetTilePosition(NewTile, TileNow);
                        // 连接+转移
                        LinkTile(NewTile, ref TileNow);
                        
                    }
                    break;
                case 1: // 3 random things
                    for (int tile_num = 1; tile_num <= 3; tile_num++)
                    {
                        // 新的tile
                        NewTile = new Tile();
                        TileCount++;
                        NewTile.InitTile((Tile.TileTypes)Random.Range(6, 16), MapStart, TileCount, inGM);
                        // 移动
                        SetTilePosition(NewTile, TileNow);
                        // 连接+转移
                        LinkTile(NewTile, ref TileNow);
                    }
                    break;
                case 2: // 1 question + 2 bombs 
                    // 新的question tile
                    NewTile = new Tile();
                    TileCount++;
                    NewTile.InitTile((Tile.TileTypes)Random.Range(11, 16), MapStart, TileCount, inGM);
                    
                    // 移动
                    SetTilePosition(NewTile, TileNow);
                    // 连接+转移
                    LinkTile(NewTile, ref TileNow);

                    // 两个bomb
                    for (int tile_num = 1; tile_num <= 2; tile_num++)
                    {
                        // 新的tile
                        NewTile = new Tile();
                        TileCount++;
                        NewTile.InitTile((Tile.TileTypes)6, MapStart, TileCount, inGM);
                        
                        // 移动
                        SetTilePosition(NewTile, TileNow);
                        // 连接+转移
                        LinkTile(NewTile, ref TileNow);
                    }
                    break;
                case 3: // 2 lucky + question
                    for (int tile_num = 1; tile_num <= 2; tile_num++)
                    {
                        // 新的tile
                        NewTile = new Tile();
                        TileCount++;
                        NewTile.InitTile((Tile.TileTypes)Random.Range(9, 11), MapStart, TileCount, inGM);
                        
                        // 移动
                        SetTilePosition(NewTile, TileNow);
                        // 连接+转移
                        LinkTile(NewTile, ref TileNow);
                    }
                    // 1 que
                    NewTile = new Tile();
                    TileCount++;
                    NewTile.InitTile((Tile.TileTypes)Random.Range(11, 16), MapStart, TileCount, inGM);
                    
                    // 移动
                    SetTilePosition(NewTile, TileNow);
                    // 连接+转移
                    LinkTile(NewTile, ref TileNow);
                    break;
                case 4: // 2 random + 1 bomb
                    for (int tile_num = 1; tile_num <= 2; tile_num++)
                    {
                        // 新的tile
                        NewTile = new Tile();
                        TileCount++;
                        NewTile.InitTile((Tile.TileTypes)Random.Range(7, 16), MapStart, TileCount, inGM);
                        
                        // 移动
                        SetTilePosition(NewTile, TileNow);
                        // 连接+转移
                        LinkTile(NewTile, ref TileNow);
                    }
                    // 1 que
                    NewTile = new Tile();
                    TileCount++;
                    NewTile.InitTile((Tile.TileTypes)6, MapStart, TileCount, inGM);
                    
                    // 移动
                    SetTilePosition(NewTile, TileNow);
                    // 连接+转移
                    LinkTile(NewTile, ref TileNow);
                    break;
                case 5:// boss +normal rewards + start
                    // boss
                    NewTile = new Tile();
                    TileCount++;
                    NewTile.InitTile((Tile.TileTypes)4, MapStart, TileCount, inGM);
                    
                    // 移动
                    SetTilePosition(NewTile, TileNow);
                    // 连接+转移
                    LinkTile(NewTile, ref TileNow);

                    // reward
                    NewTile = new Tile();
                    TileCount++;
                    NewTile.InitTile((Tile.TileTypes)2, MapStart, TileCount, inGM);
                    
                    // 移动
                    SetTilePosition(NewTile, TileNow);
                    // 连接+转移
                    LinkTile(NewTile, ref TileNow);

                    // start
                    NewTile = new Tile();
                    TileCount++;
                    NewTile.InitTile(0, MapStart, TileCount, inGM);
                    
                    // 移动
                    SetTilePosition(NewTile, TileNow);
                    // 连接+转移
                    LinkTile(NewTile, ref TileNow);
                    break;
                case 6:// boss + big rewards + cup
                    // boss
                    NewTile = new Tile();
                    TileCount++;
                    NewTile.InitTile((Tile.TileTypes)4, MapStart, TileCount, inGM);
                    
                    // 移动
                    SetTilePosition(NewTile, TileNow);
                    // 连接+转移
                    LinkTile(NewTile, ref TileNow);

                    // reward
                    NewTile = new Tile();
                    TileCount++;
                    NewTile.InitTile((Tile.TileTypes)1, MapStart, TileCount, inGM);
                    
                    // 移动
                    SetTilePosition(NewTile, TileNow);
                    // 连接+转移
                    LinkTile(NewTile, ref TileNow);

                    // start
                    NewTile = new Tile();
                    TileCount++;
                    NewTile.InitTile((Tile.TileTypes)3, MapStart, TileCount, inGM);
                    
                    // 移动
                    SetTilePosition(NewTile, TileNow);
                    // 连接+转移
                    LinkTile(NewTile, ref TileNow);
                    break;
                default:
                    for (int tile_num = 1; tile_num <= 100; tile_num++)
                    {
                        // 新的tile
                        NewTile = new Tile();
                        TileCount++;
                        NewTile.InitTile((Tile.TileTypes)Random.Range(11, 16), MapStart, TileCount, inGM);
                        
                        // 移动
                        SetTilePosition(NewTile, TileNow);
                        // 连接+转移
                        LinkTile(NewTile, ref TileNow);
                    }
                    break;
            }
        }
        
    }

    private void SetTilePosition(Tile NewTile, Tile TileNow)
    {
        float YShift;
        // 移动
        YShift = TileNow.TilePfb.GetComponent<RectTransform>().sizeDelta.y / 2.0f +
            NewTile.TilePfb.GetComponent<RectTransform>().sizeDelta.y / 2.0f + TileDistance;
        NewTile.TilePfb.GetComponent<RectTransform>().localPosition =
            new Vector2(TileNow.TilePfb.GetComponent<RectTransform>().localPosition.x,
            TileNow.TilePfb.GetComponent<RectTransform>().localPosition.y + YShift);
    }
    private void LinkTile(Tile NewTile, ref Tile TileNow)
    {
        TileNow.Left = NewTile;
        NewTile.Pre = TileNow;
        TileNow = NewTile;
    }
}
