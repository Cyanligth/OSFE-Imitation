using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private void Start()
    {
        GameManager.Resource.Destroy(gameObject, 2f);
    }
}
