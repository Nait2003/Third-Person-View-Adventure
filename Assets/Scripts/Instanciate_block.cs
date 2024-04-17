using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instanciate_block : MonoBehaviour
{
    public float height_offset = 0;
    public string fireButton = "Fire1";

    public Vector3 worldPosition;
    Plane plane = new Plane(Vector3.up, 0);

    public LayerMask Interactable_item;
    public LayerMask mask;
    public Interactable focus;
    public GameObject Cube;
    public GameObject Transparent_Cube;
    GameObject clone = null;

    private bool buildMode = false;
    private bool GridToggle = true;

    // Start is called before the first frame update

    Vector3 NewWorldPosition;
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition + new Vector3(0, 50, 0));
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(Input.GetMouseButtonDown(0));
            RaycastHit hit;

            if (!Physics.Raycast(ray, out hit, 100, Interactable_item))
            {
                RemoveFocus();
            }

        }
        //Input.GetMouseButtonDown(0))
        RaycastHit scan;
        if (Physics.Raycast(ray, out scan, 100, Interactable_item))
        {
            
            Debug.Log(Input.GetMouseButtonDown(0));
           

            if (Input.GetMouseButtonDown(0))
            {
                Interactable interactable = scan.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    SetFocus(interactable);
                }
            
            }
        }
        if (Input.GetButtonDown("Build_Button"))
        {
            Debug.Log(buildMode);
            buildMode = !buildMode;
            if (buildMode == false)
            {
                Destroy(clone);
                clone = null;
            }

        }

        if (Input.GetButtonDown("GridToggleButton"))
        {
            GridToggle = !GridToggle;
        }

        if (buildMode)
        {

            
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

    void SetFocus(Interactable NewFocus)
    {
        focus = NewFocus;
    }

    void RemoveFocus()
    {
        focus = null;
    }

}

