using UnityEngine;
using TMPro;

public class InteractionTXT : MonoBehaviour
{
    [Header("Configurações do Objeto")]
    public string textoDesteObjeto = "Pressione MB1 para interagir";

    [Header("Configurações do Player")]
    public TextMeshProUGUI textoHUD;
    public float distanciaLimite = 3f;

    [Header("Layers Interativas")]
    [SerializeField] private LayerMask layersInterativas;

    // Guarda o último objeto clicado
    private InteractionTXT ultimoObjetoInteragido;

    private void Start()
    {
        if (CompareTag("Player") && textoHUD != null)
        {
            textoHUD.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (!CompareTag("Player"))
            return;

        VerificarOlhar();
    }

    private void VerificarOlhar()
    {
        if (Camera.main == null)
            return;

        Ray raio = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        // O Raycast só detecta as layers marcadas no Inspector
        if (Physics.Raycast(raio, out hit, distanciaLimite, layersInterativas))
        {
            InteractionTXT alvo = hit.collider.GetComponentInParent<InteractionTXT>();

            if (alvo != null && textoHUD != null)
            {
                // Se clicou para interagir
                if (Input.GetMouseButtonDown(0))
                {
                    ultimoObjetoInteragido = alvo;
                    textoHUD.gameObject.SetActive(false);

                    // Aqui você pode chamar a lógica específica do objeto
                    return;
                }

                // Se continua olhando para o mesmo objeto que acabou de interagir
                if (alvo == ultimoObjetoInteragido)
                {
                    textoHUD.gameObject.SetActive(false);
                    return;
                }

                // Exibe o texto normalmente
                textoHUD.text = alvo.textoDesteObjeto;
                textoHUD.gameObject.SetActive(true);
                return;
            }
        }

        // Se não estiver olhando para um objeto interativo,
        // libera o bloqueio e esconde o texto
        ultimoObjetoInteragido = null;

        if (textoHUD != null)
        {
            textoHUD.gameObject.SetActive(false);
        }
    }
}