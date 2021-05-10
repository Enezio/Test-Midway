using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simple_Ship : Enemy_Base
{
    [Header("Moviment Settings")]
    public float speed = 1f;
    public float end_Moviment_Y;        // If ship position Y get smaller than this value, the ship will be disabled

    #region Mono

    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        Moviment();

        if (transform.position.y < end_Moviment_Y)
            Death();
    }

    #endregion

    #region Overrides

    public override void Initialize(Transform target)
    {
        this.target = target;
        gameObject.SetActive(true);

        for(int i = 0; i < transform.childCount; i++)
        {
            Enemy_Base turret = transform.GetChild(i).GetComponent<Enemy_Base>();

            if(turret != null)
                turret.Initialize(target);
        }
    }

    protected override void Moviment()
    {
        // Move down
        transform.Translate(-transform.up * speed * Time.deltaTime);
    }

    protected override void Death()
    {
        Global_Event.Enemy_Death(score_Points);
        PoolManager.ReleaseObject(this.gameObject);
    }

    #endregion
}