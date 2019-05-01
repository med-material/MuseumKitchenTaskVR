using UnityEngine;

public class EyeRay : MonoBehaviour
{

    private LineRenderer line;
    private GameObject _prevHit = null;
    public static RaycastHit CurrentlyHit;

    void Start()
    {
        line = GetComponent<LineRenderer>();

        line.startWidth = 0.01f;
        line.endWidth = 0.005f;
    }



    void Update()
    {
        var gaze = new Vector3(PupilData._2D.GazePosition.x, PupilData._2D.GazePosition.y, 2.0f);
        UpdateLine(gaze);

        var dir = (Camera.main.ViewportToWorldPoint(gaze) - Camera.main.transform.localPosition).normalized;
        var ray = new Ray(Camera.main.transform.localPosition, dir);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            var go = hit.transform.gameObject;
            CurrentlyHit = hit;
            if (go == null) return;
            if (go.GetComponent<IGazeContact>() == null) return;

            if (_prevHit != go)
            {
                if (_prevHit != null)
                    _prevHit.GetComponent<IGazeContact>().OnGazeExit();
                go.GetComponent<IGazeContact>().OnGazeEnter();
            }
            else if (_prevHit == go)
            {
                go.GetComponent<IGazeContact>().OnGazeOver();
            }

            _prevHit = go;
        }
        CurrentlyHit = hit;

    }

    private void UpdateLine(Vector3 gazePoint)
    {

        line.SetPosition(0, Camera.main.transform.localPosition + new Vector3(0, 0.1f, 0));
        line.SetPosition(1, Camera.main.ViewportToWorldPoint(gazePoint));
    }
}