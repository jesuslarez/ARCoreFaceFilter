using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Material material;

    private void Start()
    {
        GetComponent<Slider>().onValueChanged.AddListener(delegate { UpdateMaterial(); });
    }

    private void UpdateMaterial()
    {
        Color colour = material.color;
        colour.a = GetComponent<Slider>().value;
        material.color = colour;
    }
}
