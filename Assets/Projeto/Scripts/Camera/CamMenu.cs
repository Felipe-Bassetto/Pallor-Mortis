using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMenu : MonoBehaviour
{
    [Header("Configurações de Camera(POV)")]
    public bool camMove;
    public float velocidade;

    [SerializeField] private GameObject camSpot;

    Camera cam;
    private Vector3 camLocation;

    [Header("Scripts")]
    [SerializeField] private Cutscenes cut;

    // Start is called before the first frame update
    void Start()
    {
        camLocation = camSpot.transform.position;
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (camMove) cam.transform.position = Vector3.MoveTowards(cam.transform.position, camLocation, velocidade * Time.unscaledDeltaTime);

        if (cam.transform.position == camLocation)
        {
            CameraMovement(false);
            cut.Inicio();
        }
    }

    public void CameraMovement(bool move) => camMove = move;

    public void CamPos()
    {
        camMove = false;
        cam.transform.position = camSpot.gameObject.transform.position;
    }
}
