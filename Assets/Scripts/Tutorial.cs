using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public TextMeshProUGUI pickUpText;
    public TextMeshProUGUI dropText;
    public TextMeshProUGUI bagText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pickUpText.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showPickUpText()
    {
        pickUpText.gameObject.SetActive(true);
    }

    public void showDropText()
    {
        pickUpText.gameObject.SetActive(false);
        dropText.gameObject.SetActive(true);
    }

    public void removeDropText()
    {
        dropText.gameObject.SetActive(false);
    }

    public void showBagText()
    {
        pickUpText.gameObject.SetActive(false);
        dropText.gameObject.SetActive(false);
        bagText.gameObject.SetActive(true);
    }

    public void removeBagText()
    {
        dropText.gameObject.SetActive(false);
        bagText.gameObject.SetActive(false);
    }
}
