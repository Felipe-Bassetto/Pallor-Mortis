using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class ItemEvents : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject triggerPorta4;
    public GameObject[] arrLuzes;

    [SerializeField] private GameObject prefabChave2;
    [SerializeField] private GameObject prefabUrsinhoCortado;

    [Header("Scripts")]
    [SerializeField] private LightEvents le;
    [SerializeField] private DoorInteraction door;
    [SerializeField] private GerenciadorConfusao confusion;
    [SerializeField] private ItensController ic;

    [Header("Audio")]
    public AudioSource audioSource;

    [Header("Camera")]
    private Camera cameraPrincipal;

    [Header("Necroterio")]
    [SerializeField] private Cutscenes cut;

    private bool receiturarioLido = false;
    private bool obituarioLido = false;

    private void Start()
    {
        cameraPrincipal = Camera.main;

        ic = FindObjectOfType<ItensController>();
    }

    public void Update()
    {
        if(receiturarioLido && obituarioLido) cut.Necroterio();

        bool click = Input.GetMouseButtonDown(0);
        if(click)
        {
            if (ic.itemActive != -1)
            {
                GameObject go = ic.arrItens[ic.itemActive];

                Ray ray = cameraPrincipal.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity) )
                {
                    if (hit.collider.tag == "Ursinho" && go.tag == "Canivete")
                    {
                        Instantiate(prefabChave2, hit.transform.position, Quaternion.identity);
                        Instantiate(prefabUrsinhoCortado, hit.transform.position, Quaternion.identity);
                        Destroy(hit.collider.gameObject);
                    }

                    if(hit.collider.tag == "Sensor Cartão"  && go.tag == "Cartão")
                    {
                        door.AltState(false);
                    }
                }
            }
        }
    }

    public void GrabKey()
    {
        var variables = Variables.Object(gameObject);
        variables.Set("triggerActived", true); // Marca trigger como já ativo

        if(triggerPorta4 != null) triggerPorta4.SetActive(true); // Ativa trigger da porta 4

        le.AlterStateCanBlink(false); // Faz as luzes ficarem todas apagadas com excessão de uma
        le.ApagarLuzes(arrLuzes);
        le.LigarDesligControllers(false);

        audioSource.Stop(); // Para audio em loop das vozes

        door.AltState(false); // Destranca a porta 3

        confusion.DesativarEfeito(); // Desativa confusão
    }

    public void receituario() => receiturarioLido = true;
    public void obituario() => obituarioLido = true;
}
