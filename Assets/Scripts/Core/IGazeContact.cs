using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGazeContact
{
    void OnGazeOver();
    void OnGazeEnter();
    void OnGazeExit();
}
