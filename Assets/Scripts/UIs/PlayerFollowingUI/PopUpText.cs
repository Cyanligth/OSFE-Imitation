using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpText : MonoBehaviour
{
    TMP_Text text;
    PlayerAttack playerAttack;
    Animator animator;
    private void Awake()
    {
        text = GetComponent<TMP_Text>();
        animator = GetComponent<Animator>();
        Color color = new Color(1, 1, 1);
        color.a = 0;
        text.color = color;
        playerAttack = GetComponentInParent<PlayerAttack>();
        playerAttack.OnManaLow.AddListener((float f) => { ManalessPopUpText(f); });
    }

    public void ManalessPopUpText(float f)
    {
        animator.SetTrigger("Pop");
        text.text = "Need more mana!(" + string.Format("{0:0.0}", f) + ")";
    }

    /*
    IEnumerator TextFadeOut()
    {
        Color color = new Color(1, 1, 1);
        color.a = 1;
        float time = 1.5f;
        while(time > 0)
        {
            if (time > 1)
            {
                time -= Time.deltaTime;
                continue;
            }
            color.a = time;
            text.color = color;
            time -= Time.deltaTime;
            yield return null;
        }
    }
    */
}