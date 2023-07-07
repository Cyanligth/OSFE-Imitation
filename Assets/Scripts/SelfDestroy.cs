using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    private void Start()
    {
        GameManager.Resource.Destroy(gameObject, 2f);
    }
}
