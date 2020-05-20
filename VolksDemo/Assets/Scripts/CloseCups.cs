using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CloseCups : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject ca = GameObject.FindGameObjectWithTag("PopCanvas");
        GameObject PoPBlackBG = ca.transform.Find("PoPBlackBG").gameObject;
        PoPBlackBG.SetActive(false);
        GameObject AllCups = ca.transform.Find("AllCups").gameObject;
        AllCups.SetActive(false);
    }
}
