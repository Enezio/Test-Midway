using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Int_Test : MonoBehaviour
{
    public Sprite[] numbers;
    public int score = 350;

    void Start()
    {
        char[] numbers = score.ToString().ToCharArray();

        foreach (char n in numbers)
            Debug.Log(n);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void From_Score_To_Number()
    {

    }
}
