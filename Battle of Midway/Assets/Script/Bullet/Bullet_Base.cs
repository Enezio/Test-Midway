using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet_Base : MonoBehaviour
{
    [Header("Bullet_Base")]
    [SerializeField] int damage;
    [SerializeField] protected float mov_Speed;
    protected Vector2 dir_To_Move = new Vector2(0,1);

    public virtual void Initialize_Bullet(Vector2 dir)
    {
        dir_To_Move = dir;
    }

    public abstract void Move();

    public virtual void On_Hit(Health_System other_Health)
    {
        other_Health.Take_Damage(damage, this.transform, Damage_Type.Normal);
        gameObject.SetActive(false);
    }

    private void OnBecameInvisible()
    {
        PoolManager.ReleaseObject(this.gameObject);
    }
}