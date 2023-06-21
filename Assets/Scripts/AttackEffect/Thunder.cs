using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : MonoBehaviour
{
    private void Start()
    {
        GameManager.Resource.Destroy(gameObject, 2f);
    }
}
