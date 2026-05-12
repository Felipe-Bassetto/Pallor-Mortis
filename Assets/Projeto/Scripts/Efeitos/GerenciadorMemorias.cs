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
        // Garante que o volume e o canvas comecem desativados [3], [2]
        if (volumeMemorias != null) 
        {
            volumeMemorias.weight = 0;

            // Tenta obter o componente de Vignette do Profile do Volume [4], [5]
            if (volumeMemorias.profile.TryGet(out vignetteComponent))
            {
                vignetteComponent.intensity.value = 0f;
            }
        }

        if (canvasMemorias != null) canvasMemorias.SetActive(false);
    }

    void Update()
    {
        // Proteção: Se não houver volume ou vignette, não executa a lógica [6]
        if (volumeMemorias == null || vignetteComponent == null) return;

        // 1. Controla o peso (weight) do Volume global
        float pesoAlvo = memoriasAtivas ? 1f : 0f;
        volumeMemorias.weight = Mathf.MoveTowards(volumeMemorias.weight, pesoAlvo, velocidadeTransicao * Time.deltaTime);

        // 2. Lógica de transição específica das Memórias
        if (memoriasAtivas)
        {
            // O Vignette caminha suavemente até a intensidade 1.0 (tela cheia) [5]
            vignetteComponent.intensity.value = Mathf.Lerp(vignetteComponent.intensity.value, 1.0f, velocidadeVignette * Time.deltaTime);

            // 3. Ativa o Canvas quando a tela estiver totalmente coberta
            // Usamos 0.99f pois valores float raramente chegam a 1.0 exato imediatamente
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
            // Retorna o vignette para 0 e desativa o canvas ao desligar o efeito
            vignetteComponent.intensity.value = Mathf.Lerp(vignetteComponent.intensity.value, 0f, velocidadeVignette * Time.deltaTime);
            if (canvasMemorias != null && canvasMemorias.activeSelf) canvasMemorias.SetActive(false);
        }
    }

    [ContextMenu("Ativar Memorias")]
    public void AtivarMemorias()
    {
        memoriasAtivas = true;
        // Começa a intensidade em 0.6 conforme seu pedido
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