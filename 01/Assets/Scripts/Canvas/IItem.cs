using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class IItem : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler {
    private Item item;
    
    private EnumVoxels voxelID;
    //private EnumItems itemID;
    private Image image;
    private string itemName;
    
    private int stack = 1;
    private TextMeshProUGUI textMeshPro;

    private Transform parentAfterDrag;
    private GameObject dragging;
    
    [SerializeField] private GameObject itemPrefab;

    private string header;
    private string content;
    private string id;

    private IInterface iInterface;
    private bool openMenu;

    private void Awake() {
        image = GetComponentInChildren<Image>();
        textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
        
        // Procure pelo GameObject "Dragging Item" para não ter que fazer isso pelo Inspector, até pq isso é um prefab que sera gerado inGame, então não da pra ficar puxando do Inspector
        dragging = GameObject.Find("Dragging Item");

        // Procure pelo GameObject "Interface Manager", e dentro dele procure pelo component (script) "IInterface"
        iInterface = GameObject.Find("Interface Manager").GetComponent<IInterface>();
    }
    
    private void Start() {
        
    }

    private void Update() {
        openMenu = iInterface.getOpenMenu;

        RefreshCount();
        TooltipUpdate();
    }

    public void InitialiseItem(Item newItem) {
        item = newItem;

        voxelID = newItem.voxelID;
        //itemID = newItem.itemID;
        image.sprite = newItem.sprite;
        itemName = newItem.itemName;
    }

    // Atualize o texto que mostra a quantidade de itens na pilha
    public void RefreshCount() {
        textMeshPro.text = stack.ToString();

        // Só exiba um texto se houver mais de 1 item na pilha
        bool textActive = stack > 1;
        textMeshPro.gameObject.SetActive(textActive);
    }

    public Item getItem {
        get {
            return item;
        }
    }

    public EnumVoxels getVoxelID {
        get {
            return voxelID;
        }
    }

    /*
    public EnumItems getItemID {
        get {
            return itemID;
        }
    }
    */

    public Image getImage {
        get {
            return image;
        }
    }

    public string getItemName {
        get {
            return itemName;
        }
    }

    public int getStack {
        get {
            return stack;
        }
        set {
            stack = value;
        }
    }

    public Transform getParentAfterDrag {
        get {
            return parentAfterDrag;
        }
        set {
            parentAfterDrag = value;
        }
    }

    public void OnPointerDown(PointerEventData eventData) {
        
        // Se não tiver nenhum item sendo arrastado
        if(dragging.transform.childCount == 0) {

            // Se eu clicar com o botão esquerdo do mouse sobre um item ou pilha de itens
            if(eventData.button == PointerEventData.InputButton.Left) {

                // Desative o raycast, para que possamos clicar sobre um slot
                image.raycastTarget = false;

                // Salve o slot de origem
                parentAfterDrag = transform.parent;

                // Pegue o item ou pilha de itens completa
                transform.SetParent(dragging.transform);
            }

            // Se eu clicar com o botão direito do mouse sobre um item ou pilha de itens
            if(eventData.button == PointerEventData.InputButton.Right) {
                
                // Se a pilha de itens for menor ou igual a 1
                if(stack <= 1) {

                    // Desative o raycast, para que possamos clicar sobre um slot
                    image.raycastTarget = false;

                    // Salve o slot de origem
                    parentAfterDrag = transform.parent;

                    // Pegue o item
                    transform.SetParent(dragging.transform);
                }   

                // Se não
                else {

                    // Crie um item para representar metade da pilha
                    GameObject itemObject = Instantiate(itemPrefab);

                    IItem item2 = itemObject.GetComponentInChildren<IItem>();
                    item2.InitialiseItem(item);

                    itemObject.name = item2.itemName;

                    // Desative o raycast, para que possamos clicar sobre um slot
                    item2.getImage.raycastTarget = false;

                    // Pegue o item
                    item2.parentAfterDrag = transform.parent;                    
                    item2.transform.SetParent(dragging.transform);

                    int result = stack / 2;
                    int remainder = stack % 2;
                    int valueRemaining = result + remainder;

                    // Exemplo, se temos 3 itens na pilha, vamos pegar 2 e deixar 1 no slot
                    
                    stack = result;

                    // Atualize o texto do TextMeshPro da pilha que esta no slot
                    RefreshCount();

                    item2.stack = valueRemaining;
                    
                    // Atualize o texto do TextMeshPro da pilha que esta sendo arrastada
                    item2.RefreshCount();
                }
            }
        }

        // Se não, se tiver um item sendo arrastado
        else if(dragging.transform.childCount == 1) {

            // Procure nos objetos filho do GameObject "Dragging Item" o component (script) "IItem"
            IItem iItem3 = dragging.GetComponentInChildren<IItem>();

            // Se os itens em um slot não estiver no maximo que a pilha permite
            if(stack < item.maxStack) {

                // Se eu clicar com o botão esquerdo sobre um item
                if(eventData.button == PointerEventData.InputButton.Left) {
                    if((stack + iItem3.stack) <= item.maxStack) {
                        
                        // Coloque o item ou pilha de itens completa no mesmo slot
                        stack += iItem3.stack;

                        // Atualize o texto do TextMeshPro da pilha que esta no slot
                        RefreshCount();

                        // Atualize o valor da pilha para zero
                        iItem3.stack = 0;

                        // Se a pilha de itens que estiver sendo arrastada for menor que zero
                        if(iItem3.stack <= 0) {

                            // Destrua a pilha de itens que esta sendo arrastada
                            Destroy(iItem3.gameObject);
                        }
                        
                        // Se não
                        else {

                            // Atualize o texto do TextMeshPro da pilha que esta sendo arrastada
                            iItem3.RefreshCount();
                        }

                        // Se não houver espaço para colocar todos os itens da pilha que esta sendo arrastada na pilha que esta no slot, devera colocar o maximo de itens possivel, e continuar com o restando sendo arrastado
                    }
                }

                // Se eu clicar com o botão direito sobre um item
                if(eventData.button == PointerEventData.InputButton.Right) {

                    // Acrecente um item a pilha de itens que esta no slot
                    stack++;

                    // Atualize o texto do TextMeshPro da pilha que esta no slot
                    RefreshCount();

                    // Subtraia um item da pilha de itens que esta sendo arratado
                    iItem3.stack--;

                    // Se a pilha de itens que estiver sendo arrastada for menor que zero
                    if(iItem3.stack <= 0) {

                        // Destrua a pilha de itens que esta sendo arrastada
                        Destroy(iItem3.gameObject);
                    }

                    // Se não
                    else {

                        // Atualize o texto do TextMeshPro da pilha que esta sendo arrastada
                        iItem3.RefreshCount();
                    }
                }                    
            }
        }
    }

    private void TooltipUpdate() {
        header = itemName;
        id = voxelID.ToString();
        //id = itemID.ToString();

        if(dragging.transform.childCount > 0) {
            IItem iItem3 = dragging.GetComponentInChildren<IItem>();

            // Se tiver um item sendo arrastado
            if(dragging.transform.childCount > 0) {

                // Esconda o Tooltip
                Tooltip.Hide();
            }
        }

        //if(dragging.transform.childCount > 0) {
            //Tooltip.Hide();
        //}

        // Se eu fechar o menu
        if(!openMenu) {

            // Esconda o Tooltip
            Tooltip.Hide();
        }
    }
    
    // Se eu colocar o cursor do mouse sobre um item
    public void OnPointerEnter(PointerEventData eventData) {

        // Exiba o Tooltip
        Tooltip.Show(header, content, id);
    }

    // Se eu retirar o cursor do mouse sobre um item
    public void OnPointerExit(PointerEventData eventData) {

        // Esconda o Tooltip
        Tooltip.Hide();
    }
}
