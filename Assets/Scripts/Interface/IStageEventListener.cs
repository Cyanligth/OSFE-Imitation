using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStageEventListener
{
    public abstract void GameStartEvent();
    public abstract void StageClearEvent();
    public abstract void ReadyToBattleEvent();
    public abstract void NextStageEvent(string nextStage);
    public abstract void NextWorldEvent();
}
