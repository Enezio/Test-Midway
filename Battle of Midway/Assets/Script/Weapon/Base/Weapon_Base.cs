using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon_Base : MonoBehaviour
{
    [Header("Weapon_Base")]
    [SerializeField] protected GameObject bullet_Prefab;

    protected bool ready = false;
    protected Shoot_Point shoot_Point;

    public virtual void Initialize_Weapon(Shoot_Point shoot_Point)
    {
        if (shoot_Point != null)
        {
            this.shoot_Point = shoot_Point;

            ready = true;
        }
        else
            Debug.LogError("Note: Tried to initialize " + gameObject.name + " but occured an error on Initialization");
    }

    public abstract void Shoot(Transform target);
}