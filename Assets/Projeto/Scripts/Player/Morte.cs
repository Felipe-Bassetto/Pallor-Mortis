using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Morte : MonoBehaviour
{
    [SerializeField] private GameObject deathCanvas;
    bool fadeOut = false;
    [SerializeField] private RawImage imageMemorie;
    [SerializeField] private Color preto;
    [SerializeField] private float velocidadeFade;
    [SerializeField] private GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(fadeOut) imageMemorie.color = Color.Lerp(imageMemorie.color, preto, Time.deltaTime * velocidadeFade);
    }

    public void PlayerDeath()
    {
        StartCoroutine(Sla());
    }

    IEnumerator Sla()
    {
        fadeOut = true;
        yield return new WaitForSeconds(3f);
        deathCanvas.SetActive(true);
    }
}
