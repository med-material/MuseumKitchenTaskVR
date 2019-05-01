using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChangeCurrentSceneTxt : MonoBehaviour
{

    private Text currentSceneTxt;

    // Use this for initialization
    void Start()
    {
        currentSceneTxt = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            currentSceneTxt.text = SceneManage.currentScene;
        }
    }
}
