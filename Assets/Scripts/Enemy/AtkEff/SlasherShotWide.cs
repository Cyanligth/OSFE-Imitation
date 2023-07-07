using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SlasherShotWide : MonoBehaviour
{
    [SerializeField] float speed;
    private void Start()
    {
        StartCoroutine(Move());
    }
    IEnumerator Move()
    {
        while(transform.position.x > GameManager.Data.map[0, 0].x -1)
        {
            transform.position += Vector3.left * Time.deltaTime * speed;
            yield return null;
        }
        GameManager.Resource.Destroy(gameObject);
    }
}
