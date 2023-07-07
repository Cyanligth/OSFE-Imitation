using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearSlash : MonoBehaviour
{
    private void Start()
    {
        GameManager.Destroy(gameObject, 2f);
    }
}
