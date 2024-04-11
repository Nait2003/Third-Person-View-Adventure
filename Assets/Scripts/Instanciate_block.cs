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
    public GameObject Transparent_Cube;
    GameObject clone = null;

    private bool buildMode = false;
    private bool GridToggle = true;

    // Start is called before the first frame update

    Vector3 NewWorldPosition;
    void Update()
    {

        if (Input.GetButtonDown("Build_Button"))
        {
            buildMode = !buildMode;
            if (buildMode == false)
            {
            Destroy (clone);
            clone = null;
            }

        }

        if (Input.GetButtonDown("GridToggleButton"))
        {
            GridToggle = !GridToggle;
        }

        if (buildMode)
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition + new Vector3(0, 50, 0));
            RaycastHit hitInfo;


            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, mask))
            {
                worldPosition = hitInfo.point;

                if (GridToggle)
                {
                    NewWorldPosition = new Vector3(
                        Mathf.Round(worldPosition.x),
                        Mathf.Round(worldPosition.y),
                        Mathf.Round(worldPosition.z));    
                }
                else
                {
                    NewWorldPosition = worldPosition;
                }
                Debug.Log(hitInfo.collider.gameObject);

                if (!clone)
                {
                    clone = Instantiate(Transparent_Cube, NewWorldPosition, Quaternion.identity);
                }
                clone.transform.position = NewWorldPosition;

                Physics.IgnoreCollision(clone.GetComponent<Collider>(), GetComponent<Collider>());
                if (Input.GetButtonDown(fireButton))
                {
                    Instantiate(Cube, NewWorldPosition, Quaternion.identity);
                }

            }
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.blue);

        }
    }
}
