using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour {
    [SerializeField] GameObject inventory;
    
    void Start() {
        inventory.SetActive(false);
    }

    void Update() {
        Inventory();
    }    

    void Inventory() {
        if(Input.GetKeyDown(KeyCode.E)) {
            inventory.SetActive(!inventory.activeSelf);
        }
    }
}
