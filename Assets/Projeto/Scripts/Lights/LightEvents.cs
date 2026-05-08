using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LightEvents : MonoBehaviour
{
    [Header("Arrays")]
    [SerializeField] private GameObject[] arrControllers;
    [SerializeField] private GameObject[] arrLights;
    [SerializeField] private GameObject[] lightsBath;
    [SerializeField] private GameObject[] lightsCorridor;

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

    public void ApagarLuzes(GameObject[] arrLuzes)
    {
        foreach(GameObject obj in arrLuzes)
        {
            obj.SetActive(false);
        }
    }

    public void AcenderLuzes(GameObject[] arrLuzes)
    {
        foreach(GameObject obj in arrLuzes)
        {
            obj.SetActive(true);
        }
    }

    public void LigarDesligControllers(bool isOn)
    {
        foreach(GameObject obj in arrControllers)
        {
            obj.SetActive(isOn);
        }
    }

    public void LightController(bool bath)
    {
        if (bath)
        {
            AcenderLuzes(lightsBath);
            ApagarLuzes(lightsCorridor);
        }
        else
        {
            AcenderLuzes(lightsCorridor);
            ApagarLuzes(lightsBath);
        }
    }

    public void StartBurn()
    {
        StartCoroutine(BurnLights());
    }

    IEnumerator Pisca(GameObject obj, int qtd)
    {
        int counterBlinks = 0;

        Debug.Log("canBlink: " + canBlink);
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

    IEnumerator BurnLights()
    {
        foreach (GameObject luz in arrLights)
        {
            luz.SetActive(false);
            Debug.Log(luz.name);
            var variables = Variables.Object(luz);
            variables.Set("CanLightUp", false);
            yield return new WaitForSeconds(1f);
        }
    }

}
