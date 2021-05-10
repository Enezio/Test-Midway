using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_01 : Enemy_Base
{
    [Header("Moviment settings")]
    [SerializeField] float speed = .5f;
    [SerializeField] float dist_To_Change_Target = 0.01f;
    [SerializeField] Transform[] path;

    int current_Index = 0;

    [SerializeField] List<Enemy_Base> m_Turrets;

    #region Mono

    void Update()
    {
        Moviment();
    }

    #endregion

    #region Overrides

    public override void Initialize(Transform target)
    {
        this.target = target;
        gameObject.SetActive(true);

        for (int i = 0; i < transform.childCount; i++)
        {
            Enemy_Base turret = transform.GetChild(i).GetComponent<Enemy_Base>();

            if (turret != null)
            {
                turret.Initialize(target);
                m_Turrets.Add(turret);
            }
        }
    }

    protected override void Moviment()
    {
        if(Vector2.Distance(transform.position, path[current_Index].position) < dist_To_Change_Target)
        {
            current_Index++;
            current_Index = current_Index >= path.Length ? 0 : current_Index;
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, path[current_Index].position, speed * Time.deltaTime);
        }
    }

    #endregion

    public void Loose_Turret(Enemy_Base turret)
    {
        m_Turrets.Remove(turret);

        if(m_Turrets.Count < 1)
        {
            Debug.Log("EndGame");
            Global_Event.End_Game();
        }

    }

}
