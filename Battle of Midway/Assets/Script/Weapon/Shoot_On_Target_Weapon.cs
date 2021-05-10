using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot_On_Target_Weapon : Weapon_Base
{
    [SerializeField] float delay_Btw_Shots;
    float last_Time_Shot;

    private void OnEnable()
    {
        last_Time_Shot = Time.time;
    }

    public override void Shoot(Transform target)
    {
        if(target != null)
        {
            if (last_Time_Shot + delay_Btw_Shots < Time.time)
            {
                last_Time_Shot = Time.time;

                foreach (Transform point in shoot_Point.main_Shoot_Point)
                {
                    Vector2 shoot_Dir = (target.position - point.position).normalized;
                    Bullet_Base bullet = PoolManager.SpawnObject(bullet_Prefab, point.position, Quaternion.identity).GetComponent<Bullet_Base>();
                    bullet.Initialize_Bullet(shoot_Dir);
                    Debug.DrawRay(point.position, shoot_Dir, Color.red, 1f);
                }
            }
        }
    }
}