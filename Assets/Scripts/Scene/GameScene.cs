using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override IEnumerator Loading()
    {
        yield return null;
        progress = 1;
    }
}
