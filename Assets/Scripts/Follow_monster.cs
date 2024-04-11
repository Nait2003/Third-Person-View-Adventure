using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow_monster : MonoBehaviour
{

    public Transform target;
    public float speed;


    void Start()

    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }
}
