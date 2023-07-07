using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundControl : MonoBehaviour, IStageEventListener
{
    BackGround[] backGrounds = new BackGround[6];
    BackGround curBg;
    EventMaster master;
    private void Awake()
    {
        master = GameManager.Resource.Load<EventMaster>("Data/EventMaster");
        backGrounds = GetComponentsInChildren<BackGround>();
        foreach (BackGround backGround in backGrounds)
        {
            backGround.gameObject.SetActive(false);
        }
    }
    private void OnEnable()
    {
        master.AddStageEventListener(this);
    }
    private void OnDisable()
    {
        master?.RemoveStageEventListener(this);
    }

    public void ChangeBG()
    {
        DataManager.World world = GameManager.data.curWorld;
        foreach (BackGround backGround in backGrounds)
        {
            if(backGround.name == world.ToString())
            {
                backGround.gameObject.SetActive(true);
                curBg = backGround;
            }
            else
                backGround.gameObject.SetActive(false);
        }
        int g = Random.Range(0, 2);
        switch ((int)world)
        {
            case 0: // fire
                if (g == 0)
                    GameManager.Sound.Play("Battle_of_Fire_I", Define.Sound.Bgm);
                else
                    GameManager.Sound.Play("Battle_of_Fire_II", Define.Sound.Bgm);
                break;
            case 1: // forest
                if (g == 0)
                    GameManager.Sound.Play("Battle_of_Nature_I", Define.Sound.Bgm);
                else
                    GameManager.Sound.Play("Battle_of_Nature_II", Define.Sound.Bgm);
                break;
            case 2: // ice
                if (g == 0)
                    GameManager.Sound.Play("Battle_of_Ice_I", Define.Sound.Bgm);
                else
                    GameManager.Sound.Play("Battle_of_Ice_II", Define.Sound.Bgm);
                break;
            case 3: // ruins
                if (g == 0)
                    GameManager.Sound.Play("Battle_of_Ruins_I", Define.Sound.Bgm);
                else
                    GameManager.Sound.Play("Battle_of_Ruins_II", Define.Sound.Bgm);
                break;
            case 4: // west
                if (g == 0)
                    GameManager.Sound.Play("Reva", Define.Sound.Bgm);
                else
                    GameManager.Sound.Play("Shiso", Define.Sound.Bgm);
                break;
        }
    }
    IEnumerator Boom()
    {
        yield return new WaitForSeconds(1.5f);
        master.ReadyToBattleInvoke();
    }
    public void NextStageEvent(string nextStage)
    {
        curBg.Scrolling(2);
        StartCoroutine(Boom());
    }

    public void GameStartEvent()
    {
        
    }

    public void NextWorldEvent()
    {
        ChangeBG();
    }

    public void StageClearEvent()
    {
        
    }

    public void ReadyToBattleEvent()
    {
        
    }
}
