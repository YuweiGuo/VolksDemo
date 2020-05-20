using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dice1Click : MonoBehaviour,IPointerClickHandler
{
    public GM inGM;
    public int Type = 0;


    public void OnPointerClick(PointerEventData eventData)
    {
        switch (this.gameObject.transform.name)
        {
            case "dice1_pfb(Clone)" :
                Type = 1;
                break;
            case "dice2_pfb(Clone)" :
                Type = 2;
                break;
            case "dice3_pfb(Clone)" :
                Type = 3;
                break;
            default:
                Type = 0;
                break;
        }
        inGM.DiceClick(Type);
    }
}
