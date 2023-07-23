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

            // Mova o item para o slot de origem
            RectTransform rectTransform = item.GetComponent<RectTransform>();

            // Não to lembrada pra que serve, mas acho que era algum bug que quando você colocava o item no slot, o item diminuia de tamanho
            rectTransform.localScale = transform.localScale;

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
