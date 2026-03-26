using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggers : MonoBehaviour
{
    [Header("Porta")]
    public DoorInteraction doorInt;
    public DoorAction doorAct;
    
    [Header("Trigger")]
    public int index;

    void OnTriggerExit(Collider other)
    {
        // Destroy everything that leaves the trigger
        if(other.gameObject.tag == "Player")
        {
            doorInt.AltState(false);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        doorAct.OpenDoor(true);
    }
}
