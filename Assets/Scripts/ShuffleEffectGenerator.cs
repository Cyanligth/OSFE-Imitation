using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuffleEffectGenerator : MonoBehaviour
{
    [SerializeField] int count;
    private Vector3[] effTargetPos;

    private void Awake()
    {
        count = 6; // GameManager.Player.CardList.Count;
    }
    public void Generate()
    {
        effTargetPos = new Vector3[count];
        effTargetPos[0] = new Vector3(-5.8f, -4);
        effTargetPos[1] = new Vector3(-4.6f, -4);
        Vector3 vector3 = new Vector3(-7, -3);
        for (int i = 2; i < count; i++)
        {
            effTargetPos[i] = vector3;
            vector3 += new Vector3(0, 1.05f);
        }
        for (int i = 0; i < count; i++)
        {
            ShuffleEffect shuffleEff = GameManager.Resource.Instantiate<ShuffleEffect>("Particle/ShuffleEffect", transform.position, Quaternion.Euler(-90, 0, 0));
            shuffleEff.pos = effTargetPos[i];
        }
    }
}
