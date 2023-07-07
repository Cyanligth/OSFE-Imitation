using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SinglePlayer : ButtonBase
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        GameManager.Scene.LoadScene("GameScene");
    }
}
