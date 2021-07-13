using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TouchPhaseDisplay : MonoBehaviour
{

    public Text phaseDisplayText;
    private Vector2[] touches = new Vector2[5];
    private RaycastHit hit;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch t in Input.touches)
            {
                touches[t.fingerId] = Camera.main.ScreenToWorldPoint(Input.GetTouch(t.fingerId).position);
                if (Input.GetTouch(t.fingerId).phase == TouchPhase.Began)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(t.fingerId).position);
                    
                    if (Physics.Raycast(ray, out hit, 100))
                    {
                        print("Hit something!");
                        string name1 = hit.collider.name;
                    }
                }
            }
        }
    }
}
