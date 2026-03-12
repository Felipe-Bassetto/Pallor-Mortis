using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItensController : MonoBehaviour
{
    [Header("Itens")]
    public List<GameObject> arrItens = new List<GameObject>();

    private GameObject test;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        /*If(Input.GetKeyDown(KeyCode.Q))
        {
            dropItem();
        }*/
    }

    public void addItem(GameObject item)
    {
        int qtdArr = arrItens.Count;
        if(qtdArr < 2)
        {
            arrItens.Add(item);
            test = item;
        }
    }

    public void dropItem()
    {
        int qtdArr = arrItens.Count;
        if(qtdArr > 0)
        {
            arrItens.Remove(test);
        }
    }
}
