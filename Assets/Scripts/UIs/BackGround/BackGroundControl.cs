using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundControl : MonoBehaviour
{
    BackGround[] backGrounds = new BackGround[6];
    private void Awake()
    {
        backGrounds = GetComponentsInChildren<BackGround>();
        foreach (BackGround backGround in backGrounds)
        {
            backGround.gameObject.SetActive(false);
        }
    }

    public void ChangeBG()
    {
        DataManager.World world = GameManager.data.curWorld;
        foreach (BackGround backGround in backGrounds)
        {
            if(backGround.name == world.ToString())
                backGround.gameObject.SetActive(true);
            else
                backGround.gameObject.SetActive(false);
        }
    }
}
