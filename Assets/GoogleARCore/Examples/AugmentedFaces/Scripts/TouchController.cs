using UnityEngine;
using UnityEngine.UI;

public class TouchController : MonoBehaviour
{

    private Touch theTouch;
    private Vector2[] touches = new Vector2[5];
    private RaycastHit hit;
    private Sprite headerSprite;
    public Sprite noseSprite;
    public Sprite jawSprite;
    public Sprite zygomaticSprite;
    public Sprite maxillaSprite;
    public Sprite orbitSprite;
    public Sprite frontalBoneSprite;
    public Sprite SphenoidSprite;
    public Sprite parietalSprite;
    public Sprite temporalSprite;
    public Sprite occipitalSprite;
    // Start is called before the first frame update

    // Update is called once per frame
    private void Start()
    {
        headerSprite = GameObject.Find("HeaderButton").GetComponent<Image>().sprite;
        GameObject.Find("HeaderButton").GetComponent<Button>().onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
        CheckHeaderLabel();
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
                        this.hit = hit;
                        if (CheckCollider(hit))
                        {
                            GameObject.Find("HeaderButton").GetComponent<Image>().sprite = headerSprite;
                            return;
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
        else if (colliderName.Equals("SphenoidCollider"))
        {
            headerSprite = SphenoidSprite;
            return true;
        }
        else if (colliderName.Equals("ParietalCollider"))
        {
            headerSprite = parietalSprite;
            return true;
        }
        else if (colliderName.Equals("TemporalCollider"))
        {
            headerSprite = temporalSprite;
            return true;
        }
        else if (colliderName.Equals("OccipitalCollider"))
        {
            headerSprite = occipitalSprite;
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

    private static void CheckHeaderLabel()
    {
        string labelText = GameObject.Find("HeaderButton").GetComponent<Image>().sprite.name;
        if (labelText.Equals("NasalLabel"))
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
        else if (labelText.Equals("FrontalBone"))
        {
            Application.OpenURL("https://en.wikipedia.org/wiki/Frontal_bone");
        }
        else if (labelText.Equals("Sphenoid"))
        {
            Application.OpenURL("https://en.wikipedia.org/wiki/Sphenoid_bone");
        }
        else if (labelText.Equals("Parietal"))
        {
            Application.OpenURL("https://en.wikipedia.org/wiki/Parietal_bone");
        }
        else if (labelText.Equals("Temporal"))
        {
            Application.OpenURL("https://en.wikipedia.org/wiki/Temporal_bone");
        }
        else if (labelText.Equals("Occipital"))
        {
            Application.OpenURL("https://en.wikipedia.org/wiki/Occipital_bone");
        }
    }
}
