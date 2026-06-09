using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Creditos : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private GameObject creditosText;
    [SerializeField] private Image logoStudio;

    [Header("Movimentação")]
    [SerializeField] private float velocidade = 50f;
    [SerializeField] private float endY = 2500f;

    [Header("Fade")]
    [SerializeField] private float fadeDuration = 2f;

    private RectTransform rectTransformCreditos;

    private bool creditosAtivos;
    private bool creditosFinalizados;

    private void Awake()
    {
        creditosText.SetActive(false);
        logoStudio.gameObject.SetActive(false);

        rectTransformCreditos = creditosText.GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (!creditosAtivos || creditosFinalizados)
            return;

        rectTransformCreditos.anchoredPosition += Vector2.up * velocidade * Time.deltaTime;

        if (rectTransformCreditos.anchoredPosition.y >= endY)
        {
            creditosFinalizados = true;
            StartCoroutine(FadeLogo());
        }
    }

    public void IniciarCreditos()
    {
        creditosText.SetActive(true);

        creditosAtivos = true;
    }

    private IEnumerator FadeLogo()
    {
        logoStudio.gameObject.SetActive(true);

        Color cor = logoStudio.color;
        cor.a = 0f;
        logoStudio.color = cor;

        float tempo = 0f;

        while (tempo < fadeDuration)
        {
            tempo += Time.deltaTime;

            cor.a = Mathf.Clamp01(tempo / fadeDuration);
            logoStudio.color = cor;

            yield return null;
        }

        SceneManager.LoadScene("Tela Inicial");
    }
}