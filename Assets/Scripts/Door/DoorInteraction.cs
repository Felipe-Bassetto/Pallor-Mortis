using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    [Header("Porta")]
    public bool trancada;
    public Transform pivot;
    public float velocidadeRotacao = 120f;
    public bool aberta;

    [Header("Rotação")]
    public Vector3 rotacaoAbertaOffset = new Vector3(0, 90, 0);
    private bool canRotate = false;

    private Quaternion rotacaoFechada;
    private Quaternion rotacaoAberta;

    [Header("GameObject")]
    public DoorAction doorAct;

    void Start()
    {
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

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == pivot || hit.transform.IsChildOf(pivot))
                {
                    if (trancada)
                    {
                        Debug.Log("Trancada");
                        return;
                    }
                    canRotate = true;
                    aberta = !aberta;
                }
            }
        }

        // ANIMAÇÃO
        Quaternion alvo = aberta ? rotacaoAberta : rotacaoFechada;
        //if(doorAct == null)
        //{
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
        canRotate = true;
        aberta = close;
    }
}