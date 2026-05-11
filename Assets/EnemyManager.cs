using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyManager : MonoBehaviour
{
    [Header("Configurações de Movimento")]
    public float raioPerambulacao = 10f;
    private NavMeshAgent agente;

    [Header("Configuração de Visão (FOV)")]
    public float raioVisao = 10f;
    [Range(0, 360)]
    public float anguloVisao = 45f;
    public LayerMask layerPlayer;      
    public LayerMask layerObstaculos;  

    [Header("Referências")]
    public Transform player;
    private bool consegueVerPlayer;

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        
        // Inicia a verificação de visão 5 vezes por segundo para poupar processamento
        StartCoroutine(RotinaVisao());
    }

    private IEnumerator RotinaVisao()
    {
        WaitForSeconds espera = new WaitForSeconds(0.2f);
        while (true)
        {
            yield return espera;
            VerificarCampoDeVisao();
        }
    }

    private void VerificarCampoDeVisao()
    {
        // Physics.OverlapSphere retorna um ARRAY (lista) de colisores
        Collider[] alvosNoRaio = Physics.OverlapSphere(transform.position, raioVisao, layerPlayer);

        // Verificamos se a lista não está vazia antes de acessar o primeiro item 
        if (alvosNoRaio.Length > 0)
        {
            // AGORA SIM: Pegamos o transform do PRIMEIRO objeto da lista 
            Transform alvo = alvosNoRaio[0].transform; 
            Vector3 direcaoParaAlvo = (alvo.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, direcaoParaAlvo) < anguloVisao / 2)
            {
                float distanciaAteAlvo = Vector3.Distance(transform.position, alvo.position);

                // Raycast para garantir que não há paredes no caminho
                if (!Physics.Raycast(transform.position, direcaoParaAlvo, distanciaAteAlvo, layerObstaculos))
                {
                    consegueVerPlayer = true;
                    return;
                }
            }
        }
        consegueVerPlayer = false;
    }

    void Update()
    {
        // Só executa se o player existir e o inimigo estiver no chão azul (NavMesh)
        if (player == null || !agente.isOnNavMesh) return;

        if (consegueVerPlayer)
        {
            agente.SetDestination(player.position);
        }
        else
        {
            if (!agente.pathPending && agente.remainingDistance <= agente.stoppingDistance)
            {
                agente.SetDestination(ObterPontoAleatorio(transform.position, raioPerambulacao));
            }
        }
    }

    Vector3 ObterPontoAleatorio(Vector3 centro, float raio)
    {
        Vector3 direcaoAleatoria = Random.insideUnitSphere * raio + centro;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(direcaoAleatoria, out hit, raio, 1)) return hit.position;
        return centro;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, raioVisao);

        Vector3 direcaoA = DirecaoPeloAngulo(-anguloVisao / 2);
        Vector3 direcaoB = DirecaoPeloAngulo(anguloVisao / 2);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + direcaoA * raioVisao);
        Gizmos.DrawLine(transform.position, transform.position + direcaoB * raioVisao);

        if (consegueVerPlayer)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, player.position);
        }
    }

    private Vector3 DirecaoPeloAngulo(float anguloEmGraus)
    {
        anguloEmGraus += transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(anguloEmGraus * Mathf.Deg2Rad), 0, Mathf.Cos(anguloEmGraus * Mathf.Deg2Rad));
    }
}