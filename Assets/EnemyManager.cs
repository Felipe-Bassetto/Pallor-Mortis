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
    public Transform player; // Se deixado vazio no Prefab, o script buscará na cena
    private bool consegueVerPlayer;

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();

        // BUSCA AUTOMÁTICA DO PLAYER (Solução para o Prefab)
        if (player == null)
        {
            // Procura na cena o objeto marcado com a etiqueta "Player" [3]
            GameObject jogadorEncontrado = GameObject.FindWithTag("Player");
            if (jogadorEncontrado != null)
            {
                player = jogadorEncontrado.transform;
            }
            else
            {
                Debug.LogError("Inimigo não encontrou o Player! Certifique-se de que o Player tem a Tag 'Player'.");
            }
        }
        
        // Inicia a verificação de visão 5 vezes por segundo para poupar processamento [4]
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
        // Detecta colisores da camada Player no raio de visão [5]
        Collider[] alvosNoRaio = Physics.OverlapSphere(transform.position, raioVisao, layerPlayer);

        if (alvosNoRaio.Length > 0)
        {
            // Pega o transform do primeiro item encontrado na lista [4]
            Transform alvo = alvosNoRaio[0].transform; 
            Vector3 direcaoParaAlvo = (alvo.position - transform.position).normalized;

            // Verifica se o alvo está dentro do cone de visão
            if (Vector3.Angle(transform.forward, direcaoParaAlvo) < anguloVisao / 2)
            {
                float distanciaAteAlvo = Vector3.Distance(transform.position, alvo.position);

                // Raycast para garantir que não existam obstáculos entre o inimigo e o player [5]
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
        // Só executa se o player foi encontrado e o inimigo está no NavMesh [3]
        if (player == null || !agente.isOnNavMesh) return;

        if (consegueVerPlayer)
        {
            // Persegue o jogador
            agente.SetDestination(player.position);
        }
        else
        {
            // Patrulha aleatória se não encontrar o jogador [6]
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
        // Garante que o ponto sorteado esteja dentro da malha de navegação (NavMesh) [5]
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

        if (consegueVerPlayer && player != null)
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