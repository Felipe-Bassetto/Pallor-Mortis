using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class ItemEvents : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject triggerPorta4;
    public GameObject[] arrLuzes;

    [Header("Scripts")]
    [SerializeField] private LightEvents le;
    [SerializeField] private PlayerPOV POV;
    [SerializeField] private DoorInteraction door;

    [Header("Audio")]
    [SerializeField] private AudioSource audio;

    public void GrabKey()
    {
        var variables = Variables.Object(gameObject);
        triggerPorta4.SetActive(true);
        variables.Set("triggerActived", true);
        le.AlterStateCanBlink(false);
        le.ApagarLuzes(arrLuzes);
        POV.ConfusionActive(false);
        audio.Stop();
        door.AltState(false);
    }
}
