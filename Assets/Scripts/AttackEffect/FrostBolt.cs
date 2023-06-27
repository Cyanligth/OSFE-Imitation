using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostBolt : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Move());
    }
    IEnumerator Move()
    {
        float time = 0;
        while(time < 1.5f)
        {
            transform.position += Vector3.right * 15 * Time.deltaTime;
            time += Time.deltaTime;
            yield return null;
        }
        yield return null;
        GameManager.Resource.Destroy(gameObject);
    }
}
