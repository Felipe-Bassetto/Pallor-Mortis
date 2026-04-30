using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class TriggerLamps : MonoBehaviour
{
    public GameObject[] arrLights;
    public GameObject[] arrLightsInst;

    public GameObject lightBath;
    public GameObject lightCorridor;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        foreach (GameObject luz in arrLightsInst)
        {
            luz.SetActive(false);
            var variables = Variables.Object(luz);
            variables.Set("CanLightUp", false);
        }

        StartCoroutine(BurnLights());
    }

    public IEnumerator BurnLights()
    {
        foreach (GameObject luz in arrLights)
        {
            luz.SetActive(false);
            var variables = Variables.Object(luz);
            variables.Set("CanLightUp", false);
            yield return new WaitForSeconds(1f);
        }
    }

    public void LightController(bool bath)
    {
        if(bath)
        {
            lightBath.SetActive(true);
            lightCorridor.SetActive(false);
        }
        else
        {
            lightBath.SetActive(false);
            lightCorridor.SetActive(true);
        }
    }
}
