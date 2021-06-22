using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TouchPhaseDisplay : MonoBehaviour
{

    private Touch theTouch;
    private float timeTouchEnded;
    private float displayTime = 0.5f;
    private Vector2[] touches = new Vector2[5];
    private RaycastHit2D hit;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            theTouch = Input.GetTouch(0);
            foreach (Touch t in Input.touches)
            {
                touches[t.fingerId] = Camera.main.ScreenToWorldPoint(Input.GetTouch(t.fingerId).position);
                if (Input.GetTouch(t.fingerId).phase == TouchPhase.Began)
                {
                    Ray ray = Camera.main.ScreenPointToRay(theTouch.position);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 100))
                    {
                        Application.OpenURL("https://en.wikipedia.org/wiki/Skull");
                    }
                }
            }
        }
    }
}
