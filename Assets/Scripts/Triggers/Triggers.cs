using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggers : MonoBehaviour
{

    [Header("Porta")]
    public DoorInteraction doorInt;
    public DoorAction doorAct;
    

    void OnTriggerExit(Collider other)
    {
        // Destroy everything that leaves the trigger
        if(other.gameObject.tag == "Player")
        {
            if (doorInt != null)
            {
                doorInt.AltState(false);
            }
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(doorAct != null && other.tag == "Player")
        {
            if (doorAct.portaIdent == DoorAction.Porta.Porta4)
            {
                doorInt.AltState(false);
                doorAct.OpenDoor(true);
                Destroy(gameObject);
            }   
        }
        
    }
}
