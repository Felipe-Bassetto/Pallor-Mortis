using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class GrabItem : MonoBehaviour
{
    [Header("Camera")]
    private Camera cameraPrincipal;

    [Header("Item")]
    LayerMask layerMask;

    [Header("Scripts")]
    private ItensController it;
    [SerializeField] private ItemEvents ie;

    [Header("Sounds")]
    public SoundManager sm;

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
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask) && gameObject.name == hit.collider.gameObject.name)
            {
                int maoAdicionada = it.addItem(gameObject);
                if(maoAdicionada != -1) // Verifica se foi adicionado e coloca na mão caso sim
                {
                    gameObject.transform.SetParent(it.gameObject.transform);
                    gameObject.transform.position = it.gameObject.transform.Find("LeftHand").transform.position;

                    var variables = Variables.Object(gameObject);

                    bool triggerActived = variables.Get<bool>("triggerActived");


                    if(gameObject.tag == "Chave")
                    {
                        sm.PlaySound(0);
                        
                        if(!triggerActived)
                        {
                            ie.GrabKey();                      
                        }
                    }
                    else if(gameObject.tag == "Nota")
                    {
                        sm.PlaySound(1);
                    }
                }
                
            } 
        }
    }
}
