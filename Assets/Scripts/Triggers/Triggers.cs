using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggers : MonoBehaviour
{
    [Header("Porta")]
    public DoorInteraction doorInt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerExit(Collider other)
    {
        // Destroy everything that leaves the trigger
        if(other.gameObject.tag == "Player")
        {
            doorInt.AltState(false);
            Destroy(gameObject);
        }
    }
}
