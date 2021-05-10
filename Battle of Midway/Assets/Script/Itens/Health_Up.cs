using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Up : MonoBehaviour
{
    [SerializeField] int heal_Amount;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Health_System health = other.GetComponent<Health_System>();

            if (health)
            {
                health.Heal(heal_Amount);
                gameObject.SetActive(false);
            }
        }
    }
}