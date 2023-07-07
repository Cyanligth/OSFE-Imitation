using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckStackIcon : MonoBehaviour
{
    [SerializeField]Image image;
    public void IconSet(Sprite sprite)
    {
        image.sprite = sprite;
    }
}
