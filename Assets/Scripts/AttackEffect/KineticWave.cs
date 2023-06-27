using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KineticWave : MonoBehaviour
{
    private int count = 0;
    private void Start()
    {
        StartCoroutine(KineticWaveClone());
    }
    IEnumerator KineticWaveClone()
    {
        while(count < 3)
        {
            count++;
            yield return new WaitForSeconds(0.25f);
            GameManager.Resource.Instantiate<GameObject>("Effect/Card/KineticWaveClone", new Vector3(transform.position.x + (1.6f * count), transform.position.y), transform.rotation);
        }
        yield return null;
        GameManager.Resource.Destroy(gameObject);
    }
}
