using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabItem : MonoBehaviour
{
    [Header("Camera")]
    private Camera cameraPrincipal;

    [Header("Item")]
    LayerMask layerMask;

    [Header("Scripts")]
    private ItensController it;

    // Start is called before the first frame update
    void Start()
    {
        cameraPrincipal = Camera.main;
        layerMask = LayerMask.GetMask("Item");
        it = FindFirstObjectByType<ItensController>();
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
                int maoAdicionada = it.addItem(gameObject);
                if(maoAdicionada != 0)
                {
                    gameObject.transform.SetParent(it.gameObject.transform);
                    gameObject.transform.position = it.gameObject.transform.Find("LeftHand").transform.position;
                }
                
            } 
        }
    }
}
