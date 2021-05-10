using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Input_Handler))]
[RequireComponent(typeof(Health_System))]
[RequireComponent(typeof(Animator))]
public class Player_FSM : MonoBehaviour
{
    [Header("Moviment Settings")]
    [Range(0,20)]
    public float mov_Speed;
    Vector3 mov_dir;

    [Header("Shoot Settings")]
    public Weapon_Base current_Weapon;
    [SerializeField] Shoot_Point shoot_Point;

    [Header("Screen Bounderies")]
    [SerializeField] Vector2 hor_Edges;
    [SerializeField] Vector2 ver_Edges;

    [Header("Health Settings")]
    [SerializeField] float time_Damaged = 1f;
    IEnumerator damage_Anim_Coroutine;

    Input_Handler input_Handler;
    Health_System health;
    Animator anim;

    // Extra Events to Handle comunication
    public static event Action<Health_System> Player_On_Enable_Event;
    public static event Action Player_Death_Event;
    public static event Action<int> Player_Take_Damage_Event;
    public static event Action<int> Player_Heal_Event;

    private void Awake()
    {
        input_Handler = GetComponent<Input_Handler>();
        health = GetComponent<Health_System>();
        anim = GetComponent<Animator>();

        health.Death_Event += Player_Death_Event;
        health.Take_Damage_Event += Player_Take_Damage_Event;
        health.Take_Damage_Event += Took_Damage;
        health.Heal_Event += Player_Heal_Event;
    }

    private void OnEnable()
    {
        Player_On_Enable_Event?.Invoke(health);
    }

    void Start()
    {
        if (current_Weapon != null)
            current_Weapon.Initialize_Weapon(shoot_Point);

        health.Take_Damage(1, transform, Damage_Type.Normal);
    }

    void Update()
    {
        mov_dir = input_Handler.Get_Move_Input();

        if (input_Handler.Get_Shoot_input_Held())
            Shoot();
    }

    private void FixedUpdate()
    {
        Move(mov_dir);
    }

    #region Moviment

    private void Move(Vector2 dir)
    {
        // Move player
        transform.Translate(dir * mov_Speed * Time.deltaTime);

        // Keep Player inside screen
        transform.position =
        new Vector3
        (
            Mathf.Clamp(transform.position.x, hor_Edges.x, hor_Edges.y),
            Mathf.Clamp(transform.position.y, ver_Edges.x, ver_Edges.y),
            0
        );

        // Animator
        anim.SetFloat("Vel", dir.x);
    }

    #endregion

    #region Weapon

    void Shoot()
    {
        current_Weapon.Shoot(null);
    }

    public void Collect_Weapon(Weapon_Base new_Weapon)
    {
        current_Weapon = new_Weapon;
        current_Weapon.Initialize_Weapon(shoot_Point);
        current_Weapon.transform.SetParent(transform);
    }

    #endregion

    #region Health

    void Took_Damage(int damage)
    {
        if(damage_Anim_Coroutine != null)
            StopCoroutine(damage_Anim_Coroutine);

        damage_Anim_Coroutine = Handle_Anim_Switch();

        StartCoroutine(damage_Anim_Coroutine);
    }

    IEnumerator Handle_Anim_Switch()
    {
        anim.SetBool("Damaged", true);
        anim.SetBool("Not_Damaged", false);

        yield return new WaitForSeconds(time_Damaged);

        anim.SetBool("Damaged", false);
        anim.SetBool("Not_Damaged", true);
    }

    #endregion

}