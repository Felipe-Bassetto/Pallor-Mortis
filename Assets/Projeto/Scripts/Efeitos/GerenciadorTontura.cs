using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GerenciadorConfusao : MonoBehaviour
{
    [Header("Referências de Volume")]
    public Volume volumeTontura; 
    private Vignette vignetteComponent;

    [Header("Configurações da Transição")]
    public float velocidadeTransicao = 0.75f;
    
    [Header("Configurações da Tontura")]
    [SerializeField] private float velocidadeVignette = 1.5f;
    public bool usarPulsação = true;
    public float velocidadePulso = 2f;
    public float intensidadeMinimaNoPulso = 0.1f;
    private float intensidadeAlvo = 0.1f;

    [Header("Teste (Aperte T no jogo)")]
    public bool efeitoAtivo = false;

    [Header("TimerEfeito")]
    [SerializeField] private float maxCount;

    private bool canCount = false;
    private float counterTimer = 0f;

    [Header("Scripts")]
    [SerializeField] private GameManager gm;

    void Start()
    {
        if (volumeTontura != null) volumeTontura.weight = 0;

        if (volumeTontura.profile.TryGet(out vignetteComponent))
        {
            intensidadeAlvo = 0.1f;
            vignetteComponent.intensity.value = intensidadeAlvo;
        }
    }

    void Update()
    {
        if(canCount)
        {
            if (counterTimer >= maxCount)
            {
                if (vignetteComponent.intensity.value >= 0.7f) gm.Restart();
                else
                {
                    counterTimer = 0f;
                    intensidadeMinimaNoPulso += 0.1f;
                    intensidadeAlvo += 0.1f;
                }

            }
            else counterTimer += Time.deltaTime;
        }

        vignetteComponent.intensity.value = Mathf.Lerp(vignetteComponent.intensity.value,intensidadeAlvo,velocidadeVignette * Time.deltaTime);

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
    public void AtivarEfeito()
    {
        efeitoAtivo = true;
        canCount = true;
    }


    [ContextMenu("Desativar Efeito")]
    public void DesativarEfeito() => efeitoAtivo = false;
}