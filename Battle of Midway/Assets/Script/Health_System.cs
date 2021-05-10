using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class Health_System : MonoBehaviour
{
    public int max_Health;
    [SerializeField] int current_Health;
    public int Current_Health
    {
        get { return current_Health; }
    }

    public float normal_Damage_Mult = 1f;
    public float explosion_Damage_Mult = 1f;

    public bool is_Invincible;
    [SerializeField] float invincibility_Time;
    [HideInInspector] public string[] damage_Tags;

    public UnityEvent death_Effect;

    // Events
    public event Action Death_Event;
    public event Action<int> Take_Damage_Event;
    public event Action<int> Heal_Event;


    private void Awake()
    {
        if (current_Health <= 0)
            current_Health = max_Health;
    }

    public void Take_Damage(int damage, Transform hostil_Obj, Damage_Type damage_Type)
    {
        if (is_Invincible || current_Health <= 0)
            return;

        if (Is_Damage_Tag(hostil_Obj.tag) == false)
            return;

        if (damage_Type == Damage_Type.Normal)
            damage = Mathf.RoundToInt(damage * normal_Damage_Mult);
        else if (damage_Type == Damage_Type.Explosion)
            damage = Mathf.RoundToInt(damage * explosion_Damage_Mult);


        current_Health -= Mathf.Abs(damage);
        current_Health = Mathf.Clamp(current_Health, 0, max_Health);

        Take_Damage_Event?.Invoke(Mathf.Abs(damage));

        if (Check_Death() == false)
            StartCoroutine(Become_Invincible());
    }

    public void Heal(int heal)
    {
        current_Health += Mathf.Abs(heal);
        current_Health = Mathf.Clamp(current_Health, 0, max_Health);

        Heal_Event?.Invoke(Mathf.Abs(heal));
    }

    bool Check_Death()
    {
        if (current_Health <= 0)
        {
            Death_Event?.Invoke();
            death_Effect.Invoke();
            return true;
        }
        else
            return false;
    }

    bool Is_Damage_Tag(string tag_Name)
    {
        if (damage_Tags.Length < 1)
            return true;

        foreach (string tag in damage_Tags)
        {
            if (tag == tag_Name)
                return true;
        }

        return false;
    }

    public IEnumerator Become_Invincible()
    {
        is_Invincible = true;

        yield return new WaitForSeconds(invincibility_Time);

        is_Invincible = false;
    }

}

public enum Damage_Type
{
    Normal,
    Explosion
}
