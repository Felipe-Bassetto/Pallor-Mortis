using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItensController : MonoBehaviour
{
    [Header("Itens")]
    [SerializeField] private GameObject slotAtivo1;
    [SerializeField] private GameObject slotAtivo2;
    [SerializeField] private RawImage item1;
    [SerializeField] private RawImage item2;

    public List<GameObject> arrItens = new List<GameObject>();
    public int itemActive;

    private GameObject prefabDrop;
    private LayerMask layerMask;

    [Header("Camera")]
    private Camera cameraPrincipal;

    [Header("Scripts")]
    [SerializeField] private Crafting craft;

    void Start()
    {
        cameraPrincipal = Camera.main;
        layerMask = LayerMask.GetMask("Dropable");
    }

    void Update()
    {
        switch (itemActive)
        {
            case 0:
                slotAtivo1.SetActive(true);
                slotAtivo2.SetActive(false);
                break;
            case 1:
                slotAtivo1.SetActive(false);
                slotAtivo2.SetActive(true);
                break;
            case -1:
                slotAtivo1.SetActive(false);
                slotAtivo2.SetActive(false);
                break;
        }

        bool keyQHolding = Input.GetKey(KeyCode.Q) && itemActive != -1;
        bool keyQReleased = Input.GetKeyUp(KeyCode.Q);

        if (keyQHolding)
        {
            dropItem();
        }

        if (keyQReleased && prefabDrop != null)
        {
            Destroy(prefabDrop);
            prefabDrop = null;
        }

        bool change = Input.GetKeyDown(KeyCode.Alpha1);
        if(change)
        {
            changeItem();
        }
    }

    public int addItem(GameObject item)
    {
        int returnNum = -1;
        
        for(int i = 0; i < 2; i++)
        {
            if(arrItens[i] == null)
            {
                if(itemActive >= 0)
                {
                    arrItens[itemActive].SetActive(false);
                }
                arrItens[i] = item;
                returnNum = i;
                itemActive = returnNum;
                break;
            }
        }
        
        return returnNum;
    }

    public void dropItem()
    {
        if(itemActive == -1) return;

        GameObject objActive = arrItens[itemActive];

        if (prefabDrop == null)
        {
            GrabItem grab = objActive.GetComponentInChildren<GrabItem>();
            prefabDrop = Instantiate(grab.prefabDrop);
        }

        Ray ray = cameraPrincipal.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)) prefabDrop.transform.position = hit.point;

        if (Input.GetMouseButtonDown(0))
        {
            objActive.transform.SetParent(null);
            objActive.transform.position = prefabDrop.transform.position;

            if (hit.collider.tag == "Crafting") craft.AddItem(objActive);

            objActive = null;
            
            switch(itemActive)
            {
                case 0:
                    item1.texture = null; 
                    item1.gameObject.SetActive(false);
                    slotAtivo1.SetActive(false);
                    break;
                case 1:
                    item2.texture = null;
                    item2.gameObject.SetActive(false);
                    slotAtivo2.SetActive(false);
                    break;
            }

            Destroy(prefabDrop);
            prefabDrop = null;

            arrItens[itemActive] = null;
            itemActive = -1;
        }
    }

    public void changeItem()
    {
        for(int i = 0; i < 2; i++)
        {
            if(i == itemActive) continue;
            else if(arrItens[i] != null)
            {                
                if(itemActive != -1)
                {
                    arrItens[itemActive].SetActive(false);
                }
                if(itemActive == 1)
                {
                    arrItens[itemActive].SetActive(false);
                    itemActive = -1;
                }
                else
                {
                    arrItens[i].SetActive(true);
                    itemActive = i;
                }
                break;                
            }
            else
            {
                if(itemActive != -1)
                {
                    arrItens[itemActive].SetActive(false);
                }
                itemActive = -1;
            }
        }
    }

    public void DestroyKey()
    {
        GameObject objActive = arrItens[itemActive];
        Destroy(objActive);
        arrItens[itemActive] = null;
        itemActive = -1;
    }
}