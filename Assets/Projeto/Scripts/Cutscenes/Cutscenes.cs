using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscenes : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private PlayerPOV pov;
    [SerializeField] private Movement mov;
    [SerializeField] private SoundManager sm;

    [Header("Camera")]
    private Camera cam;

    [Header("Custcene Bailarina")]
    [SerializeField] private Transform rostoBailarina;
    [SerializeField] private Transform pontoDireita;
    [SerializeField] private Transform pontoEsquerda;
    [SerializeField] private Transform caixinha;
    [SerializeField] private Transform pontoFinal;

    [Header("Variaveis Gerais")]
    [SerializeField] private float speedRotation;
    [SerializeField] private float speedMoving;

    private bool camRotationCutscene;
    private bool playerMovingCutscene;
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

        if(playerMovingCutscene) player.position = Vector3.MoveTowards(player.position, pontoFinal.position, speedMoving * Time.deltaTime);

        if (cam.transform.rotation == lookDirection) camRotationCutscene = false;
        if (player.position == pontoFinal.position) playerMovingCutscene = false;
    }

    public void Bailarina()
    {
        pov.Cutscene(true);
        mov.PlayMovement(true);
        rb.isKinematic = true;

        sm.DiminuirVolumeGradual(2);

        StartCoroutine(CutBailarina());
    }

    IEnumerator CutBailarina()
    {
        speedRotation = 0.5f;
        lookDirection = Quaternion.LookRotation(pontoDireita.position - cam.transform.position);
        camRotationCutscene = true;
        playerMovingCutscene = true;

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
}
