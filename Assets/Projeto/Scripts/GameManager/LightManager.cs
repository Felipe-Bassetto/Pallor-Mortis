using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class LightManager : MonoBehaviour
{
    public GameObject[] arrLightsOut;
    public GameObject[] arrLightsOn;

    private void OnTriggerEnter(Collider other)
    {
        foreach (GameObject luz in arrLightsOn)
        {
            var variables = Variables.Object(luz);
            bool canLightUp = variables.Get<bool>("CanLightUp");
            if (canLightUp)
            {
                luz.SetActive(true);
            }

        }

        foreach (GameObject luz in arrLightsOut)
        {
            var variables = Variables.Object(luz);
            bool canLightUp = variables.Get<bool>("CanLightUp");
            if (canLightUp)
            {
                luz.SetActive(false);
            }
        }
    }
}
