using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GerenciadorMemorias : MonoBehaviour
{
    [Header("Referências de Volume")]
    public Volume volumeMemorias; 
    private Vignette vignetteComponent;

    [Header("Referências de UI")]
    public GameObject canvasMemorias; // Arraste seu Canvas aqui

    [Header("Configurações da Transição")]
    public float velocidadeTransicao = 0.75f;
    public float velocidadeVignette = 1.0f; // Velocidade da "cobertura" da tela
    
    [Header("Status")]
    public bool memoriasAtivas = false;

    void Start()
    {
        if (volumeMemorias != null) 
        {
            volumeMemorias.weight = 0;

            if (volumeMemorias.profile.TryGet(out vignetteComponent))
            {
                vignetteComponent.intensity.value = 0f;
            }
        }

        if (canvasMemorias != null) canvasMemorias.SetActive(false);
    }

    void Update()
    {
        if (volumeMemorias == null || vignetteComponent == null) return;

        float pesoAlvo = memoriasAtivas ? 1f : 0f;
        volumeMemorias.weight = Mathf.MoveTowards(volumeMemorias.weight, pesoAlvo, velocidadeTransicao * Time.deltaTime);

        if (memoriasAtivas)
        {
            vignetteComponent.intensity.value = Mathf.Lerp(vignetteComponent.intensity.value, 1.0f, velocidadeVignette * Time.deltaTime);

            if (vignetteComponent.intensity.value >= 0.99f)
            {
                if (canvasMemorias != null && !canvasMemorias.activeSelf)
                {
                    canvasMemorias.SetActive(true);
                }
            }
        }
        else
        {
            vignetteComponent.intensity.value = Mathf.Lerp(vignetteComponent.intensity.value, 0f, velocidadeVignette * Time.deltaTime);
            if (canvasMemorias != null && canvasMemorias.activeSelf) canvasMemorias.SetActive(false);
        }
    }

    [ContextMenu("Ativar Memorias")]
    public void AtivarMemorias()
    {
        memoriasAtivas = true;
        if (vignetteComponent != null)
        {
            vignetteComponent.intensity.value = 0.6f;
        }
    }

    [ContextMenu("Desativar Memorias")]
    public void DesativarMemorias()
    {
        memoriasAtivas = false;
    }
}