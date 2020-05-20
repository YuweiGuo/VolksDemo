﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChooseBoxClick : MonoBehaviour, IPointerClickHandler
{
    public TileManager TM;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (TM.ClickStep == 0)
        {
            int NextStepOpt = Random.Range(0, 100);
            if (NextStepOpt < 70)
            {
                TM.ClickStep = 3;
            }
            else
            if (NextStepOpt < 90)
            {
                TM.ClickStep = 2;
            }
            else
            {
                TM.ClickStep = 1;
            }
        }
        Sprite spr;
        switch (TM.ClickStep)
        {
            case 1:
                spr = Tools.FindSpritePerName("choosegame_step2");
                this.GetComponent<Image>().sprite = spr;
                Tools.SetObjAndSprSameSize(this.gameObject, spr);
                TM.ClickStep += 1;
                break;
            case 2:
                spr = Tools.FindSpritePerName("choosegame_step3");
                this.GetComponent<Image>().sprite = spr;
                Tools.SetObjAndSprSameSize(this.gameObject, spr);
                TM.ClickStep += 1;
                break;
            case 3:
                spr = Tools.FindSpritePerName("collectgame_step3");
                this.GetComponent<Image>().sprite = spr;
                Tools.SetObjAndSprSameSize(this.gameObject, spr);
                TM.ClickStep += 1;
                break;
            case 4:
                this.gameObject.SetActive(false);
                GameObject.Destroy(this.gameObject);
                TM.ClickStep = 0;
                break;
            default:
                break;
        }
    }
}
