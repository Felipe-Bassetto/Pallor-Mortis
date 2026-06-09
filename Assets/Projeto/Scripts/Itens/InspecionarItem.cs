using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InspecionarItem : MonoBehaviour
{
    [Header("Camera")]
    private Camera cameraPrincipal;

    [Header("Variaveis")]
    public float velocidade = 0.1f;
    public float velocidadeRotacao = 1f;
    public float velocidadeInspec = 5f;
    public float distanceClick = 1f;

    LayerMask layerMask;
    private bool objMove = false;
    private bool objBack = false;
    public Vector3 objInspecting;
    private Vector3 positionInitial;
    private Quaternion rotationInitial;
    private Vector3 scaleInitial;
    private bool isIspecting = false;
    private Quaternion rotationInspecting;
    private bool canRotate;
    private Quaternion lookRot;

    [Header("Scripts")]
    [SerializeField] private Movement mov;
    [SerializeField] private PlayerPOV pov;
    [SerializeField] private SoundManager sm;

    // Start is called before the first frame update
    void Start()
    {
        cameraPrincipal = Camera.main;
        layerMask = LayerMask.GetMask("Memorias");

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

            if (Physics.Raycast(ray, out hit, distanceClick, layerMask) && !objMove && gameObject.name == hit.collider.gameObject.name && !isIspecting)
            {
                gameObject.transform.SetParent(cameraPrincipal.transform);
                objMove = true;
                objBack = false;
                canRotate = true;
                mov.PlayMovement(true);
                pov.CamLock(false);
                isIspecting = true;

                lookRot = Quaternion.LookRotation(cameraPrincipal.transform.position - gameObject.transform.position, Vector3.up);

                switch (hit.collider.gameObject.tag)
                {
                    case "Retrato":
                        sm.PlaySound(10);
                        break;

                    case "CaixaDeMusica":
                        sm.PlaySound(17);
                        break;

                    case "Garrafa":
                        sm.PlaySound(18);
                        break;

                    case "Ursinho":
                        sm.PlaySound(19);
                        break;
                }
            }
        }

        if (Input.GetMouseButton(0) && isIspecting)
        {
            float rotX = Input.GetAxis("Mouse X") * velocidadeInspec;
            float rotY = Input.GetAxis("Mouse Y") * velocidadeInspec;

            gameObject.transform.Rotate(Vector3.up, -rotX, Space.World);
            gameObject.transform.Rotate(Vector3.right, rotY, Space.World);
        }

        if (objMove) // Mantem a nota na tela para ser lida
        {
            if (Input.GetKey(KeyCode.E)) // Voltar nota para posição original
            {
                gameObject.transform.SetParent(null);
                mov.PlayMovement(false);
                pov.CamLock(true);
                objBack = true;
                objMove = false;

                isIspecting = false;
            }

            gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition, objInspecting, velocidade * Time.deltaTime);

            if(gameObject.transform.rotation == lookRot) canRotate = false;

            if(canRotate) gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, lookRot, velocidadeRotacao * Time.deltaTime);
        }

        if (objBack)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.localPosition, positionInitial, velocidade * Time.deltaTime);
            gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, rotationInitial, velocidadeRotacao * Time.deltaTime);
            gameObject.transform.localScale = scaleInitial;
        }
    }
}
