using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class ItemEvents : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject triggerPorta4;
    public GameObject[] arrLuzes;

    [Header("Scripts")]
    [SerializeField] private LightEvents le;
    [SerializeField] private PlayerPOV POV;
    [SerializeField] private DoorInteraction door;
    [SerializeField] private GerenciadorConfusao confusion;

    [Header("Audio")]
    public AudioSource audioSource;

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
}
