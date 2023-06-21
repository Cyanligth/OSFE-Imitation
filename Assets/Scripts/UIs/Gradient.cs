using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gradient : MonoBehaviour
{
    public UnityEngine.Gradient gradient;

    [Range(0, 1)] public float i;

    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
        image.color = gradient.Evaluate(i);
    }

}
