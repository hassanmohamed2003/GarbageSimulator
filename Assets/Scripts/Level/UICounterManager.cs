using TMPro;
using UnityEngine;

public class UICounterManager : MonoBehaviour
{

    public TextMeshProUGUI plasticText;
    public TextMeshProUGUI glassText;
    public TextMeshProUGUI metalText;
    public TextMeshProUGUI limitText;
    public TextMeshProUGUI totalLeftText;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCounters(
        float collectedPlastic,
        float collectedGlass,
        float collectedMetal,
        float collectionLimitCounter,
        float totalItems)
    {
        UpdatePlastic(collectedPlastic);
        UpdateMetal(collectedMetal);
        UpdateGlass(collectedGlass);
        UpdateLimit(collectionLimitCounter);
        UpdateTotalLeft(totalItems);

    }
    private void UpdatePlastic(float collectedPlastic)
    {
        plasticText.text = $"<sprite=2>: {collectedPlastic}";
    }
    private void UpdateMetal(float collectedMetal)
    {
        metalText.text = $"<sprite=1>: {collectedMetal}";
    }
    private void UpdateGlass(float collectedGlass)
    {
        glassText.text = $"<sprite=0>: {collectedGlass}";
    }
    private void UpdateLimit(float collectionLimitCounter)
    {
        limitText.text = $"Space left: {collectionLimitCounter}";

        if (collectionLimitCounter == 0)
        {
            limitText.text = $"Bag Full!";
        }
    }
    private void UpdateTotalLeft(float totalItems)
    {
        totalLeftText.text = $"Trash Left: {totalItems}";
    }

    public void CompletedText()
    {
        limitText.text = $"Completed!";
    }
}
