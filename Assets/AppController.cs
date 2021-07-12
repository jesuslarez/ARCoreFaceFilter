using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class AppController : MonoBehaviour
{
    [SerializeField]
    public GameObject menuPanel;
    [SerializeField]
    public GameObject mainMenu;
    [SerializeField]
    public GameObject aboutPanel;
    [SerializeField]
    public GameObject settingsPanel;
    [SerializeField]
    public GameObject gameUI;

    public Material material;
    public void Play(bool value)
    {
        if (value)
        {
            menuPanel.SetActive(false);
            gameUI.SetActive(true);
        }
        else
        {
            menuPanel.SetActive(true);
            gameUI.SetActive(false);
        }

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
    public void Settings()
    {
        mainMenu.SetActive(false);
        settingsPanel.SetActive(true);
    }
    public void MainMenu()
    {
        mainMenu.SetActive(true);
        aboutPanel.SetActive(false);
        settingsPanel.SetActive(false);
    }
    public void Repository()
    {
        Application.OpenURL("https://github.com/jesuslarez/ARCoreFaceFilter");
    }


    public void TakeScreenshot()
    {
        StartCoroutine(TakeScreenshotCoroutine());
    }
    IEnumerator TakeScreenshotCoroutine()
    {
        gameUI.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        NativeGallery.SaveImageToGallery(ScreenCapture.CaptureScreenshotAsTexture(), "Screenshots", "NewImage.jpg");
        gameUI.SetActive(true);
    }
}
