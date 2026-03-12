using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : MonoBehaviour
{

    [Header("Camera")]
    private Camera cameraPrincipal;

    [Header("Porta")]
    public bool trancada;
    public GameObject doorPivot;
    private Quaternion rotacaoPortaAberta = Quaternion.Euler(0, 90, 0);
    private Quaternion rotacaoPortaFechada = Quaternion.Euler(0, 0, 0);
    public float velocidadeRotacao = 1f;
    LayerMask layerMask;
    private bool aberta = false;
    public int doorNum;
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

        if(aberta & (pivotName == gameObject.name))doorPivot.transform.rotation = Quaternion.RotateTowards(doorPivot.transform.rotation,rotacaoPortaAberta,velocidadeRotacao * Time.unscaledDeltaTime);
        else if(!aberta & (pivotName == gameObject.name))doorPivot.transform.rotation = Quaternion.RotateTowards(doorPivot.transform.rotation,rotacaoPortaFechada,velocidadeRotacao * Time.unscaledDeltaTime);
    }
}
