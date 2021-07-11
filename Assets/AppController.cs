using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppController : MonoBehaviour
{
    [SerializeField]
    public GameObject menuPanel;
    [SerializeField]
    public GameObject mainMenu;
    [SerializeField]
    public GameObject aboutPanel;
    public void Play(bool value)
    {
        menuPanel.SetActive(value);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void About()
    {
        mainMenu.SetActive(false);
        aboutPanel.SetActive(true);
    }
    public void MainMenu()
    {
        mainMenu.SetActive(true);
        aboutPanel.SetActive(false);
    }
    public void Repository()
    {
        Application.OpenURL("https://github.com/jesuslarez/ARCoreFaceFilter");
    }
}
