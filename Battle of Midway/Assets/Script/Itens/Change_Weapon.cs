using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change_Weapon : MonoBehaviour
{
    [SerializeField] Weapon_Base new_Weapon;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<Player_FSM>().Collect_Weapon(new_Weapon);
            gameObject.SetActive(false);
        }
    }
}