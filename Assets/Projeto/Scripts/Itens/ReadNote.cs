using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadNote : MonoBehaviour
{
    [Header("Nota")]
    public float velocidade = 0.1f;
    public float velocidadeRotacao = 1f;
    public float distanceClick;

    private Vector3 positionInitial;
    private Quaternion rotationInitial;
    private Vector3 scaleInitial;
    private bool noteMove = false;
    private bool noteBack = false;
    private Vector3 noteReading = new Vector3(0,0,0.5f);

    [Header("GameObjects")]
    LayerMask layerMask;
    public GameObject triggerAc;
    public GameObject lightBlink;

    [Header("Scripts")]
    public LightEvents le;
    public Movement mov;
    public PlayerPOV pov;

    [Header("Camera")]
    private Camera cameraPrincipal;

    [Header("Porta")]
    public DoorInteraction doorInt;

    [Header("Luzes")]
    [SerializeField] private int qtdLightsBlink;

    // Start is called before the first frame update
    void Start()
    {
        cameraPrincipal = Camera.main;
        layerMask = LayerMask.GetMask("Note");

        positionInitial = gameObject.transform.position;
        rotationInitial = gameObject.transform.rotation;
        scaleInitial = gameObject.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Verifica click na nota
        {
            Ray ray = cameraPrincipal.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit; 

            if (Physics.Raycast(ray, out hit, distanceClick, layerMask) && !noteMove && gameObject.name == hit.collider.gameObject.name)
            {
                gameObject.transform.SetParent(cameraPrincipal.transform);
                noteMove = true;
                noteBack = false;
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

                if(lightBlink != null)
                {
                    le.PiscarLampadas(lightBlink, qtdLightsBlink);
                    lightBlink = null;
                }
            }

            gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition, noteReading, velocidade * Time.deltaTime);
            Quaternion lookRot = Quaternion.LookRotation(cameraPrincipal.transform.position - gameObject.transform.position, Vector3.up);

            gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation,lookRot,velocidadeRotacao * Time.deltaTime);
        }

        if(noteBack)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.localPosition, positionInitial, velocidade * Time.deltaTime);
            gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, rotationInitial, velocidadeRotacao * Time.deltaTime);
            gameObject.transform.localScale = scaleInitial;
            if(triggerAc != null)
            {
                triggerAc.SetActive(true);
            }
        }
    }
}
