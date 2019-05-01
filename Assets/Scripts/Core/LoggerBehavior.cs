// originally created by Theo and Kiefer (French interns at AAU Fall 2017) 
// modified by Bianca

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace RockVR.Video.Demo
{
    public class LoggerBehavior : MonoBehaviour
    {

        #region Public editor fields

        #endregion

        private static Logger _logger;
        private static List<object> _toLog;
        private Vector3 gazeToWorld;
        private static string CSVheader = AppConstants.CsvFirstRow;
        [SerializeField] private Camera dedicatedCapture;
        //[SerializeField] private Transform viveCtrlRight;
        public InputField personID;

        public static string sceneName = "_";


        #region Unity Methods

        private void Start()
        {
            _toLog = new List<object>();
        }

        private void Update()
        {
            if (Camera.main != null)
            {
                dedicatedCapture.transform.position = Camera.main.transform.position;
                dedicatedCapture.transform.rotation = Camera.main.transform.rotation;
            }

            // synchronizes data logging with video capture beginning and end
            if (VideoCaptureCtrl.instance.status != VideoCaptureCtrl.StatusType.STARTED || VideoCaptureCtrl.instance.status == VideoCaptureCtrl.StatusType.STOPPED) return;

            DoLog();

            if (SceneManage.loadTestScene == -2 && PupilManager.calibrationStarted)
            {
                SceneManage.loadTestScene = -1;
            }

            AddToLog();
        }


        private void AddToLog()
        {
            if (PupilData._2D.GazePosition != Vector2.zero) // gives an error when redoing the eye-tracking calibration
            {
                gazeToWorld = dedicatedCapture.ViewportToWorldPoint(new Vector3(PupilData._2D.GazePosition.x, PupilData._2D.GazePosition.y, Camera.main.nearClipPlane));
            }

            var raycastHit = EyeRay.CurrentlyHit;
            var tmp = new
            {
                // default variables for all scenes
                aa = personID.text,
                a = DateTime.Now,
                b = (int)(1.0f / Time.unscaledDeltaTime), // frames per second during the last frame, could calucate an average frame rate instead
                c = PupilManager.SceneClass.sceneStat,
                cc = SceneTimer.sceneTimer,
                d = dedicatedCapture.transform.position.x,
                e = dedicatedCapture.transform.position.y,
                f = dedicatedCapture.transform.position.z,
                g = dedicatedCapture.transform.rotation.x,
                h = dedicatedCapture.transform.rotation.y,
                i = dedicatedCapture.transform.rotation.z,
                j = PupilData._2D.GazePosition != Vector2.zero ? PupilData._2D.GazePosition.x : double.NaN,
                k = PupilData._2D.GazePosition != Vector2.zero ? PupilData._2D.GazePosition.y : double.NaN,
                l = PupilData._2D.GazePosition != Vector2.zero ? gazeToWorld.x : double.NaN,
                m = PupilData._2D.GazePosition != Vector2.zero ? gazeToWorld.y : double.NaN,
                n = PupilData._2D.GazePosition != Vector2.zero ? PupilTools.FloatFromDictionary(PupilTools.gazeDictionary, "confidence") : double.NaN, // confidence value calculated after calibration 

                // Baking tray variables
                o = SceneManage.loadTestScene == 2 ? SceneManage.getViveCtrlRight.transform.position.x : double.NaN,
                p = SceneManage.loadTestScene == 2 ? SceneManage.getViveCtrlRight.transform.position.y : double.NaN,
                q = SceneManage.loadTestScene == 2 ? SceneManage.getViveCtrlRight.transform.position.z : double.NaN,
                r = SceneManage.loadTestScene == 2 ? SceneManage.getViveCtrlRight.transform.rotation.x : double.NaN,
                s = SceneManage.loadTestScene == 2 ? SceneManage.getViveCtrlRight.transform.rotation.y : double.NaN,
                t = SceneManage.loadTestScene == 2 ? SceneManage.getViveCtrlRight.transform.rotation.z : double.NaN,
                tt = SceneManage.loadTestScene == 2 ? ControllerGrabObject.bunLastActive.ctrlEventHolder : "null",
                ttt = SceneManage.loadTestScene == 2 && ControllerGrabObject.objectInHand != null ? ControllerGrabObject.bunLastActive.bunActive.name : "null",
                u = SceneManage.loadTestScene == 2 && ControllerGrabObject.objectInHand != null ? ControllerGrabObject.bunLastActive.bunActive.transform.position.x : double.NaN,
                v = SceneManage.loadTestScene == 2 && ControllerGrabObject.objectInHand != null ? ControllerGrabObject.bunLastActive.bunActive.transform.position.y : double.NaN,
                x = SceneManage.loadTestScene == 2 && ControllerGrabObject.objectInHand != null ? ControllerGrabObject.bunLastActive.bunActive.transform.position.z : double.NaN,

                // Museum variables
                cbis = SceneManage.loadTestScene == 3 && PaintingsSets.CurrentSet != null ? PaintingsSets.CurrentSet.SetName : "null",
                ord = SceneManage.loadTestScene == 3 ? PaintingsSets.myIntArrayString : "null",
                oo = SceneManage.loadTestScene == 3 && raycastHit.transform != null ? raycastHit.transform.name : "null",
                ox = SceneManage.loadTestScene == 3 && raycastHit.transform != null ? raycastHit.transform.position.x : double.NaN,
                oy = SceneManage.loadTestScene == 3 && raycastHit.transform != null ? raycastHit.transform.position.y : double.NaN,
                oz = SceneManage.loadTestScene == 3 && raycastHit.transform != null ? raycastHit.transform.position.z : double.NaN,
                oox = SceneManage.loadTestScene == 3 && raycastHit.transform != null ? CalculEyeGazeOnObject(raycastHit).x : double.NaN,
                ooy = SceneManage.loadTestScene == 3 && raycastHit.transform != null ? CalculEyeGazeOnObject(raycastHit).y : double.NaN,
                ooz = SceneManage.loadTestScene == 3 && raycastHit.transform != null ? CalculEyeGazeOnObject(raycastHit).z : double.NaN
            };
            _toLog.Add(tmp);
        }

        private Vector3 CalculEyeGazeOnObject(RaycastHit hit)
        {
            return hit.transform.InverseTransformPoint(hit.point);
        }

        public static void DoLog()
        {
            CSVheader = AppConstants.CsvFirstRow;
            _logger = Logger.Instance;
            if (_toLog.Count == 0 && SceneManage.loadTestScene == -2)
            {
                var firstRow = new { CSVheader };
                _toLog.Add(firstRow);
            }
            _logger.Log(_toLog.ToArray());
            _toLog.Clear();
        }

        #endregion
    }
}