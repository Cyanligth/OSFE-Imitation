using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuffleEffect : MonoBehaviour
{
    [SerializeField] float time;
    public Vector3 pos;

    private void Start()
    {
        StartCoroutine(ShuffleEff());
    }

    IEnumerator ShuffleEff()
    {
        Vector3 targetPos = pos;
        Vector3 startPos = transform.position;
        Vector3 upPos = startPos + new Vector3(0, 1, 0);
        float curTime = 0;
        float xSpeed = 0;
        float ySpeed = 0;
        while (curTime < time)
        {
            xSpeed = Mathf.Lerp(startPos.x, targetPos.x, curTime);
            ySpeed = Mathf.Lerp(Mathf.Lerp(startPos.y, upPos.y, curTime), Mathf.Lerp(upPos.y, targetPos.y, curTime), curTime);
            transform.position = new Vector3(xSpeed, ySpeed);
            curTime += Time.deltaTime * 2.5f;
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        GameManager.Resource.Destroy(gameObject);
    }
}
