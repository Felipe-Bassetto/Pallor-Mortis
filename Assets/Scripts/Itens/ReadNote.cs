using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadNote : MonoBehaviour
{
    [Header("Nota")]
    public float velocidade = 0.1f;
    public float velocidadeRotacao = 1f;

    private Vector3 positionInitial;
    private Quaternion rotationInitial;
    private bool noteMove = false;
    private bool noteBack = false;
    private Vector3 noteReading = new Vector3(0,0,0.5f);
    private Quaternion noteRotation = Quaternion.Euler(-90, 0, 0);

    [Header("GameObjects")]
    LayerMask layerMask;
    public Movement mov;
    public PlayerPOV pov;

    [Header("Camera")]
    private Camera cameraPrincipal;

    [Header("Porta")]
    public DoorInteraction doorInt;

    // Start is called before the first frame update
    void Start()
    {
        cameraPrincipal = Camera.main;
        layerMask = LayerMask.GetMask("Note");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Verifica click na nota
        {
            Ray ray = cameraPrincipal.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit; 

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask) && !noteMove)
            {
                positionInitial = gameObject.transform.localPosition;
                rotationInitial = gameObject.transform.rotation;
                gameObject.transform.SetParent(cameraPrincipal.transform);
                noteMove = true;
                mov.PlayMovement(true);
                pov.CamLock(false);
            } 
        }

        if(noteMove) // Mantem a nota na tela para ser lida
        {
            if(Input.GetKey(KeyCode.E)) // Voltar nota para posição original
            {
                gameObject.transform.SetParent(null);
                doorInt.AltState(false);
                mov.PlayMovement(false);
                pov.CamLock(true);
                noteBack = true;
                noteMove = false;
            }

            gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition, noteReading, velocidade * Time.unscaledDeltaTime);
            Quaternion lookRot = Quaternion.LookRotation(cameraPrincipal.transform.position - gameObject.transform.position, Vector3.up);

            gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation,lookRot,velocidadeRotacao * Time.unscaledDeltaTime);
        }

        if(noteBack)
        {
            gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition, positionInitial, velocidade * Time.unscaledDeltaTime);
            gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation,noteRotation,velocidadeRotacao * Time.unscaledDeltaTime);
        }
    }
}
