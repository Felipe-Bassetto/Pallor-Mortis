using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    [Header("Porta")]
    public bool trancada;
    public GameObject doorPivotDir;
    public GameObject doorPivotEsq;
    public bool isDouble;
    public float velocidadeRotacao = 1f;

    [Header("Valores de Rotação")]
    public Quaternion rotacaoPortaDirAberta;
    public Quaternion rotacaoPortaEsqAberta;
    public Quaternion rotacaoPortaFechada;

    private bool aberta = false;
    private string pivotName;
    LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        layerMask = LayerMask.GetMask("Door");
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;    
        if (Input.GetMouseButtonUp(0)) // Verificação se o player clicou na porta
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
        if(aberta & (pivotName == gameObject.name)) // Abre a porta
        {
            doorPivotDir.transform.rotation = Quaternion.RotateTowards(doorPivotDir.transform.rotation,rotacaoPortaEsqAberta,velocidadeRotacao * Time.unscaledDeltaTime);
            if(isDouble) // Caso seja porta dupla, abre a segunda junto
            {
                doorPivotEsq.transform.rotation = Quaternion.RotateTowards(doorPivotEsq.transform.rotation,rotacaoPortaDirAberta,velocidadeRotacao * Time.unscaledDeltaTime);
            }
        }
        else if(!aberta & (pivotName == gameObject.name)) // Fecha a porta
        {
            doorPivotDir.transform.rotation = Quaternion.RotateTowards(doorPivotDir.transform.rotation,rotacaoPortaFechada,velocidadeRotacao * Time.unscaledDeltaTime);
            if(isDouble) // Caso seja porta dupla, fecha junto
            {
                doorPivotEsq.transform.rotation = Quaternion.RotateTowards(doorPivotEsq.transform.rotation,rotacaoPortaFechada,velocidadeRotacao * Time.unscaledDeltaTime);
            }
        }
        
    }
}
