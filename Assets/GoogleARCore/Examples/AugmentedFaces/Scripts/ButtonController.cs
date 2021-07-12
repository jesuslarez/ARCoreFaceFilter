using UnityEngine;
using UnityEngine.UI;
public class ButtonController : MonoBehaviour
{
    [SerializeField]
    private Sprite switchEnabled;
    [SerializeField]
    private Sprite switchDisabled;
    private bool isEnabled = false;
    public GameObject dome;
    public GameObject jaw;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        if (isEnabled)
        {
            gameObject.GetComponent<Button>().image.sprite = switchDisabled;
            isEnabled = false;
        }
        else
        {
            gameObject.GetComponent<Button>().image.sprite = switchEnabled;
            isEnabled = true;
        }
        dome.GetComponent<Outline>().enabled = !dome.GetComponent<Outline>().enabled;
        jaw.GetComponent<Outline>().enabled = !jaw.GetComponent<Outline>().enabled;
    }
}