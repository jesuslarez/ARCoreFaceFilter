using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColourButtonController : MonoBehaviour
{
    public Material material;

    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(UpdateMaterial);
    }

    private void Update()
    {
        Color currentColour = material.color;
        Color newColour = gameObject.GetComponent<Image>().color;
        if (currentColour == newColour)
        {
            gameObject.GetComponent<Button>().Select();
        }   
    }
    private void UpdateMaterial()
    {
        Color currentColour = material.color;
        Color newColour = gameObject.GetComponent<Image>().color;
        currentColour.r = newColour.r;
        currentColour.g = newColour.g;
        currentColour.b = newColour.b;
        material.color = currentColour;
    }
}