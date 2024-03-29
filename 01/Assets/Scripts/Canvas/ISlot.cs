using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ISlot : MonoBehaviour, IPointerDownHandler {
    private GameObject dragging;

    private IItem item;

    [SerializeField] private GameObject itemPrefab;

    private void Awake() {
        // Procure pelo GameObject "Dragging Item" para não ter que fazer isso pelo Inspector
        dragging = GameObject.Find("Dragging Item");
    }

    private void Start() {
        
    }

    private void Update() {
        
    }

    public void OnPointerDown(PointerEventData eventData) {
        // Procure nos objetos filho do GameObject "Dragging Item" o component (script) "IItem"
        item = dragging.GetComponentInChildren<IItem>();

        // Se tiver um item sendo arrastado
        if(dragging.transform.childCount == 1) {
            // Se eu clicar com o botão esquerdo sobre um slot
            if(eventData.button == PointerEventData.InputButton.Left) {
                // Se o slot estiver vazio, coloque o item sendo arrastado no slot
                if(transform.childCount == 0) {
                    // Reative o Raycast do item para que possamos clicar nele novamente
                    item.getImage.raycastTarget = true;

                    // Salve um novo slot de origem
                    item.getParentAfterDrag = transform;

                    // Mova o item para o slot de origem
                    item.transform.SetParent(item.getParentAfterDrag);
                }
            }
            // Se eu clicar com o botão direito sobre um slot
            if(eventData.button == PointerEventData.InputButton.Right) {
                // ???
                // Se tiver menos de 2 itens sendo arrastados
                if(item.getStack < 2) {
                    // Reative o Raycast do item para que possamos clicar nele novamente
                    item.getImage.raycastTarget = true;

                    // Salve um novo slot de origem
                    item.getParentAfterDrag = transform;

                    // Mova o item para o slot de origem
                    item.transform.SetParent(item.getParentAfterDrag);
                }
                // Se não, se tiver 2 ou mais itens sendo arrastados
                else {
                    // Crie um item para representar metade da pilha
                    GameObject itemObject = Instantiate(itemPrefab);

                    // Procure nos objetos filho do GameObject "Dragging Item" o component (script) "IItem"
                    IItem item2 = itemObject.GetComponentInChildren<IItem>();

                    // De as informações iniciais do Item (ID, sprite, nome)
                    item2.InitialiseItem(item.getItem);

                    // De um nome para o GameObject apartir o nome do Item
                    itemObject.name = item2.getItemName;

                    // Salve um novo slot de origem para a metade que foi criada
                    item2.transform.SetParent(transform);

                    // Não to lembrada pra que serve, mas acho que era algum bug que quando você colocava o item no slot, o item diminuia de tamanho
                    RectTransform rectTransform = item2.GetComponent<RectTransform>();
                    rectTransform.localScale = transform.localScale;

                    // Subtraia um item da pilha de itens que esta sendo arratado
                    item.getStack--;

                    // Atualize o texto do TextMeshPro da pilha que esta sendo arrastada
                    item.RefreshCount();

                    // Ta com um bugzinho quando eu coloco 1 unico item em varios slots seguidos
                }
            }
        }
    }
}
