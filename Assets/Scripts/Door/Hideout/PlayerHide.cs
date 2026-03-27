using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHide : MonoBehaviour
{
    [Header("Variaveis")]
    public bool playerMove = false;
    public bool playerBack = false;
    public float velocidade;

    [Header("Scripts")]
    public DoorInteraction doorInt;
    public Movement mov;
    public PlayerPOV pov;
    public StateController stateCon;

    private Vector3 initialPos;
    private Quaternion initialRot;

    [Header("GameObject")]
    public GameObject player;
    public GameObject hidePoint;

    void Update()
    {
        if (playerMove)
        {
            Debug.Log("Escondendo");
            player.transform.position = Vector3.MoveTowards(player.transform.position, hidePoint.transform.position, velocidade * Time.unscaledDeltaTime);
        }
        if (playerBack) player.transform.position = Vector3.MoveTowards(player.transform.position, initialPos, velocidade * Time.unscaledDeltaTime);
    }

    IEnumerator HideIn() 
    {
        initialPos = player.transform.position;
        mov.PlayMovement(true);
        playerMove = true;
        yield return new WaitForSeconds(1);
        doorInt.RotateDoor(false);
        stateCon.changeHidden(true);
        playerMove = false;
    }

    IEnumerator HideOut() 
    {
        playerBack = true;
        yield return new WaitForSeconds(1);
        doorInt.RotateDoor(false);
        stateCon.changeHidden(false);
        playerBack = false;
        mov.PlayMovement(false);
    }

    void OnMouseDown()
    {
        if(stateCon.playerHidden)
        {
            StartCoroutine(HideOut());
        }
        else
        {
            StartCoroutine(HideIn());
        }
        
    }

}

