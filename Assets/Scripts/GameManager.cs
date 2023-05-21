using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public List<GameObject> firstArrows1;
    public List<GameObject> firstArrows2;
    public List<GameObject> firstArrows3;
    public Transform startPos1;
    public Transform startPos2;
    public Transform startPos3;
    public List<Canvas> canvases;

    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;
    public TextMeshProUGUI gorevText2;
    public TextMeshProUGUI gorevText3;
    public TextMeshProUGUI zipzipText2;
    public TextMeshProUGUI zipzipText3;
    public TextMeshProUGUI wolcomeText;
    public GameObject buttonR;
    public GameObject buttonN;
    public GameObject buttonE;

    public bool golunkenarindangecti = false;

    private int index = 0;
    public List<int> maxZipPoints;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }


    void Start() // VSync'i Android'de 
    {
        Application.targetFrameRate = Screen.currentResolution.refreshRate;
        StartCoroutine(WelcomeText());
    }

    public IEnumerator ArrivedToFinalLoc()
    {
        yield return new WaitForSecondsRealtime(.5f);

        CanvasGroup CG1 = canvases[index].GetComponent<CanvasGroup>();
        CanvasGroup CG2 = canvases[canvases.Count - 1].GetComponent<CanvasGroup>();

        text1.text = "Ziplayis Adeti: " + Bunny.instance.totalZipPoint;
        if (Bunny.instance.totalZipPoint <= maxZipPoints[index])
        {
            index++;
            if (index == 3)
            {
                if (golunkenarindangecti == true)
                {
                    text2.text = "Basardin, ulasman gereken her yere en kisa �ekilde ulastin. \n Tebrikler oyunu harika bir sekilde bitirdin!";
                    buttonN.SetActive(false);
                    buttonE.SetActive(true);
                }
                else
                {
                    text2.text = "Gölün kenarindan gecmen gerekiyordu, bir seferkine göl kenarindan gecmeyi unutma!";
                    buttonR.SetActive(true);
                    golunkenarindangecti = false;
                    index--;
                }
            }
            else
            {
                text2.text = "Basardin, ulasman gereken yere en kisa sürede vardin! \n simdi bir sonraki oyuna gec!";
                buttonN.SetActive(true);
            }
        }
        else
        {
            text2.text = "Ne yazik ki basaramadin ama hiç merak etme, tekrar deneyebilirsin!";
            buttonR.SetActive(true);
        }
        StartCoroutine(canvasThing(CG1, CG2));
    }

    public void Restart()
    {
        CanvasGroup CG2 = canvases[index].GetComponent<CanvasGroup>();
        CanvasGroup CG1 = canvases[canvases.Count - 1].GetComponent<CanvasGroup>();
        StartCoroutine(canvasThing(CG1, CG2));

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("arrow"))
        {
            go.SetActive(false);
        }

        switch (index)
        {
            case 0:
                foreach (GameObject arrow in firstArrows1)
                    arrow.SetActive(true);
                Bunny.instance.transform.position = startPos1.transform.position;
                break;
            case 1:
                foreach (GameObject arrow in firstArrows2)
                    arrow.SetActive(true);
                Bunny.instance.transform.position = startPos2.transform.position;
                break;
            case 2:
                foreach (GameObject arrow in firstArrows3)
                    arrow.SetActive(true);
                Bunny.instance.transform.position = startPos3.transform.position;
                break;

        }
        Bunny.instance.totalZipPoint = 0;
        Bunny.instance.Ziptext.text = "Ziplayis Adeti: " + 0;
        buttonR.SetActive(false);
    }

    public void Next()
    {
        CanvasGroup CG2 = canvases[index].GetComponent<CanvasGroup>();
        CanvasGroup CG1 = canvases[canvases.Count - 1].GetComponent<CanvasGroup>();
        StartCoroutine(canvasThing(CG1, CG2));

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("arrow"))
        {
            go.SetActive(false);
        }

        switch (index)
        {
            case 1:
                foreach (GameObject arrow in firstArrows2)
                    arrow.SetActive(true);
                Bunny.instance.transform.position = startPos2.transform.position;
                gorevText2.text = "Görev 2: Ağaclardan Caliliklara En Kisa Rotadan Gitmen Lazım!";
                Bunny.instance.Ziptext = zipzipText2;
                break;
            case 2:
                foreach (GameObject arrow in firstArrows3)
                    arrow.SetActive(true);
                Bunny.instance.transform.position = startPos3.transform.position;
                gorevText3.text = "Görev 3: Agaclardan Salincaklara Golun Etrafindan En Kısa Rotadan Gitmen Lazim!";
                Bunny.instance.Ziptext = zipzipText3;
                break;
        }

        Bunny.instance.totalZipPoint = 0;
        Bunny.instance.Ziptext.text = "Ziplayis Adeti: " + 0;
        Bunny.instance.gameObject.transform.SetParent(CG2.transform.GetChild(0).transform);
        buttonN.SetActive(false);
    }


    private IEnumerator canvasThing(CanvasGroup CG1, CanvasGroup CG2)
    {
        float t = 0;
        CG2.gameObject.SetActive(true);
        while (CG2.alpha < 1f)
        {
            t += 1.5f * Time.deltaTime;
            CG1.alpha = Mathf.Lerp(1, 0, t);
            CG2.alpha = Mathf.Lerp(0, 1, t);

            yield return null;
        }
        CG1.alpha = 0;
        CG1.gameObject.SetActive(false);
        CG2.alpha = 1;
    }

    private IEnumerator WelcomeText()
    {
        yield return new WaitForSeconds(5f);
        while(wolcomeText.alpha > 0)
        {
            wolcomeText.alpha -= 1f * Time.deltaTime;
            yield return null;
        }
    }

    public void Exit()
    {
        Application.Quit();
    }
}
