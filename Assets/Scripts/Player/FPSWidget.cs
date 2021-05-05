using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSWidget : MonoBehaviour
{
    private string label = "";
    private float count;
	
    IEnumerator Start ()
    {
        GUI.depth = 2;
        while (true) {
            if ( Mathf.Approximately(Time.timeScale, 1f)) {
                yield return new WaitForSeconds (0.1f);
                count = (1 / Time.deltaTime);
                label = "FPS :" + (Mathf.Round (count));
            } else {
                label = "Pause";
            }
            yield return new WaitForSeconds (0.5f);
        }
    }
	
    void OnGUI ()
    {
        GUI.Label (new Rect (5, 40, 100, 25), label);
    }
}
