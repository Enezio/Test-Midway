using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy_Base : MonoBehaviour
{
    [Header("Enemy_Base")]
    protected Transform target;
    public int score_Points;
    public GameObject death_Effect_Prefab;

    protected virtual void Start()
    {
        // If needed, Force initialization
        if (target == null)
        {
            Initialize(GameObject.FindGameObjectWithTag("Player").transform);
            Debug.LogWarning("Note: " + gameObject.name + " forced initialization");
        }
    }

    public virtual void Initialize(Transform target)
    {
        this.target = target;
        gameObject.SetActive(true);
    }

    protected abstract void Moviment();

    protected virtual void Death()
    {
        Global_Event.Enemy_Death(score_Points);
        PoolManager.SpawnObject(death_Effect_Prefab, transform.position, transform.rotation);
        PoolManager.ReleaseObject(this.gameObject);
    }
}