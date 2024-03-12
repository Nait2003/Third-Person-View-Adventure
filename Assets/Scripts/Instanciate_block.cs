using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instanciate_block : MonoBehaviour
{
    public float height_offset = 0;
    public string fireButton = "Fire1";

    public Vector3 worldPosition;
    Plane plane = new Plane(Vector3.up, 0);

    public LayerMask mask;

    public GameObject Cube;
    // Start is called before the first frame update
    void Update()
    {
        
    if (Input.GetButtonDown(fireButton))
    {

        float distance;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, mask))
        {
            worldPosition = hitInfo.point;
            Debug.Log(worldPosition);
            GameObject Clone = Instantiate(Cube, worldPosition, Quaternion.identity);

            Physics.IgnoreCollision(Cube.GetComponent<Collider>(), GetComponent<Collider>());
        }
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.blue);
  
    }


    }


}