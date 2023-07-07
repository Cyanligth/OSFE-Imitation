using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiagBeamCharge : MonoBehaviour
{
    public int x;
    public int y;
    public void End()
    {
        DiagBeamPurple diag = GameManager.Resource.Instantiate<DiagBeamPurple>("Effect/Enemy/DiagBeamPurple", transform.position, transform.rotation);
        diag.x = x;
        diag.y = y;
        diag.Beam();
        GameManager.Resource.Destroy(gameObject);
    }
}
