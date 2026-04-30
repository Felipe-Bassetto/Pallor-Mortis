using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEvents : MonoBehaviour
{
    public void PiscarLampadas(GameObject[] arrLights)
    {
        if(arrLights.Length != 0)
        {
            foreach (GameObject obj in arrLights)
            {
                StartCoroutine(Pisca(obj));
            }
        }
    }

    IEnumerator Pisca(GameObject obj)
    {
        for (int i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(0.125f);
            obj.SetActive(false);
            yield return new WaitForSeconds(0.125f);
            obj.SetActive(true);
        }
    }

}
