using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppController : MonoBehaviour
{
    [SerializeField]
    public GameObject mainMenu;
    [SerializeField]
    public GameObject faceFilter;
    private void Awake()
    {
        //Time.timeScale = 0.0f;
    }
    public void Play()
    {
        mainMenu.SetActive(false);
      //  Time.timeScale = 1.0f;
    }
    public void Quit()
    {
        Application.Quit();
    }
}
