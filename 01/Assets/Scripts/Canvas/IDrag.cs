using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IDrag : MonoBehaviour {
    private IItem item;

    private IInterface iInterface;

    private void Awake() {
        // Procure pelo GameObject "Interface Manager", e dentro dele procure pelo component (script) "IInterface"
        iInterface = GameObject.Find("Interface Manager").GetComponent<IInterface>();
    }

    private void Start() {
        
    }

    private void Update() {
        Drag();
    }

    private void Drag() {
        bool openMenu = iInterface.getOpenMenu;

        // Se tiver um item sendo arrastado
        if(transform.childCount == 1) {
            // Procure nos objetos filho deste GameObject o component (script) "IItem"
            item = GetComponentInChildren<IItem>();

            // Corrigi um bug que quando você pega metade da pilha, o item diminui de tamanho
            RectTransform rectTransform = item.GetComponent<RectTransform>();            
            rectTransform.localScale = transform.localScale;
            
            // Corrigi um bug onde a imagem do item não aparece
            rectTransform.sizeDelta = new Vector2(16, 16);

            // Mova o item junto com o cursor do mouse
            item.transform.position = Input.mousePosition;

            // Se eu clicar com o botão esquerdo do mouse, ou fechar o menu
            if(Input.GetMouseButtonDown(0) || !openMenu) {
                if(!EventSystem.current.IsPointerOverGameObject()) {
                    // Reative o Raycast do item para que possamos clicar nele novamente
                    item.getImage.raycastTarget = true;

                    // Devolva o item ao slot de origem
                    item.transform.SetParent(item.getParentAfterDrag);
                }
            }
        }
    }
}
