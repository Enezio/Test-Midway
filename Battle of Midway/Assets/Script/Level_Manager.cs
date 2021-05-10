using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Manager : MonoBehaviour
{
    [Header("Level Setings")]
    [SerializeField] float level_Duration;
    [SerializeField] GameObject level_Boss;

    [Header("Spawn Settings")]
    [SerializeField] Transform spawn_Root;
    [SerializeField] Vector2 area_Size;
    Vector2 hor_Spawn_Area;
    Vector2 ver_Spawn_Area;

    [SerializeField] float wait_Time_Start_Spawning;
    [SerializeField] float time_To_Spawn;
    [SerializeField] GameObject[] prefabs_To_Spawn;
    float last_Spawn_Time;

    [Header("Debug")]
    [SerializeField] Color area_Color = Color.red;

    Transform player;

    void Awake()
    {
        last_Spawn_Time = -1000;

        hor_Spawn_Area.x = spawn_Root.position.x - area_Size.x/2;
        hor_Spawn_Area.y = spawn_Root.position.x + area_Size.x/2;
        ver_Spawn_Area.x = spawn_Root.position.y - area_Size.y/2;
        ver_Spawn_Area.y = spawn_Root.position.y + area_Size.y/2;

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        level_Duration -= Time.deltaTime;
        if(level_Duration < 0)
        {
            level_Boss.SetActive(true);
            gameObject.SetActive(false);
            return;
        }

        if (Time.time < wait_Time_Start_Spawning)
            return;

        if(Time.time > last_Spawn_Time + time_To_Spawn)
        {
            Spawn();
        }
    }

    void Spawn()
    {
        last_Spawn_Time = Time.time;

        GameObject obj_To_Spawn = prefabs_To_Spawn[Random.Range(0, prefabs_To_Spawn.Length)];
        float x = Random.Range(hor_Spawn_Area.x, hor_Spawn_Area.y);
        float y = Random.Range(ver_Spawn_Area.x, ver_Spawn_Area.y);

        Enemy_Base enemy = PoolManager.SpawnObject(obj_To_Spawn, new Vector3(x, y, 0), Quaternion.identity).GetComponent<Enemy_Base>();
        enemy.Initialize(player);
    }

    private void OnDrawGizmos()
    {
        if(spawn_Root != null)
        {
            Gizmos.color = area_Color;
            Gizmos.DrawCube(spawn_Root.position, area_Size);
        }
    }

}