using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyFollowingCanvas : MonoBehaviour
{
    TMP_Text text;
    BaseEnemy baseEnemy;
    private void Awake()
    {
        text = GetComponentInChildren<TMP_Text>();
        baseEnemy = GetComponentInParent<BaseEnemy>();
        baseEnemy.OnHit.AddListener(() => { SetHp(); });
    }
    private void OnEnable()
    {
        SetHp();
    }
    private void SetHp()
    {
        text.text = baseEnemy.curHp.ToString();
    }
}
