using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objs_To_Pool : MonoBehaviour
{
    [SerializeField] Obj_To_Pool[] objs_To_Pool;

    void Start()
    {
        foreach(Obj_To_Pool obj in objs_To_Pool)
            PoolManager.WarmPool(obj.prefab_To_Pool, obj.amount_To_Pool);
    }
}

[System.Serializable]
public class Obj_To_Pool
{
    [SerializeField] string obj_Name;
    public GameObject prefab_To_Pool;
    public int amount_To_Pool;
}
