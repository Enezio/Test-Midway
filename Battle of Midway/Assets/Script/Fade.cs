using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    // FAde trocando diretamente o alpha, que nem no game lá
    [SerializeField] SpriteRenderer sr_To_fade;
    [SerializeField] float final_Alpha;
    [SerializeField] float fade_Duration;

}