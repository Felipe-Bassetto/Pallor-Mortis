using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    [Header("Porta")]
    public bool trancada;
    public Transform pivot;
    public float velocidadeRotacao = 120f;

    [Header("Rotação")]
    public Vector3 rotacaoAbertaOffset = new Vector3(0, 90, 0);

    private Quaternion rotacaoFechada;
    private Quaternion rotacaoAberta;

    public bool aberta;

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

                    aberta = !aberta;
                }
            }
        }

        // ANIMAÇÃO
        Quaternion alvo = aberta ? rotacaoAberta : rotacaoFechada;

        pivot.rotation = Quaternion.RotateTowards(
            pivot.rotation,
            alvo,
            velocidadeRotacao * Time.deltaTime
        );
    }

    public void AltState(bool tranc)
    {
        trancada = tranc;
    }
}