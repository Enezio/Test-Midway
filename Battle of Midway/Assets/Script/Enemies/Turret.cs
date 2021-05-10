using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health_System))]
public class Turret : Enemy_Base
{
    [Header("Shoot Settings")]
    [SerializeField] Shoot_Point shoot_Point;
    [SerializeField] Weapon_Base weapon;

    Health_System health;

    #region Mono

    protected override void Start()
    {
        base.Start();

        health = GetComponent<Health_System>();
        health.Death_Event += Death;
        weapon.Initialize_Weapon(shoot_Point);
    }

    private void Update()
    {
        Moviment();
        Attack();
    }

    #endregion

    #region Overrides

    public override void Initialize(Transform target)
    {
        if(health != null)
            health.Heal(health.max_Health);

        base.Initialize(target);
    }

    protected override void Moviment()
    {
        transform.up = (target.position - transform.position).normalized;
    }

    protected override void Death()
    {
        Global_Event.Enemy_Death(score_Points);
        GameObject death_Effect_Obj = PoolManager.SpawnObject(death_Effect_Prefab, transform.position, transform.rotation);
        death_Effect_Obj.transform.SetParent(transform.parent);
        gameObject.SetActive(false);
    }

    #endregion

    private void Attack()
    {
        weapon.Shoot(target);
    }
}