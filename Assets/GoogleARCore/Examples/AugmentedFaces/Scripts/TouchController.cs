using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TouchController : MonoBehaviour
{

    private Touch theTouch;
    private float timeTouchEnded;
    private float displayTime = 0.5f;
    private Vector2[] touches = new Vector2[5];
    private RaycastHit2D hit;
    public Sprite noseSprite;
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
                    
                    //To-Do refactor if else statements

                    if (Physics.Raycast(ray, out hit, 100))
                    {
                        if (hit.transform.name.Equals("NoseCollider"))
                        {
                            GameObject.Find("HeaderLabel")
                                .transform.GetChild(0)
                                .GetComponent<SpriteRenderer>()
                                .sprite = noseSprite;
                        }
                        else if (hit.transform.parent.name.Equals("HeaderLabel"))
                        {
                            SpriteRenderer spriteRenderer = hit.transform.GetComponent<SpriteRenderer>();
                            Texture2D texture = spriteRenderer.sprite.texture;
                            string name2 = texture.name;
                            if (name2.Equals("NoseLabel"))
                            {
                                Application.OpenURL("https://en.wikipedia.org/wiki/Human_nose");
                            }
                        }
                        else
                        {
                            Application.OpenURL("https://en.wikipedia.org/wiki/Skull");
                        }
                    }
                }
            }
        }
    }
}
