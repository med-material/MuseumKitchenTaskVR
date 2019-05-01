using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;


public class PaintingsSets : MonoBehaviour
{

    public static PaintingsSet CurrentSet = null;
    private List<PaintingsSet> _paintingsSets = new List<PaintingsSet>();
    public static float prevDelta = 0;
    private int TimePerSet = 60;
    public static bool paintingsSeen = false;

    private int[] myIntArray = { 1, 2, 3 };
    public static string myIntArrayString;

    // Use this for initialization
    void Start()
    {
        // Run in Build mode: remember to move the paintings folder to the apk data folder for every build
        // TODO: fix this, so we don't have to copy and paste the images to the new build folder 
        var info = new DirectoryInfo(Application.dataPath + "\\Resources\\MuseumAssets\\Paintings");
        var sets = new List<DirectoryInfo>(info.GetDirectories());

        foreach (var t in sets)
        {
            var files = t.GetFiles("*.jpg").ToList();
            files = files.Concat(t.GetFiles("*.png")).ToList();
            _paintingsSets.Add(new PaintingsSet
            {
                SetName = t.Name,
                Picture1 = Resources.Load("MuseumAssets\\Paintings\\" + t.Name + "\\" + Path.GetFileNameWithoutExtension(files.ElementAt(0).Name)) as Texture2D,
                Picture2 = Resources.Load("MuseumAssets\\Paintings\\" + t.Name + "\\" + Path.GetFileNameWithoutExtension(files.ElementAt(1).Name)) as Texture2D,
                Picture3 = Resources.Load("MuseumAssets\\Paintings\\" + t.Name + "\\" + Path.GetFileNameWithoutExtension(files.ElementAt(2).Name)) as Texture2D,
            });
        }
        CurrentSet = _paintingsSets.FirstOrDefault();
        SetPaintings(_paintingsSets.FirstOrDefault());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            DisplayNextSet(KeyCode.LeftArrow);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            DisplayNextSet(KeyCode.RightArrow);
        }
        if (SceneTimer.timerActivated && SceneManage.loadTestScene == 3)
        {
            prevDelta += Time.deltaTime;

            if (prevDelta >= TimePerSet)
            {
                DisplayNextSet(KeyCode.RightArrow);
                prevDelta = 0;
            }
        }
    }

    public void PreviousBtn()
    {
        DisplayNextSet(KeyCode.LeftArrow);
        prevDelta = 0;
    }

    public void NextBtn()
    {
        DisplayNextSet(KeyCode.RightArrow);
        prevDelta = 0;
    }

    private void DisplayNextSet(KeyCode key)
    {
        var index = _paintingsSets.FindIndex(x => x == CurrentSet);
        switch (key)
        {
            case KeyCode.LeftArrow:
                index -= 1;
                break;
            case KeyCode.RightArrow:
                index += 1;
                break;
        }
        if (index < 0)
            index = _paintingsSets.Count - 1;
        else if (index == _paintingsSets.Count)
        {
            paintingsSeen = true;
            index = 0;
        }
        SetPaintings(_paintingsSets[index]);
        CurrentSet = _paintingsSets[index];
    }

    private void SetPaintings(PaintingsSet set)
    {
        //randomises which paintings are shown within the set
        for (int t = 0; t < myIntArray.Length; t++)
        {
            int tmp = myIntArray[t];
            int r = Random.Range(0, myIntArray.Length);
            myIntArray[t] = myIntArray[r];
            myIntArray[r] = tmp;
        }

        // check the painting order
        print("Paint" + myIntArray[0]);
        print("Paint" + myIntArray[1]);
        print("Paint" + myIntArray[2]);

        myIntArrayString = myIntArray[0].ToString() + myIntArray[1].ToString() + myIntArray[2].ToString();
        print(myIntArrayString);


        GameObject.Find("Paint" + (myIntArray[0])).GetComponent<Renderer>().material.mainTexture = set.Picture1;
        GameObject.Find("Paint" + (myIntArray[1])).GetComponent<Renderer>().material.mainTexture = set.Picture2;
        GameObject.Find("Paint" + (myIntArray[2])).GetComponent<Renderer>().material.mainTexture = set.Picture3;
    }
}


public class PaintingsSet
{
    public string SetName { get; set; }

    public Texture2D Picture1 { get; set; }
    public string Picture1Name { get; set; }
    public Texture2D Picture2 { get; set; }
    public string Picture2Name { get; set; }
    public Texture2D Picture3 { get; set; }
    public string Picture3Name { get; set; }
}