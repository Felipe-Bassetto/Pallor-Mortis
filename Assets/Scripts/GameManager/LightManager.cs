using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    public GameObject[] arrLightsOut;
    public GameObject[] arrLightsOn;

    private void OnTriggerEnter(Collider other)
    {
        foreach (GameObject luz in arrLightsOn)
        {
            luz.SetActive(true);
        }

        foreach (GameObject luz in arrLightsOut)
        {
            luz.SetActive(false);
        }
    }
}
