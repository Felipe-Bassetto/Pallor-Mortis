using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerActive : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject trigger;

    public void ActivateTrigger()
    {
        trigger.SetActive(true);
    }

}
