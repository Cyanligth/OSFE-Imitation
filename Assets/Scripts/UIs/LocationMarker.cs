using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class LocationMarker : MonoBehaviour
{
    Animator animator;
    private int stage;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        stage = 0;
    }
    public void SetStage(int stage)
    {
        this.stage = stage;
        animator.SetInteger("Stage", stage);
    }
    public void Move(Vector3 vector3)
    {
        StartCoroutine(MoveRoutine(vector3));
        animator.SetTrigger("Move");
    }
    IEnumerator MoveRoutine(Vector3 vector3)
    {
        float time = 0;
        Vector3 v3 = transform.position;
        Quaternion q = transform.rotation;
        while (time < 1)
        {
            // transform.rotation = Quaternion.Lerp(q, Quaternion.Euler(0,0, q.z+90), time);
            transform.position = new Vector3(Mathf.Lerp(v3.x, vector3.x, time), Mathf.Lerp(v3.y, vector3.y, time));
            time += Time.deltaTime * 4;
            yield return null;
        }
    }
}
