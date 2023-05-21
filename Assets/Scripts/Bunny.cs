using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bunny : MonoBehaviour
{
    public static Bunny instance;
    public float totalZipPoint;
    public TextMeshProUGUI Ziptext;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public IEnumerator Move(Vector2 endLoc, int zipPoint, List<MyArrow> arrowsToTurnOff, List<MyArrow> arrowsToTurnOn, bool isThatFinal)
    {
        Vector2 startLoc = transform.position;
        float percentage = 0;
        float currentZipPoint;
    
        while (Vector2.Distance(transform.position, endLoc) > .75f)
        {
            percentage = (transform.position.y - startLoc.y) / (endLoc.y - startLoc.y);
            currentZipPoint = Mathf.Lerp(0, zipPoint, percentage);
            Ziptext.text = "Ziplayis Adeti: " + (totalZipPoint + currentZipPoint).ToString("F0");
            transform.position = Vector2.Lerp(transform.position, endLoc, 4f * Time.deltaTime);
            yield return null;
        }
        totalZipPoint += zipPoint;
        Ziptext.text = "Ziplayis Adeti: " + totalZipPoint.ToString("F0");

        foreach (MyArrow arrow in arrowsToTurnOff)
            arrow.gameObject.SetActive(false);
        foreach (MyArrow arrow in arrowsToTurnOn)
            arrow.gameObject.SetActive(true);

        if (isThatFinal)
            StartCoroutine(GameManager.instance.ArrivedToFinalLoc());


    }
}
