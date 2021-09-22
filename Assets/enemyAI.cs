using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAI : MonoBehaviour
{
    public Transform[] wall;
    public int speed;

    private int wallIndex;
    private float dist;
    // Start is called before the first frame update
    void Start()
    {
        wallIndex = 0;
        transform.LookAt(wall[wallIndex].position);
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(transform.position, wall[wallIndex].position);
        if(dist < 1f)
        {
            IncreaseIndex();
        }
        Patrol();
        
        
    }

    void Patrol()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void IncreaseIndex()
    {
        wallIndex++;
        if(wallIndex >= wall.Length)
        {
            wallIndex = 0;
        }
        transform.LookAt(wall[wallIndex].position);
    }
  
}
