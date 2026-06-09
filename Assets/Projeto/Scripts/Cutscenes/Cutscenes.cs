using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscenes : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private PlayerPOV pov;
    [SerializeField] private Movement mov;
    [SerializeField] private SoundManager sm;
    [SerializeField] private Memorias memories;

    [Header("Camera")]
    private Camera cam;

    [Header("Custcene Bailarina")]
    [SerializeField] private Transform rostoBailarina;
    [SerializeField] private Transform pontoDireita;
    [SerializeField] private Transform pontoEsquerda;
    [SerializeField] private Transform caixinha;

    [Header("Cutscenes Gerais")]
    [SerializeField] private Transform pontoFinal;

    [Header("Cutscene Inicial")]
    [SerializeField] private Transform pontoFrente;
    [SerializeField] private Transform pontoLado;

    [Header("Variaveis Gerais")]
    [SerializeField] private float speedRotation;
    [SerializeField] private float speedMoving;

    private bool camRotationCutscene;
    private bool playerMovingCutscene;
    private bool camMovingCutscene;
    [SerializeField] private bool memoriaBailarinaAtiva = false;
    [SerializeField] private bool memoriaEspelhoAtiva = true;
    private Quaternion lookDirection;

    [Header("Componentes")]
    [SerializeField] private Rigidbody rb;

    [Header("Player")]
    [SerializeField] private Transform player;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        sm = FindFirstObjectByType<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(camRotationCutscene) cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, lookDirection, speedRotation * Time.deltaTime);

        if(camMovingCutscene) cam.gameObject.transform.position = Vector3.MoveTowards(cam.gameObject.transform.position, pontoFinal.position, speedMoving * Time.deltaTime);

        if(pontoFinal != null)
        {
            if(cam.gameObject.transform.position == pontoFinal.position)  camMovingCutscene = false;
        }
        
        if (playerMovingCutscene) player.position = Vector3.MoveTowards(player.position, pontoFinal.position, speedMoving * Time.deltaTime);

        if (cam.transform.rotation == lookDirection) camRotationCutscene = false;

        if (pontoFinal != null)
        {
            if (player.position == pontoFinal.position)
            {
                playerMovingCutscene = false;
                if (!memoriaBailarinaAtiva)
                {
                    memories.MemoriaBailarina();
                    memoriaBailarinaAtiva = true;
                }

                if (!memoriaEspelhoAtiva)
                {
                    memories.MemoriaMirror();
                    memoriaEspelhoAtiva = true;
                }
            }
        }
    }

    public void Inicio()
    {
        StartCoroutine(CutInicio());
    }

    public void Bailarina()
    {
        pov.Cutscene(true);
        mov.PlayMovement(false);
        rb.isKinematic = true;

        sm.DiminuirVolumeGradual(2);

        StartCoroutine(CutBailarina());
    }

    public void Espelho()
    {
        pov.Cutscene(true);
        mov.PlayMovement(true);
        rb.isKinematic = true;

        sm.DiminuirVolumeGradual(2);

        StartCoroutine(CutMirror());
    }

    public void Necroterio()
    {
        pov.Cutscene(true);
        mov.PlayMovement(false);
        rb.isKinematic = true;

        sm.DiminuirVolumeGradual(2);

        StartCoroutine(CutNecro());
    }

    IEnumerator CutInicio()
    {
        camMovingCutscene = true;

        speedRotation = 0.7f;
        lookDirection = Quaternion.LookRotation(pontoFrente.position - cam.transform.position);
        camRotationCutscene = true;

        yield return new WaitForSeconds(1.5f);

        camMovingCutscene = false;
        lookDirection = Quaternion.LookRotation(pontoLado.position - cam.transform.position);
        camRotationCutscene = true;

        yield return new WaitForSeconds(1.5f);

        lookDirection = Quaternion.LookRotation(pontoFrente.position - cam.transform.position);
        camRotationCutscene = true;

        yield return new WaitForSeconds(1.5f);

        camRotationCutscene = false;
        camMovingCutscene = false;

        memories.InicioGame();
    }

    IEnumerator CutBailarina()
    {
        playerMovingCutscene = true;
        yield return new WaitForSeconds(2f);
        sm.PlayOST(0);
        sm.AumentarVolumeGradual(3);

        speedRotation = 0.5f;
        lookDirection = Quaternion.LookRotation(pontoDireita.position - cam.transform.position);
        camRotationCutscene = true;
        

        yield return new WaitForSeconds(2f);

        speedRotation = 0.25f;
        lookDirection = Quaternion.LookRotation(pontoEsquerda.position - cam.transform.position);
        camRotationCutscene = true;

        yield return new WaitForSeconds(4f);

        lookDirection = Quaternion.LookRotation(rostoBailarina.position - cam.transform.position);
        camRotationCutscene = true;

        yield return new WaitForSeconds(3f);

        lookDirection = Quaternion.LookRotation(caixinha.position - cam.transform.position);
        camRotationCutscene = true;
    }

    IEnumerator CutMirror()
    {
        playerMovingCutscene = true;
        yield return new WaitForSeconds(1f);

        sm.PlayOST(0);
        sm.AumentarVolumeGradual(3);
    }
    IEnumerator CutNecro()
    {
        sm.PlayOST(0);
        sm.AumentarVolumeGradual(3);

        yield return new WaitForSeconds(2f);

        memories.MemoriaNecroterio();
    }
}
