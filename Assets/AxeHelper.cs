using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeHelper : MonoBehaviour
{
    AxeGather _axeGather;

    private void Awake()
    {
        _axeGather = GetComponentInParent<AxeGather>();
    }

    public void CallCheckHit()
    {
        if (_axeGather != null) _axeGather.CheckHit();
    }
}
