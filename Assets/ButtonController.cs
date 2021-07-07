using UnityEngine;
using UnityEngine.UI;
public class ButtonController : MonoBehaviour
{
    public GameObject dome;
    public GameObject jaw;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        dome.GetComponent<Outline>().enabled = !dome.GetComponent<Outline>().enabled;
        jaw.GetComponent<Outline>().enabled = !jaw.GetComponent<Outline>().enabled;
    }
}