using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : MonoBehaviour
{

    [Header("Camera")]
    private Camera cameraPrincipal;

    [Header("Porta")]
    public bool trancada;
    public GameObject doorPivotDir;
    public GameObject doorPivotEsq;
    public bool isDouble;
    public float velocidadeRotacao = 1f;
    
    private Quaternion rotacaoPortaDirAberta = Quaternion.Euler(0, 90, 0);
    private Quaternion rotacaoPortaEsqAberta = Quaternion.Euler(0, -90, 0);
    private Quaternion rotacaoPortaFechada = Quaternion.Euler(0, 0, 0);
    LayerMask layerMask;
    private bool aberta = false;
    private string pivotName;

    // Start is called before the first frame update
    void Start()
    {
        cameraPrincipal = Camera.main;
        layerMask = LayerMask.GetMask("Door");
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;    
        if (Input.GetMouseButtonUp(0))
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                pivotName = hit.collider.gameObject.name;
                if(trancada)
                {
                    Debug.Log("Trancada, fala da personagem, pop up");
                }
                else
                {
                    Debug.Log("Abre");
                    if(aberta)aberta = false;
                    else aberta = true;
                }
            }            
        }
        if(aberta & (pivotName == gameObject.name))
        {
            doorPivotDir.transform.rotation = Quaternion.RotateTowards(doorPivotDir.transform.rotation,rotacaoPortaDirAberta,velocidadeRotacao * Time.unscaledDeltaTime);
            if(isDouble)
            {
                doorPivotEsq.transform.rotation = Quaternion.RotateTowards(doorPivotEsq.transform.rotation,rotacaoPortaEsqAberta,velocidadeRotacao * Time.unscaledDeltaTime);
            }
        }
        else if(!aberta & (pivotName == gameObject.name))
        {
            doorPivotDir.transform.rotation = Quaternion.RotateTowards(doorPivotDir.transform.rotation,rotacaoPortaFechada,velocidadeRotacao * Time.unscaledDeltaTime);
            if(isDouble)
            {
                doorPivotEsq.transform.rotation = Quaternion.RotateTowards(doorPivotEsq.transform.rotation,rotacaoPortaFechada,velocidadeRotacao * Time.unscaledDeltaTime);
            }
        }
        
    }
}
