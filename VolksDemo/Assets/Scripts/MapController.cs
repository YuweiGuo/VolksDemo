using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class MapController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public bool PointerDown;
    public float StartYPos;
    public float EndYPos;
    public GameObject Camera;

    private static MapController instance = null;
    private MapController() { }

    public static MapController GetInstance()
    {
        return instance;
    }
    private void Start()
    {
        instance = this;
        PointerDown = false;
        Camera = GameObject.FindGameObjectWithTag("mapcamera");
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (PointerDown == false) return;
        float NowYPos = eventData.position.y;
        float Distance = NowYPos - StartYPos;
        Camera.transform.DOMoveY(Camera.transform.localPosition.y - Distance/15f, 0.5f);    
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (PointerDown == true) return;
        PointerDown = true;
        StartYPos = eventData.position.y;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (PointerDown == false) return;
        PointerDown = false;
    }

}
