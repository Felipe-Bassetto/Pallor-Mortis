using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class TriggerLamps : MonoBehaviour
{
    public GameObject[] arrLights;
    public GameObject[] arrLightsInst;

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
}
