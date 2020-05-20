using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RollADice : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GM inGM;
    public bool PointerDown = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (PointerDown == true) return;
            PointerDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (PointerDown == false) return;
        PointerDown = false;
        int RealStep;
        if (inGM.DiceStatus == 0)
            RealStep = Random.Range(1, 4);
        else
            RealStep = inGM.DiceStatus;

        StartCoroutine(RollDiceAnimation(RealStep));
        
    }

    public IEnumerator RollDiceAnimation(int DiceNum)
    {
        GameObject ca = GameObject.FindGameObjectWithTag("PopCanvas");
        GameObject bg = ca.transform.Find("PoPBlackBG").gameObject;
        GameObject dice = ca.transform.Find("Dice").gameObject;
        bg.SetActive(true);
        dice.SetActive(true);
        float imageCount = 15;
        float playTime = 1.2f;
        for (int imageNum = 0; imageNum < imageCount; imageNum++)
        {
            dice.GetComponent<Image>().sprite = Tools.FindSpritePerName("dice" + Random.Range(1, 4).ToString());
            yield return new WaitForSeconds(playTime*2f/(imageCount*(imageCount+1)) * (imageNum+1f));
        }
        dice.GetComponent<Image>().sprite = Tools.FindSpritePerName("dice" + DiceNum.ToString());
        yield return new WaitForSeconds(1f);
        bg.SetActive(false);
        dice.SetActive(false);
        inGM.PlayerGoForward(DiceNum);
    }
}
