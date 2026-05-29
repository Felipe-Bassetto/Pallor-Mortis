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

    [Header("Componentes")]
    [SerializeField] private Rigidbody rb;

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

    IEnumerator MemoriesBailarina()
    {
        imageMemorie.texture = arrTexturas[1];
        fadeIn = true;
        yield return new WaitForSeconds(5f); // tempo com imagem ativa

        velocidadeFade = 5;
        fadeOut = true;

        yield return new WaitForSeconds(2.5f);

        imageMemorie.texture = arrTexturas[2];
        fadeIn = true;
        sm.DiminuirVolumeGradual(3);

        yield return new WaitForSeconds(5f); // tempo com imagem ativa

        fadeOut = true;

        yield return new WaitForSeconds(2.5f);

        imageMemorie.texture = arrTexturas[3];
        fadeIn = true;

        sm.PlayOST(1);
        sm.AumentarVolumeGradual(3);

        yield return new WaitForSeconds(5f); // tempo com imagem ativa

        fadeOut = true;

        yield return new WaitForSeconds(2.5f);

        imageMemorie.texture = arrTexturas[4];
        fadeIn = true;

        yield return new WaitForSeconds(5f); // tempo com imagem ativa

        fadeOut = true;

        yield return new WaitForSeconds(2.5f);

        imageMemorie.texture = arrTexturas[5];
        desativar = true;

        sm.DiminuirVolumeGradual(3);

        yield return new WaitForSeconds(3f);

        sm.AumentarVolumeGradual(2);

        pov.Cutscene(false);
        mov.PlayMovement(false);
        rb.isKinematic = false;
    }
}
