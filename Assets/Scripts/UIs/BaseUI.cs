using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BaseUI : MonoBehaviour
{
    protected Dictionary<string, RectTransform> transforms;
    protected Dictionary<string, Button> buttons;
    protected Dictionary<string, TMP_Text> texts;
    protected Dictionary<string, Image> images;
    protected Dictionary<string, Sprite> sprites;
    protected Dictionary<string, Slider> sliders;

    protected virtual void Awake()
    {
        BindChildren();
    }

    private void BindChildren()
    {
        transforms = new Dictionary<string, RectTransform>();
        buttons = new Dictionary<string, Button>();
        texts = new Dictionary<string, TMP_Text>();
        images = new Dictionary<string, Image>();
        sprites = new Dictionary<string, Sprite>();
        sliders = new Dictionary<string, Slider>();

        RectTransform[] children = GetComponentsInChildren<RectTransform>();
        foreach (RectTransform child in children)
        {
            string key = child.gameObject.name;
            if (transforms.ContainsKey(key))
                continue;
            transforms.Add(key, child);

            Button button = child.GetComponent<Button>();
            if (button != null)
                buttons.Add(key, button);

            TMP_Text text = child.GetComponent<TMP_Text>();
            if (text != null)
                texts.Add(key, text);

            Image image = child.GetComponent<Image>();
            if(image != null)
                images.Add(key, image);

            Sprite sprite = child.GetComponent<Sprite>();
            if(sprite != null)
                sprites.Add(key, sprite);

            Slider slider = child.GetComponent<Slider>();
            if (slider != null)
                sliders.Add(key, slider);
        }
    }
}
