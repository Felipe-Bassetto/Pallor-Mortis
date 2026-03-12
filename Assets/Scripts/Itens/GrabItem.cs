using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabItem : MonoBehaviour
{
    [Header("Camera")]
    private Camera cameraPrincipal;

    [Header("Item")]
    LayerMask layerMask;
    // Start is called before the first frame update
    void Start()
    {
        cameraPrincipal = Camera.main;
        layerMask = LayerMask.GetMask("Item");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit; 

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {

            } 
        }
    }
}
