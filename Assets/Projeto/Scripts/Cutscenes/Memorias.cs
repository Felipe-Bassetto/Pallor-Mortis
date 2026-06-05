using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Memorias : MonoBehaviour
{
    [Header("Texturas")]
    [SerializeField] private Texture[] arrTexturas;
    
    [Header("GameObjects")]
    [SerializeField] private GameObject memoriaObj;
    [SerializeField] private RawImage imageMemorie;
    [SerializeField] private GameObject prefabCartao;
    [SerializeField] private GameObject caixa;
    [SerializeField] private GameObject pontoC;

    [Header("Cores")]
    [SerializeField] private Color preto;
    [SerializeField] private Color branco;
    [SerializeField] private Color transparente;

    [Header("Variaveis")]
    [SerializeField] private float velocidadeFade;
    private bool fadeIn;
    private bool fadeOut;
    private bool desativar;

    [Header("Scripts")]
    [SerializeField] private SoundManager sm;
    [SerializeField] private PlayerPOV pov;
    [SerializeField] private Movement mov;
    [SerializeField] private GerenciadorMemorias gerenciador;

    [Header("Componentes")]
    [SerializeField] private Rigidbody rb;

    [Header("Luzes")]
    [SerializeField] private GameObject luz1;
    [SerializeField] private GameObject luz2;

    [Header("Camera")]
    [SerializeField] private GameObject camPlayer;
    [SerializeField] private GameObject camNecro;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(fadeIn) imageMemorie.color = Color.Lerp(imageMemorie.color, branco, Time.deltaTime * velocidadeFade);
        if(fadeOut) imageMemorie.color = Color.Lerp(imageMemorie.color, preto, Time.deltaTime * velocidadeFade);
        if(desativar) imageMemorie.color = Color.Lerp(imageMemorie.color, transparente, Time.deltaTime * velocidadeFade);

        if(imageMemorie.color == preto) fadeOut = false;
        if(imageMemorie.color == branco) fadeIn = false;
        if(imageMemorie.color == transparente) desativar = false;
    }

    public void MemoriaBailarina()
    {
        memoriaObj.SetActive(true);
        StartCoroutine(MemoriesBailarina());
    }

    public void MemoriaMirror()
    {
        gerenciador.AtivarMemorias();
        StartCoroutine(MemoriesMirror());
    }

    public void MemoriaNecroterio()
    {
        gerenciador.AtivarMemorias();
        StartCoroutine(MemoriesNecro());
    }

    IEnumerator MemoriesBailarina()
    {
        imageMemorie.texture = arrTexturas[1];
        fadeIn = true;
        yield return new WaitForSeconds(5f); // tempo com imagem ativa

        velocidadeFade = 5;
        fadeOut = true;

        yield return new WaitForSeconds(2.7f);

        imageMemorie.texture = arrTexturas[2];
        fadeIn = true;
        luz1.SetActive(false);
        luz2.SetActive(true);

        yield return new WaitForSeconds(5f); // tempo com imagem ativa

        fadeOut = true;
        yield return new WaitForSeconds(0.8f);
        sm.PlaySound(15);
        yield return new WaitForSeconds(2.7f);

        imageMemorie.texture = arrTexturas[3];
        fadeIn = true;

        yield return new WaitForSeconds(5f); // tempo com imagem ativa

        fadeOut = true;

        yield return new WaitForSeconds(2.7f);

        imageMemorie.texture = arrTexturas[4];
        fadeIn = true;

        yield return new WaitForSeconds(4.5f); // tempo com imagem ativa

        fadeOut = true;
        
        yield return new WaitForSeconds(3f);
        

        imageMemorie.texture = arrTexturas[5];
        fadeIn = true;

        sm.PlaySound(16);
        yield return new WaitForSeconds(9f);

        fadeOut = true;

        desativar = true;

        sm.DiminuirVolumeGradual(3);

        Instantiate(prefabCartao, pontoC.transform.position, Quaternion.identity);

        yield return new WaitForSeconds(1.5f);

        sm.AumentarVolumeGradual(2);

        pov.Cutscene(false);
        mov.PlayMovement(false);
        rb.isKinematic = false;        
    }

    IEnumerator MemoriesMirror()
    {
        imageMemorie.texture = arrTexturas[0];

        fadeIn = true;
        yield return new WaitForSeconds(10f);

        desativar = true;

        yield return new WaitForSeconds(1f);

        gerenciador.DesativarMemorias();

        sm.DiminuirVolumeGradual(3);

        sm.AumentarVolumeGradual(2);

        pov.Cutscene(false);
        mov.PlayMovement(false);
        rb.isKinematic = false; 
    }

    IEnumerator MemoriesNecro()
    {
        imageMemorie.texture = arrTexturas[0];

        fadeIn = true;
        yield return new WaitForSeconds(1f);
        fadeOut = true;

        yield return new WaitForSeconds(2.7f);

        imageMemorie.texture = arrTexturas[1];
        fadeIn = true;

        yield return new WaitForSeconds(1f);
        fadeOut = true;

        yield return new WaitForSeconds(2.7f);

        imageMemorie.texture = arrTexturas[2];
        fadeIn = true;

        yield return new WaitForSeconds(1f);
        fadeOut = true;

        yield return new WaitForSeconds(2.7f);

        imageMemorie.texture = arrTexturas[4];
        fadeIn = true;

        yield return new WaitForSeconds(1f);
        fadeOut = true;

        yield return new WaitForSeconds(2.7f);

        imageMemorie.texture = arrTexturas[5];

        fadeIn = true;

        yield return new WaitForSeconds(1f);
        fadeOut = true;
    }
}
