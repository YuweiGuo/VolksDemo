using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WordGameClick : MonoBehaviour, IPointerClickHandler
{
    public TileManager TM;
    public GM inGM;
    public void OnPointerClick(PointerEventData eventData)
    {
        switch (TM.ClickStep)
        {
            case 0 :
                Sprite spr = Tools.FindSpritePerName("wordgame_step2");
                this.GetComponent<Image>().sprite = spr;
                Tools.SetObjAndSprSameSize(this.gameObject, spr);
                TM.ClickStep += 1;
                break;
            case 1:
                this.gameObject.SetActive(false);
                TM.ClickStep = 0;
                inGM.BackFromWord = 1;
                inGM.BackFromGame();
                GameObject.Destroy(this.gameObject);
                break;
            default:
                break;
        }
    }
}
