using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormSword : MonoBehaviour
{
    private int count = 0;
    private int maxCount;
    private void Start()
    {
        maxCount = 8 - GameManager.Data.playerXPos;
        StartCoroutine(StormSwordClone());
    }
    IEnumerator StormSwordClone()
    {
        while (count < maxCount)
        {
            count++;
            yield return new WaitForSeconds(0.1f);
            GameManager.Resource.Instantiate<GameObject>("Effect/Card/StormSwordClone", new Vector3(transform.position.x + (1.6f * count), transform.position.y), transform.rotation);
        }
        yield return null;
        GameManager.Resource.Destroy(gameObject);
    }
}
