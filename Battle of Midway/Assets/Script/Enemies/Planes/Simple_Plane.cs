using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health_System))]
public class Simple_Plane : Enemy_Base
{
    [Header("Moviment Settings")]
    public float speed = 1f;
    [SerializeField] float chase_Y;             // If plane Y get higher than this value, it starts to chase again
    [SerializeField] float follow_Target_Y;     // If plane Y get smaller than this value, it starts to follow the player
    [SerializeField] float fly_Away_Y;          // If plane Y get smaller than this value, it starts to fly away
    bool chase = true;                          // Is the plane chasing or flying away?
    bool follow_Target = false;                 // Is the plane following the player x moviment or not?

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

        chase = true;
        follow_Target = false;

        base.Initialize(target);
    }

    protected override void Moviment()
    {
        if(chase)
            Chase();
        else
            Fly_Away();
    }

    #endregion

    void Chase()
    {
        // Move down
        transform.Translate(-transform.up * speed * Time.deltaTime);

        // Check if can chase
        if (!follow_Target && transform.position.y < follow_Target_Y)
            follow_Target = true;
        // Check if should fly away
        if (follow_Target && transform.position.y < fly_Away_Y)
        {
            // Away anim
            chase = false;
            follow_Target = false;
        }

        // Follow player horizontal moviment
        if(follow_Target)
        {
            Vector2 dir_To_Target = target.position - transform.position;
            dir_To_Target.y = 0;
            dir_To_Target.Normalize();

            transform.Translate(dir_To_Target * speed * Time.deltaTime);            
        }
    }

    void Fly_Away()
    {
        // Move down
        transform.Translate(transform.up * speed * Time.deltaTime);

        if (transform.position.y > chase_Y)
        {
            // Chase anim
            chase = true;
        }
    }

    private void Attack()
    {
        if(chase)
            weapon.Shoot(target);
    }
}