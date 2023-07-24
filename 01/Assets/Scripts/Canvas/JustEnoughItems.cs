using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JustEnoughItems : MonoBehaviour {
    private bool result;
    [SerializeField] private IInventory iInventory;
    [SerializeField] private Item[] itemsToPickup;

    [SerializeField] private TextMeshProUGUI textMeshPro;

    private void Start() {
        
    }

    private void Update() {
        
    }

    public void PickUpItem(int id) {
        result = iInventory.AddItem(itemsToPickup[id]);

        if(result) {
            //Debug.Log("Item type added: " + voxelType);
        }
        else {
            //Debug.Log("ITEM NOT ADDED");

            WarningMensage();
        }
    }

    private void WarningMensage() {
        textMeshPro.text = "Inventario cheio";

        ColorUtility.TryParseHtmlString("#FC5454", out Color color);
        textMeshPro.color = color;

        textMeshPro.gameObject.SetActive(true);

        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut() {
        float delay = 1.0f;

        yield return new WaitForSeconds(delay);

        float fadeTime = 1.0f;
        float elapsedTime = 0.0f;

        Color startColor = textMeshPro.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        while (elapsedTime < fadeTime) {
            float alpha = Mathf.Lerp(startColor.a, endColor.a, elapsedTime / fadeTime);
            textMeshPro.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        textMeshPro.color = endColor;

        textMeshPro.gameObject.SetActive(false);
    }
}
