using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools
{
    // pfb和spr的默认路径，在Asset/Resources/ 下的这两个文件夹内
    private const string PfbLocalPath = "Prefabs/";
    private const string ImageLocalPath = "Images/";

    // 根据名字找到对应路径下的pfb
    public static Object FindPfbPerName(string pfbName)
    {
        string fullName = PfbLocalPath + pfbName;
        Object pfbObj = Resources.Load<Object>(fullName);
        if (pfbObj == null)
        {
            Debug.LogError("DG: cann't find prefab under " + fullName);
            return null;
        }
        else
        {
            return pfbObj;
        }
    }
    // 根据名字找到对应路径下的spr
    public static Sprite FindSpritePerName(string sprName)
    {
        string fullName = ImageLocalPath + sprName;
        Sprite spr = Resources.Load<Sprite>(fullName);
        if (spr == null)
        {
            Debug.LogError("DG: cann't find sprite under " + fullName);
            return null;
        }
        else
        {
            return spr;
        }
    }

    // 将一个pfb和一个spr设置为同样的尺寸
    public static void SetObjAndSprSameSize(GameObject obj, Sprite spr)
    {
        obj.GetComponent<RectTransform>().sizeDelta = new Vector2(spr.textureRect.width, spr.textureRect.height);
    }
}

