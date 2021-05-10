using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_Handler : MonoBehaviour
{
    [SerializeField] bool read_input = true;
    [SerializeField] KeyCode[] shoot_Key;
    [SerializeField] KeyCode close_Game_Key;

    public bool Can_Process_Input()
    {
        return read_input;
    }

    public Vector2 Get_Move_Input()
    {
        if(Can_Process_Input())
        {
            Vector2 move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            move = Vector2.ClampMagnitude(move, 1);
            return move;
        }

        return Vector2.zero;
    }

    public bool Get_Shoot_input_Held()
    {
        if(Can_Process_Input())
        {
            foreach (KeyCode key in shoot_Key)
            {
                if (Input.GetKey(key))
                    return true;
            }

            return false;
        }

        return false;
    }

    public bool Get_Close_Game_Key()
    {
        if (Input.GetKeyDown(close_Game_Key))
            return true;

        return false;
    }

}