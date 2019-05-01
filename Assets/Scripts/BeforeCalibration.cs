using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class BeforeCalibration : MonoBehaviour
{

    public GameObject imageBlock;
    public Image toggleEyesBtn;
    public Text toggleEyesText;
    private bool showEyes;


    public void ToggleLiveFeedOfEye()
    {
        showEyes = !showEyes;
        if (showEyes)
        {
            imageBlock.SetActive(false);
            toggleEyesBtn.GetComponent<Image>().color = Color.grey;
            toggleEyesText.text = "Fjern billede af øjne";
        }
        else
        {
            imageBlock.SetActive(true);
            toggleEyesBtn.GetComponent<Image>().color = Color.white;
            toggleEyesText.text = "Vis billede af øjne";

        }
    }

    public void ChangeSceneToMarket()
    {
        SceneManager.LoadScene("Market with 2D Calibration", LoadSceneMode.Single);
    }

}
