using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomStageGenerator : MonoBehaviour, IStageEventListener
{
    [SerializeField]RandomMapGenerator mapGenerator;
    EventMaster master;
    private List<BaseEnemy> enemyList = new List<BaseEnemy>();
    private List<BaseNpc> npcList = new List<BaseNpc>();
    private List<Obstacle> obstaclesList = new List<Obstacle>();
    // 위의 리스트들은 다음 스테이지로 넘어갈때 하나라도 남아있으면 안됨
    private void Awake()
    {
        master = GameManager.Resource.Load<EventMaster>("Data/EventMaster");
    }
    private void OnEnable()
    {
        master.AddStageEventListener(this);
    }
    private void OnDisable()
    {
        master.RemoveStageEventListener(this);
    }
    private void BattleNode()
    {
        int enemy = Random.Range(1, 4);
        int envi = Random.Range(0, 3);
        for(int i = 0; i < enemy; i++)
        {
            bool check;
            BaseEnemy be;
            switch (Random.Range(0,4))
            {
                case 0:
                    be = GameManager.Resource.Instantiate<BaseEnemy>("Prefabs/Enemy/Slasher");
                    do
                    {
                        check = be.SetStartPosition(Random.Range(6, 8), Random.Range(0, 4));
                    } while(!check);
                    be.SetStartPosition(Random.Range(6, 8), Random.Range(0, 4));
                    enemyList.Add(be);
                    break;
                case 1:
                    be = GameManager.Resource.Instantiate<BaseEnemy>("Prefabs/Enemy/Spearper");
                    do
                    {
                        check = be.SetStartPosition(Random.Range(4, 8), Random.Range(0, 4));
                    } while (!check);
                    enemyList.Add(be);
                    break;
                case 2:
                    be = GameManager.Resource.Instantiate<BaseEnemy>("Prefabs/Enemy/TurretLaser");
                    do
                    {
                        check = be.SetStartPosition(Random.Range(5, 7), Random.Range(0, 4));
                    } while (!check);
                    enemyList.Add(be);
                    break;
                case 3:
                    be = GameManager.Resource.Instantiate<BaseEnemy>("Prefabs/Enemy/TurretGun");
                    do
                    {
                        check = be.SetStartPosition(Random.Range(4, 6), Random.Range(0, 4));
                    } while (!check);
                    enemyList.Add(be);
                    break;
            }
        }
        if (envi < 1) return;
        for(int j = 0; j < envi; j++)
        {
            Obstacle ob = GameManager.Resource.Instantiate<Obstacle>("Prefabs/Npc/Obstacle");
            aa:
            ob.SetPosition(Random.Range(0, 8), Random.Range(0, 4));
            foreach(BaseEnemy baseEnemy in enemyList)
            {
                if (Vector2.Distance(ob.transform.position, baseEnemy.transform.position)<0.2f)
                {
                    goto aa;
                }
            }
            obstaclesList.Add(ob);
        }
    }
    private void DistressNode()
    {
        int i = Random.Range(1, 3);
        int p = Random.Range(0, 3);
        BaseNpc npc1;
        BaseNpc npc2;
        BaseEnemy be1;
        BaseEnemy be2;
        switch (p)
        {
            case 0:
                npc1 = GameManager.Resource.Instantiate<BaseNpc>("Prefabs/Npc/Hostage");
                be1 = GameManager.Resource.Instantiate<BaseEnemy>("Prefabs/Enemy/TurretLaser");
                npc1.SetPosition(0, Random.Range(0,2), 100);
                be1.SetStartPosition(Random.Range(5,8),npc1.yPos);
                npcList.Add(npc1);
                enemyList.Add(be1);
                if(i > 1)
                {
                    npc2 = GameManager.Resource.Instantiate<BaseNpc>("Prefabs/Npc/Hostage");
                    be2 = GameManager.Resource.Instantiate<BaseEnemy>("Prefabs/Enemy/TurretLaser");
                    npc2.SetPosition(0, Random.Range(2, 4), 100);
                    be2.SetStartPosition(Random.Range(5, 8), npc2.yPos);
                    npcList.Add(npc2);
                    enemyList.Add(be2);
                }
                break; 
            case 1:
                npc1 = GameManager.Resource.Instantiate<BaseNpc>("Prefabs/Npc/Hostage");
                be1 = GameManager.Resource.Instantiate<BaseEnemy>("Prefabs/Enemy/TurretGun");
                npc1.SetPosition(0, Random.Range(0, 2), 100);
                be1.SetStartPosition(Random.Range(5, 8), npc1.yPos);
                npcList.Add(npc1);
                enemyList.Add(be1);
                if (i > 1)
                {
                    npc2 = GameManager.Resource.Instantiate<BaseNpc>("Prefabs/Npc/Hostage");
                    be2 = GameManager.Resource.Instantiate<BaseEnemy>("Prefabs/Enemy/TurretGun");
                    npc2.SetPosition(0, Random.Range(2, 4), 100);
                    be2.SetStartPosition(Random.Range(5, 8), npc2.yPos);
                    npcList.Add(npc2);
                    enemyList.Add(be2);
                }
                break; 
            case 2:
                npc1 = GameManager.Resource.Instantiate<BaseNpc>("Prefabs/Npc/Hostage");
                npc1.spriteRenderer.flipX = true;
                if (Random.Range(0, 2) > 0)
                    be1 = GameManager.Resource.Instantiate<BaseEnemy>("Prefabs/Enemy/TurretGun");
                else
                    be1 = GameManager.Resource.Instantiate<BaseEnemy>("Prefabs/Enemy/TurretLaser");
                npc1.SetPosition(Random.Range(4,7), Random.Range(0, 2), 100);
                be1.SetStartPosition(npc1.xPos +1, npc1.yPos);
                npcList.Add(npc1);
                enemyList.Add(be1);
                if (i > 1)
                {
                    npc2 = GameManager.Resource.Instantiate<BaseNpc>("Prefabs/Npc/Hostage");
                    npc2.spriteRenderer.flipX = true;
                    if (Random.Range(0, 2) > 0)
                        be2 = GameManager.Resource.Instantiate<BaseEnemy>("Prefabs/Enemy/TurretGun");
                    else
                        be2 = GameManager.Resource.Instantiate<BaseEnemy>("Prefabs/Enemy/TurretLaser");
                    npc2.SetPosition(Random.Range(4, 7), Random.Range(2, 4), 100);
                    be2.SetStartPosition(npc2.xPos +1, npc2.yPos);
                    npcList.Add(npc2);
                    enemyList.Add(be2);
                }
                break;
        }
    }
    private void HazardNode()
    {
        // 복수자나 미사일발사대와 세라(돈)덩어리
    }
    private void CampNode()
    {
        // 캠프파이어와 토끼 소환
    }
    private void ShopNode()
    {
        npcList.Add(GameManager.Resource.Instantiate<BaseNpc>("Prefabs/Npc/Shopkeeper"));
    }
    private void TreasureNode()
    {
        // 보물과 기타등등 적
    }
    private void MinibossNode()
    {
        // 좀 더 강한 적, 일단은 그냥 적들로 채우자
    }
    private void BossNode()
    {
        GameManager.Resource.Instantiate<BaseEnemy>("Prefabs/Enemy/Terra");
        // 원래는 맵에 따라 다른 보스를 랜덤으로 소환해야 하지만 현재 구현된 보스가 테라 하나뿐이기에 무조건 테라를 생성
    }
    public void GameStartEvent()
    {
        ReadyToBattleEvent();
    }

    public void NextStageEvent(string nextStage)
    {
        if (npcList.Count > 0)
        {
            foreach (BaseNpc npc in npcList)
            {
                if (npc != null)
                    GameManager.Resource.Destroy(npc.gameObject);
            }
            npcList.Clear();
        }
        if (obstaclesList.Count > 0)
        {
            foreach (Obstacle obstacle in obstaclesList)
            {
                if (obstacle != null)
                    GameManager.Resource.Destroy(obstacle.gameObject);
            }
            obstaclesList.Clear();
        }
        enemyList.Clear();
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (i < 4)
                    GameManager.Data.boolMap[i, j] = true;
                GameManager.data.isTileUsed[i, j] = false;
            }
        }
    }

    public void NextWorldEvent()
    {
        // 무조건 전투 생성
    }

    public void StageClearEvent()
    {
        
    }

    public void ReadyToBattleEvent()
    {
        switch (mapGenerator.curNode.room)
        {
            case 0:
                BattleNode();
                break;
            case 1:
                DistressNode();
                break;
            case 2:
                HazardNode();
                break;
            case 3:
                CampNode();
                break;
            case 4:
                ShopNode();
                break;
            case 5:
                TreasureNode();
                break;
            case 6:
                MinibossNode();
                break;
            case 8:
                BossNode();
                break;
            default: break; // 7은 카운트라 예외, 9는 넥스트 월드라서 예외

        }
        GameManager.Data.SetStageEnemy(enemyList.Count);
        if (enemyList.Count == 0)
            GameManager.Data.IsClearThisStage = true;
    }
}
