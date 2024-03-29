using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IInterface : MonoBehaviour {
    private bool isFullScreen;

    [SerializeField] private GameObject gameMenu;
    private bool isPaused;

    [SerializeField] private Button backToGame;
    [SerializeField] private Button saveAndQuitToTitle;
    
    [Space(20)]
    [SerializeField] private GameObject inventory;
    private bool openMenu;

    [SerializeField] private GameObject justEnoughItems;

    private void Awake() {
        backToGame.onClick.AddListener(BackToGame);
        saveAndQuitToTitle.onClick.AddListener(SaveAndQuitToTitle);
    }   

    private void Start() {  
        gameMenu.SetActive(false);
        inventory.SetActive(false);
    }

    private void Update() {
        FullScreenInput();
        GameMenuInput();
        InventoryInput();
    }

    private void FullScreenInput() {
        if(Input.GetKeyDown(KeyCode.F11)) {
            isFullScreen = !isFullScreen;
            Screen.fullScreen = isFullScreen;
        }
    }

    private void GameMenuInput() {
        if(!openMenu) {
            if(Input.GetButtonDown("Escape")) {            
                isPaused = !isPaused;
                gameMenu.SetActive(!gameMenu.activeSelf);
            }
        }
    }

    public bool getIsPaused {
        get {
            return isPaused;
        }
    }

    private void BackToGame() {
        isPaused = false;
        gameMenu.SetActive(false);
    }

    private void SaveAndQuitToTitle() {
        //SceneManager.LoadScene("Main Menu");
    }

    private void InventoryInput() {
        if(!isPaused) {
            if(Input.GetKeyDown(KeyCode.E)) {
                openMenu = !openMenu;
                inventory.SetActive(!inventory.activeSelf);
            }            
            if(Input.GetButtonDown("Escape")) {
                openMenu = false;                
                inventory.SetActive(false);
            }

            if(inventory.activeSelf == true) {
                if(Input.GetKeyDown(KeyCode.O)) {
                    justEnoughItems.SetActive(!justEnoughItems.activeSelf);
                }
            }
        }
    }

    public bool getOpenMenu {
        get {
            return openMenu;
        }
    }
}
