using UnityEngine;
using TMPro;

public class InteractionTXT : MonoBehaviour
{
    [Header("Configurações do Objeto")]
    public string textoDesteObjeto = "Interagir"; 

    [Header("Configurações do Player (Apenas se for o Player)")]
    public TextMeshProUGUI textoHUD; 
    public float distanciaLimite = 3f;

    // Variável para controlar se o player está "dentro" de uma interação
    private bool estaInteragindo = false;

    private void Start()
    {
        if (CompareTag("Player") && textoHUD != null)
        {
            textoHUD.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (!CompareTag("Player")) return;

        // 1. Verifica se o jogador quer SAIR da interação
        if (estaInteragindo)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                estaInteragindo = false;
                // O texto permanecerá desligado até o próximo frame de detecção
            }
            
            // Enquanto estiver interagindo, garantimos que o texto esteja desligado
            if (textoHUD != null) textoHUD.gameObject.SetActive(false);
            return; 
        }

        // 2. Se não estiver interagindo, executa a detecção normal
        VerificarOlhar();
    }

    private void VerificarOlhar()
    {
        Ray raio = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(raio, out hit, distanciaLimite))
        {
            InteractionTXT alvo = hit.collider.GetComponent<InteractionTXT>();

            if (alvo != null && textoHUD != null)
            {
                // 3. Verifica se o jogador quer ENTRAR na interação
                if (Input.GetMouseButtonDown(0))
                {
                    estaInteragindo = true;
                    textoHUD.gameObject.SetActive(false);
                    return;
                }

                textoHUD.text = alvo.textoDesteObjeto;
                textoHUD.gameObject.SetActive(true);
                return;
            }
        }

        if (textoHUD != null && textoHUD.gameObject.activeSelf)
        {
            textoHUD.gameObject.SetActive(false);
        }
    }
}