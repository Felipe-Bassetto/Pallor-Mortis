using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class TriggerLamps : MonoBehaviour
{
    public GameObject[] arrLights;
    public GameObject[] arrLightsInst;

    public GameObject[] lightsBath;
    public GameObject[] lightsCorridor;

    [Header("Scripts")]
    [SerializeField] private LightEvents le;

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
            le.AcenderLuzes(lightsBath);
            le.ApagarLuzes(lightsCorridor);
        }
        else
        {
            le.AcenderLuzes(lightsCorridor);
            le.ApagarLuzes(lightsBath);
        }
    }
}
