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
    private Sprite headerSprite;
    public Sprite noseSprite;
    public Sprite jawSprite;
    public Sprite zygomaticSprite;
    public Sprite maxillaSprite;
    public Sprite orbitSprite;
    public Sprite frontalBoneSprite;
    // Start is called before the first frame update

    // Update is called once per frame
    private void Start()
    {
        headerSprite = GameObject.Find("HeaderLabel").transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
    }
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
                        if (CheckCollider(hit))
                        {
                            GameObject.Find("HeaderLabel").transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = headerSprite;
                            return;
                        }
                        if (hit.transform.parent.name.Equals("HeaderLabel"))
                        {
                            CheckHeaderLabel(hit);
                        }
                    }
                }
            }
        }
    }

    private bool CheckCollider(RaycastHit hit)
    {
        string colliderName = hit.transform.name;
        if (colliderName.Equals("NoseCollider"))
        {
            headerSprite = noseSprite;
            return true;
        }
        else if (colliderName.Equals("ZygomaticColliderRight") || colliderName.Equals("ZygomaticColliderLeft"))
        {
            headerSprite = zygomaticSprite;
            return true;
        }
        else if (colliderName.Equals("MaxilaColliderLeft")
                 || colliderName.Equals("MaxilaColliderRight")
                 || colliderName.Equals("MaxilaColliderCenter"))
        {
            headerSprite = maxillaSprite;
            return true;
        }
        else if (colliderName.Equals("Jaw"))
        {
            headerSprite = jawSprite;
            return true;
        }
        else if (colliderName.Equals("OrbitColliderRight") || colliderName.Equals("OrbitColliderLeft"))
        {
            headerSprite = orbitSprite;
            return true;
        }
        else if (colliderName.Equals("FrontalBoneCollider"))
        {
            headerSprite = frontalBoneSprite;
            return true;
        }
        else if (colliderName.Equals("LacrimalCollider"))
        {
            Application.OpenURL("https://en.wikipedia.org/wiki/Lacrimal_bone");
            return false;
        }
        else if (colliderName.Equals("PalatineCollider"))
        {
            Application.OpenURL("https://en.wikipedia.org/wiki/Palatine_bone");
            return false;
        }
        else if (colliderName.Equals("VomerCollider"))
        {
            Application.OpenURL("https://en.wikipedia.org/wiki/Vomer");
            return false;
        }
        else if (colliderName.Equals("NasalConchaCollider"))
        {
            Application.OpenURL("https://en.wikipedia.org/wiki/Inferior_nasal_concha");
            return false;
        }
        return false;
    }

    private static void CheckHeaderLabel(RaycastHit hit)
    {
        SpriteRenderer spriteRenderer = hit.transform.GetComponent<SpriteRenderer>();
        Texture2D texture = spriteRenderer.sprite.texture;
        string labelText = texture.name;
        if (labelText.Equals("NoseLabel"))
        {
            Application.OpenURL("https://en.wikipedia.org/wiki/Nasal_bone");
        }
        else if (labelText.Equals("ZygomaticLabel"))
        {
            Application.OpenURL("https://en.wikipedia.org/wiki/Zygomatic_bone");
        }
        else if (labelText.Equals("MaxillaLabel"))
        {
            Application.OpenURL("https://en.wikipedia.org/wiki/Maxilla");
        }
        else if (labelText.Equals("MandibleLabel"))
        {
            Application.OpenURL("https://en.wikipedia.org/wiki/Mandible");
        }
        else if (labelText.Equals("OrbitLabel"))
        {
            Application.OpenURL("https://en.wikipedia.org/wiki/Orbit_(anatomy)");
        }
        else if (labelText.Equals("FrontalBoneLabel"))
        {
            Application.OpenURL("https://en.wikipedia.org/wiki/Frontal_bone");
        }
    }
}
