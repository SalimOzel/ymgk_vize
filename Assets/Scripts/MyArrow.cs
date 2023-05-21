using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;

public class MyArrow : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{
    public Color colorNormal;
    public Color colorOnEnter;
    public Color colorOnDown;
    public Color colorOnClick;
    public Image image;

    public Transform endLoc;
    public int zipPoint;

    public List<MyArrow> arrowsToTurnOff;
    public List<MyArrow> arrowsToTurnOn;
    public bool isThatFinalArrow;
    public bool golKenari;

    //UnityEvent OnClickEvent;

    private void Awake()
    {
        name = gameObject.name;

        if (image == null)
            image = GetComponent<Image>();
        if (endLoc == null)
            endLoc = transform.GetChild(0);
        image.color = colorNormal;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        image.color = colorOnClick;
        StartCoroutine(Bunny.instance.Move(endLoc.position, zipPoint, arrowsToTurnOff, arrowsToTurnOn, isThatFinalArrow));
        if (golKenari)
            GameManager.instance.golunkenarindangecti = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        image.color = colorOnDown;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = colorOnEnter;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = colorNormal;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        image.color = colorNormal;
    }

    
}

