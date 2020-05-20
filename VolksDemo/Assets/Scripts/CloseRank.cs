using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CloseRank : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject ca = GameObject.FindGameObjectWithTag("PopCanvas");
        GameObject PoPBlackBG = ca.transform.Find("PoPBlackBG").gameObject;
        PoPBlackBG.SetActive(false);
        GameObject FullRank = ca.transform.Find("FullRank").gameObject;
        FullRank.SetActive(false);
    }
}
