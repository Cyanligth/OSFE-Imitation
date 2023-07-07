using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extension
{
    public static bool IsContain(this LayerMask mask, int layer)
    {
        return ((1 << layer) & mask) != 0;
    }
}
