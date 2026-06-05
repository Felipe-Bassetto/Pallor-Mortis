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
    private Vector3 noteReading = new Vector3(0, 0, 2f);

    [Header("GameObjects")]
    LayerMask layerMask;
    public GameObject triggerAc;
    public GameObject lightBlink;
    [SerializeField] private GameObject[] arrLights;
    [SerializeField] private DoorInteraction portaFechar;

    [Header("Scripts")]
    public LightEvents le;
    public Movement mov;
    public PlayerPOV pov;
    public PlayerHide ph;

    [SerializeField] private SoundManager sm;

    [Header("Camera")]
    private Camera cameraPrincipal;

    [Header("Porta")]
    public DoorInteraction doorInt;

    [Header("Luzes")]
    [SerializeField] private int qtdLightsBlink;

    [Header("prefab")]
    [SerializeField] private GameObject sombra;

    [Header("Scripts")]
    [SerializeField] private Cutscenes cutscene;

    void Start()
    {
        cameraPrincipal = Camera.main;
        layerMask = LayerMask.GetMask("Note");

        positionInitial = gameObject.transform.position;
        rotationInitial = gameObject.transform.rotation;
        scaleInitial = gameObject.transform.localScale;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
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

                if (portaFechar != null)
                {
                    portaFechar.RotateDoor(false);
                }

                switch (hit.collider.gameObject.tag)
                {
                    case "Nota":
                        sm.PlaySound(7);
                        break;

                    case "Carteirinha":
                        sm.PlaySound(8);
                        break;
                }
            }
        }

        if (noteMove)
        {
            if (Input.GetKey(KeyCode.E))
            {
                gameObject.transform.SetParent(null);
                mov.PlayMovement(false);
                pov.CamLock(true);
                noteBack = true;
                noteMove = false;

                if (doorInt != null)
                {
                    doorInt.AltState(false);
                    sm.PlaySound(3);
                }

                if (lightBlink != null)
                {
                    le.PiscarLampadas(lightBlink, qtdLightsBlink);
                    lightBlink = null;
                }
            }

            gameObject.transform.localPosition = Vector3.MoveTowards(
                gameObject.transform.localPosition,
                noteReading,
                velocidade * Time.deltaTime);

            Quaternion lookRot = Quaternion.LookRotation(
                cameraPrincipal.transform.position - gameObject.transform.position,
                Vector3.up);

            gameObject.transform.rotation = Quaternion.Slerp(
                gameObject.transform.rotation,
                lookRot,
                velocidadeRotacao * Time.deltaTime);
        }

        if (noteBack)
        {
            gameObject.transform.position = Vector3.MoveTowards(
                gameObject.transform.localPosition,
                positionInitial,
                velocidade * Time.deltaTime);

            gameObject.transform.rotation = Quaternion.Slerp(
                gameObject.transform.rotation,
                rotationInitial,
                velocidadeRotacao * Time.deltaTime);

            gameObject.transform.localScale = scaleInitial;

            if (triggerAc != null)
            {
                triggerAc.SetActive(true);
                le.AcenderLuzes(arrLights);
                Instantiate(sombra, new Vector3(7.5f, 1f, -7f), Quaternion.identity);
                ph.ChangeCutsceneBool(true);
                triggerAc = null;
            }

            if (cutscene != null) ;
        }
    }
}