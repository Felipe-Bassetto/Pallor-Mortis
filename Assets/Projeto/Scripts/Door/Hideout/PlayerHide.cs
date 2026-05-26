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
    private Rigidbody rbPlayer;

    [Header("Scripts")]
    public DoorInteraction doorInt;
    public DoorInteraction doorInt2;
    public Movement mov;
    public PlayerPOV pov;
    public StateController stateCon;

    [SerializeField] private SoundManager sm;

    private Vector3 initialPos;

    [Header("GameObject")]
    public GameObject player;
    public GameObject hidePoint;
    [SerializeField] private GameObject triggerMirror;

    private void Start()
    {
        collDoor = GetComponent<Collider>();
        rbPlayer = player.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (playerMove)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, hidePoint.transform.position, velocidade * Time.unscaledDeltaTime);
        }
        if (playerBack) player.transform.position = Vector3.MoveTowards(player.transform.position, initialPos, velocidade * Time.unscaledDeltaTime);

    }

    IEnumerator HideIn() 
    {
        rbPlayer.isKinematic = true;

        doorInt.RotateDoor(true);
        if(doorInt2 != null) doorInt2.RotateDoor(true);

        collDoor.enabled = false;
        initialPos = player.transform.position;
        mov.PlayMovement(true);
        playerMove = true;

        yield return new WaitForSeconds(1);

        doorInt.RotateDoor(false);
        if (doorInt2 != null) doorInt2.RotateDoor(false);

        stateCon.changeHidden(true);
        playerMove = false;
        collDoor.enabled = true;

        if(canViewCut) StartCoroutine(StartCutscene());

        rbPlayer.isKinematic = false;
    }

    IEnumerator HideOut() 
    {
        rbPlayer.isKinematic = true;

        doorInt.RotateDoor(true);
        if (doorInt2 != null) doorInt2.RotateDoor(true);

        collDoor.enabled = false;
        playerBack = true;

        yield return new WaitForSeconds(1); // espera 1 segundo

        doorInt.RotateDoor(false);
        if (doorInt2 != null) doorInt2.RotateDoor(false);

        stateCon.changeHidden(false);
        playerBack = false;
        mov.PlayMovement(false);

        yield return new WaitForSeconds(1); // espera 1 segundo

        collDoor.enabled = true;

        rbPlayer.isKinematic = false;
    }

    void OnMouseDown()
    {
        if(stateCon.playerHidden && !canViewCut)
        {
            StartCoroutine(HideOut());
        }
        else if(!stateCon.playerHidden)
        {
            StartCoroutine(HideIn());
        }
    }

    IEnumerator StartCutscene()
    {
        sm.PlaySound(11);
        yield return new WaitForSeconds(3f);
        ChangeCutsceneBool(false);
        triggerMirror.SetActive(true);
    }

    public void ChangeCutsceneBool(bool canStart)
    {
        canViewCut = canStart;
        doorInt.ChangeCanClick(canStart);
    }

}

