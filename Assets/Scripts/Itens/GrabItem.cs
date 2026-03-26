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

    [Header("GameObjects")]
    public GameObject trigger;

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
            Ray ray = cameraPrincipal.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit; 

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                int maoAdicionada = it.addItem(gameObject);
                if(maoAdicionada != -1) // Verifica se foi adicionado e coloca na mão caso sim
                {
                    gameObject.transform.SetParent(it.gameObject.transform);
                    gameObject.transform.position = it.gameObject.transform.Find("LeftHand").transform.position;

                    if(gameObject.tag == "Chave")
                    {
                        trigger.SetActive(true);
                    }
                }
                
            } 
        }
    }
}
