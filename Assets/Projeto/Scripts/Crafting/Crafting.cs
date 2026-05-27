using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    [Header("Variáveis")]
    public List<GameObject> listObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RemoveItem(GameObject obj) => listObjects.Remove(obj);

    public void AddItem(GameObject obj) => listObjects.Add(obj);

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Fita" || other.gameObject.tag == "Parte Cartão") listObjects.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Fita" || other.gameObject.tag == "Parte Cartão") listObjects.Remove(other.gameObject);
    }
}
