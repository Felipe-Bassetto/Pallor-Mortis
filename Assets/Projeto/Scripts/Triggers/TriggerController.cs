 using UnityEngine;
using UnityEngine.Events;
using System.Collections;

[System.Serializable]
public class TriggerEvent
{
    public float delay = 0f;
    public UnityEvent onTrigger;
}

public class TriggerController : MonoBehaviour
{
    public string targetTag = "Player";
    public TriggerEvent[] onTriggerEvents;
    public bool multTimes;

    bool triggered;

    void OnTriggerEnter(Collider other)
    {
        if (triggered) return;
        if (!other.CompareTag(targetTag)) return;

        if(!multTimes) triggered = true;
        StartCoroutine(InvokeDelayed());
    }

    IEnumerator InvokeDelayed()
    {
        foreach (var triggerEvent in onTriggerEvents)
        {
            if (triggerEvent == null) continue;
            if (triggerEvent.delay > 0f)
                yield return new WaitForSeconds(triggerEvent.delay);

            triggerEvent.onTrigger?.Invoke();
        }
    }
}
