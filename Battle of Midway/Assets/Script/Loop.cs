using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loop : MonoBehaviour
{
    [SerializeField] List<Transform> objs_To_Loop;
    [SerializeField] float obj_size;
    [SerializeField] float end_Pos;
    [SerializeField] Vector2 velocity;

    void Update()
    {
        Try_To_Loop();
        Move_Objs();
    }

    void Try_To_Loop()
    {
        if(objs_To_Loop[0].position.y < end_Pos)
        {
            Transform obj = objs_To_Loop[0];
            objs_To_Loop.Remove(obj);
            obj.position = objs_To_Loop[objs_To_Loop.Count - 1].position + new Vector3(0, obj_size, 0);
            objs_To_Loop.Add(obj);
        }
    }

    void Move_Objs()
    {
        foreach (Transform obj in objs_To_Loop)
            obj.transform.Translate(velocity * Time.deltaTime);
    }

}
