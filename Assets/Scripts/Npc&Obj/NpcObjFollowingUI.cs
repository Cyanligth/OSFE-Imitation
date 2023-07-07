using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NpcObjFollowingUI : MonoBehaviour
{
    TMP_Text text;
    BaseNpc npc;
    Obstacle obstacle;
    private void Awake()
    {
        text = GetComponentInChildren<TMP_Text>();
        npc = GetComponentInParent<BaseNpc>();
        obstacle = GetComponentInParent<Obstacle>();
        if(npc != null)
            npc.OnHit.AddListener(() => { SetHp(); });
        else if (obstacle != null)
            obstacle.OnHit.AddListener(() => { SetHp(); });

    }
    private void OnEnable()
    {
        SetHp();
    }
    private void SetHp()
    {
        if(npc != null)
            text.text = npc.Hp.ToString();
        else if(obstacle != null)
            text.text = obstacle.Hp.ToString();
    }
}
