using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    [Header("Porta")]
    public bool trancada;
    public Transform pivot;
    public float velocidadeRotacaoPadrao = 60f;
    public float velocidadeRotacaoEvent = 200f;
    public bool aberta;
    public bool needKey;
    
    private float distanceClick = 2f;
    private float velocidadeRotacao;

    [Header("Rotação")]
    public Vector3 rotacaoAbertaOffset = new Vector3(0, 90, 0);
    private bool canRotate = false;

    private Quaternion rotacaoFechada;
    private Quaternion rotacaoAberta;

    [Header("GameObject")]
    public DoorAction doorAct;

    private ItensController ic;

    void Start()
    {
        ic = FindObjectOfType<ItensController>();

        rotacaoFechada = pivot.rotation;
        rotacaoAberta = rotacaoFechada * Quaternion.Euler(rotacaoAbertaOffset);
    }

    void Update()
    {
        // INTERAÇÃO
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, distanceClick))
            {
                if (hit.transform == pivot || hit.transform.IsChildOf(pivot))
                {
                    if (trancada)
                    {
                        bool key;
                        if(ic.itemActive != -1) key = ic.arrItens[ic.itemActive].tag == "Chave";
                        else key = false;
                        if((needKey && !key) || !needKey)
                        {
                            Debug.Log("Trancada");
                            return;
                        }  
                    }
                    velocidadeRotacao = velocidadeRotacaoPadrao;
                    canRotate = true;
                    aberta = !aberta;
                }
            }
        }

        // ANIMAÇÃO
        Quaternion alvo = aberta ? rotacaoAberta : rotacaoFechada;

        if (canRotate)
        {
            Debug.Log("rotacionando");
            pivot.rotation = Quaternion.RotateTowards(pivot.rotation, alvo, velocidadeRotacao * Time.deltaTime);
            if(pivot.rotation == alvo)
            {
                canRotate = false;
            }
        }
    }

    public void AltState(bool tranc)
    {
        trancada = tranc;
    }

    public void RotateDoor(bool close)
    {
        velocidadeRotacao = velocidadeRotacaoEvent;
        canRotate = true;
        aberta = close;
    }
}