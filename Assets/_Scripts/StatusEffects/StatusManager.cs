using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour
{

    public static StatusManager Instance;
    private void Awake()
    {
        if(Instance != null) Destroy(Instance);
        else Instance = this;
    }

    [SerializeField] public List<StatusEffect> Status;
}
   
