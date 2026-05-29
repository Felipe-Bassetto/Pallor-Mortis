using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class GrabItem : MonoBehaviour
{
    [Header("Camera")]
    private Camera cameraPrincipal;

    [Header("Item")]
    [SerializeField] private GameObject itemPivot;

    LayerMask layerMask;
    

    [Header("Scripts")]
    private ItensController it;
    [SerializeField] private ItemEvents ie;
    [SerializeField] private GerenciadorConfusao gc;
    [SerializeField] private Crafting craft;

    [Header("Sounds")]
    public SoundManager sm;

    [Header("Variables")]
    private bool triggerActived;

    [Header("Prefab")]
    public GameObject prefabDrop;

    // Start is called before the first frame update
    void Start()
    {
        cameraPrincipal = Camera.main;
        layerMask = LayerMask.GetMask("Item");
        it = FindFirstObjectByType<ItensController>();

        sm = FindObjectOfType<SoundManager>();

        craft = FindObjectOfType<Crafting>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = cameraPrincipal.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit; 
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask) && gameObject.name == hit.collider.gameObject.name)
            {
                int maoAdicionada = it.addItem(itemPivot);
                if(maoAdicionada != -1) // Verifica se foi adicionado e coloca na mão caso sim
                {
                    itemPivot.transform.SetParent(it.gameObject.transform);
                    itemPivot.transform.position = it.gameObject.transform.Find("Hand").transform.position;

                    var variables = Variables.Object(gameObject);

                    if (variables.IsDefined("triggerActived"))
                    {
                        triggerActived = variables.Get<bool>("triggerActived");
                    }
                    else triggerActived = true;

                    if (gameObject.tag == "Chave")
                    {
                        sm.PlaySound(0);

                        if (!triggerActived)
                        {
                            ie.GrabKey();
                            sm.PlaySound(3);
                        }
                    }
                    else if (gameObject.tag == "Nota")
                    {
                        sm.PlaySound(1);
                    }

                    if(craft != null) craft.RemoveItem(itemPivot);
                }
                
            } 
        }
    }
}
