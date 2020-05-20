using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LocateClick : MonoBehaviour,IPointerClickHandler
{
    public GM inGM;

    public void OnPointerClick(PointerEventData eventData)
    {
        inGM.LocateCamera();
    }
}
