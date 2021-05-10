using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Default_Bullet : Bullet_Base
{
    private void Update()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Health_System other_Health = other.GetComponent<Health_System>();
        if(other_Health != null)
            On_Hit(other_Health);
    }

    public override void Move()
    {
        transform.Translate(dir_To_Move * mov_Speed * Time.deltaTime);
    }
}