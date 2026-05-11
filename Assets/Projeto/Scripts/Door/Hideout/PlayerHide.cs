using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHide : MonoBehaviour
{
    [Header("Variaveis")]
    public bool playerMove = false;
    public bool playerBack = false;
    public float velocidade;

    private Collider collDoor;
    private bool canViewCut;

    [Header("Scripts")]
    public DoorInteraction doorInt;
    public Movement mov;
    public PlayerPOV pov;
    public StateController stateCon;

    private Vector3 initialPos;

    [Header("GameObject")]
    public GameObject player;
    public GameObject hidePoint;

    private void Start()
    {
        collDoor = GetComponent<Collider>();
    }

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
        doorInt.RotateDoor(true);
        collDoor.enabled = false;
        initialPos = player.transform.position;
        mov.PlayMovement(true);
        playerMove = true;
        yield return new WaitForSeconds(1);
        doorInt.RotateDoor(false);
        stateCon.changeHidden(true);
        playerMove = false;
        collDoor.enabled = true;

        if(canViewCut) StartCoroutine(StartCutscene());
    }

    IEnumerator HideOut() 
    {
        doorInt.RotateDoor(true);
        collDoor.enabled = false;
        playerBack = true;
        yield return new WaitForSeconds(1);
        doorInt.RotateDoor(false);
        stateCon.changeHidden(false);
        playerBack = false;
        mov.PlayMovement(false);
        collDoor.enabled = true;
    }

    void OnMouseDown()
    {
        if(stateCon.playerHidden && !canViewCut)
        {
            StartCoroutine(HideOut());
        }
        else
        {
            StartCoroutine(HideIn());
        }
    }

    IEnumerator StartCutscene()
    {
        yield return new WaitForSeconds(1);
        ChangeCutsceneBool(false);
    }

    public void ChangeCutsceneBool(bool canStart) => canViewCut = canStart;
}

