using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SceneTimer : MonoBehaviour
{
    public GameObject kitchenObjs, bakingObjs, startUI, menuObj, startBtns, MuseumObj;
    public Text sceneTimerTxt, totalTimerTxt, timerBtnTxt;
    public static int sceneTimer;
    public Image timerBtnImage;

    private float sceneTimerF, totalTimerF;

    public static bool timerActivated = false;

    // used for data logging in LoggerBehavior.cs
    public class SceneTimerClass
    {
        public static int sceneStat
        {
            get
            {
                return sceneTimer;
            }
        }
    }

    void Start()
    {
        sceneTimerTxt.text = "Scene: 0";
        totalTimerTxt.text = "Total: 0";
    }

    void Update()
    {
        totalTimerF += Time.deltaTime;
        totalTimerTxt.text = "Total: " + Mathf.RoundToInt(totalTimerF).ToString();

        if (timerActivated && (SceneManage.loadTestScene == 1 || SceneManage.loadTestScene == 2))
        {
            sceneTimerF += Time.deltaTime;
            sceneTimer = Mathf.RoundToInt(sceneTimerF);
        }

        if (SceneManage.loadTestScene == 1)
        {
            sceneTimerTxt.text = "Scene: " + Mathf.RoundToInt(sceneTimerF).ToString() + " / 120";
            if (sceneTimer >= 10) //120
            {
                kitchenObjs.SetActive(false);
                ResetTimer();
            }
        }
        else if (SceneManage.loadTestScene == 2)
        {
            sceneTimerTxt.text = "Scene: " + Mathf.RoundToInt(sceneTimerF).ToString() + " / 300";

            if (sceneTimer >= 300)
            {
                bakingObjs.SetActive(false);
                ResetTimer();
            }
        }
        else if (SceneManage.loadTestScene == 3)
        {
            sceneTimerF = PaintingsSets.prevDelta;
            sceneTimerTxt.text = "Scene: " + Mathf.RoundToInt(sceneTimerF).ToString() + " / 60 per sæt";
            sceneTimer = Mathf.RoundToInt(sceneTimerF);
        }
        if (PaintingsSets.paintingsSeen)
        {
            //ResetToMenu();
            MuseumObj.SetActive(false);
            ResetTimer();
            PaintingsSets.paintingsSeen = false;
        }
    }

    public void StartTimer()
    {
        timerActivated = true;
        timerBtnTxt.text = "Continue";
    }

    public void PauseTimer()
    {
        timerActivated = false;
    }

    public void ResetTimer()
    {
        sceneTimerTxt.text = "Scene: 0";
        startUI.SetActive(true);
        sceneTimerF = 0f;
        timerActivated = false;
        timerBtnTxt.text = "Start";
    }

    public void ResetToMenu()
    {
        ResetTimer();
        menuObj.SetActive(true);
        startBtns.SetActive(true);
        MuseumObj.SetActive(false);
    }

}
