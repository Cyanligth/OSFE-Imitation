using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TitleScene : BaseScene
{
    PlayerInput input;
    private void Awake()
    {
        input = GetComponent<PlayerInput>();
    }
    protected override IEnumerator Loading()
    {
        yield return null;
    }
    private void Start()
    {
        GameManager.Sound.Play("Prelude", Define.Sound.Bgm);
    }
    private void OnChose()
    {
        
    }
    private void OnBack()
    {
        GameManager.UI.ClosePopUpUI();
    }
    private void OnEscape()
    {

    }
}
