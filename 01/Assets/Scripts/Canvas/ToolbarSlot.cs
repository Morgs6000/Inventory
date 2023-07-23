using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolbarSlot : MonoBehaviour {
    [SerializeField] private GameObject slot;
    private GameObject item;
    
    private void Start() {
        
    }

    private void Update() {
        DuplicarItem();
    }

    public void DuplicarItem() {
        if(transform.childCount < 1) {
            if(slot.transform.childCount > 0) {
                item = Instantiate(slot.transform.GetChild(0).gameObject);

                item.transform.SetParent(transform);

                // Corrigi um bug que quando você pega metade da pilha, o item diminui de tamanho
                RectTransform rectTransform = item.GetComponent<RectTransform>();
                rectTransform.localScale = transform.localScale;
            } 
        }
        if(transform.childCount > 0) {
            if(slot.transform.childCount < 1) {
                Destroy(item);
            }
        }
    }
}
