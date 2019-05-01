using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CheckGazePos : MonoBehaviour
{

    //public GameObject targetObjects;
    private bool showTargets = true;


    public void ToggleTargets()
    {
        showTargets = !showTargets;
        if (showTargets)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
}
