using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEvents : MonoBehaviour
{
    private bool canBlink = true;

    public void PiscarLampadas(GameObject light, int qtd)
    { 
        StartCoroutine(Pisca(light, qtd));
    }

    public void PiscarLampadasTrigger(GameObject light)
    {
        StartCoroutine(Pisca(light, 0));
    }

    public void AlterStateCanBlink(bool blink)
    {
        canBlink = blink;
    }

    IEnumerator Pisca(GameObject obj, int qtd)
    {
        int counterBlinks = 0;
        while (canBlink)
        {
            if(qtd > 0)
            {
                counterBlinks++;
                if (counterBlinks == qtd) canBlink = false;
            }

            yield return new WaitForSeconds(0.125f);
            obj.SetActive(false);
            yield return new WaitForSeconds(0.125f);
            obj.SetActive(true);
        }
    }



}
