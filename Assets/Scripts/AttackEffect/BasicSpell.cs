using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSpell : MonoBehaviour
{
    [SerializeField] float t;
    [SerializeField] float speed;
    Vector3 curPos;
    private void Start()
    {
        StartCoroutine(Move());
    }
    IEnumerator Move()
    {
        curPos = transform.position;
        float time = 0;
        while (time < t) 
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        yield return null;
        GameManager.Resource.Destroy(gameObject);
    }
}
