using UnityEngine;
using UnityEngine.Rendering;

public class GerenciadorConfusao : MonoBehaviour
{
    [Header("Referências de Volume")]
    public Volume volumeTontura; 

    [Header("Configurações da Transição")]
    public float velocidadeTransicao = 0.5f;
    
    [Header("Configurações da Tontura")]
    public bool usarPulsação = true;
    public float velocidadePulso = 1f;
    public float intensidadeMinimaNoPulso = 0.7f;

    [Header("Teste (Aperte T no jogo)")]
    public bool efeitoAtivo = false;

    void Start()
    {
        if (volumeTontura != null) volumeTontura.weight = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            efeitoAtivo = !efeitoAtivo;
            Debug.Log("Teste de Confusão: " + (efeitoAtivo ? "LIGADO" : "DESLIGADO"));
        }

        float pesoAlvo = efeitoAtivo ? 1f : 0f;

        if (efeitoAtivo && usarPulsação)
        {
            float oscilacao = (Mathf.Sin(Time.time * velocidadePulso) + 1f) / 2f;
            float pesoPulsante = Mathf.Lerp(intensidadeMinimaNoPulso, 1f, oscilacao);
            volumeTontura.weight = Mathf.MoveTowards(volumeTontura.weight, pesoPulsante, velocidadeTransicao * Time.deltaTime);
        }
        else
        {
            volumeTontura.weight = Mathf.MoveTowards(volumeTontura.weight, pesoAlvo, velocidadeTransicao * Time.deltaTime);
        }
    }

    [ContextMenu("Ativar Efeito")]
    public void AtivarEfeito() => efeitoAtivo = true;

    [ContextMenu("Desativar Efeito")]
    public void DesativarEfeito() => efeitoAtivo = false;
}