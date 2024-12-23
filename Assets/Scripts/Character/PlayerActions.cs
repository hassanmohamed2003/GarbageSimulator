using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    PlayerInput _Input;
    float _pickUpRange = 3f;
    CircleCollider2D circleCollider;
    private GameObject _trashObject;
    private GameObject _containerObject;
    List<GameObject> trashCollected = new List<GameObject>();
    private float collectedPlastic;
    private float collectedMetal;
    private float collectedGlass;
    public TextMeshProUGUI plasticText;
    public TextMeshProUGUI glassText;
    public TextMeshProUGUI metalText;
    public float CollectedPlastic { get => collectedPlastic; set => collectedPlastic = value; }
    public float CollectedMetal { get => collectedMetal; set => collectedMetal = value; }
    public float CollectedGlass { get => collectedGlass; set => collectedGlass = value; }

    private void Awake()
    {
        _Input = new PlayerInput();

        _Input.Gameplay.PickingUp.performed += onPickUp;
        _Input.Gameplay.Drop.performed += onDrop;
        circleCollider = GetComponent<CircleCollider2D>();
    }

    private void OnEnable()
    {
        _Input.Enable();
    }

    private void OnDisable()
    {
        _Input.Disable();

    }

    public void UpdateCounters()
    {
        Debug.Log(collectedPlastic + ": amount plastic");
        // Update the text of each TextMeshPro element
        plasticText.text = $"Plastic: {collectedPlastic}";
        glassText.text = $"Glass: {collectedGlass}";
        metalText.text = $"Metal: {collectedMetal}";

        _trashObject = null;
        _containerObject = null;
    }

    private void onDrop(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (_containerObject != null && _containerObject.TryGetComponent<Container>(out Container container))
        {
            switch (container.acceptedItem)
            {
                case ItemType.Plastic:
                    collectedPlastic = 0;
                    UpdateCounters();
                    break;
                case ItemType.Glass:
                    collectedGlass = 0;
                    UpdateCounters();
                    break;
                case ItemType.Metal:
                    collectedMetal = 0;
                    UpdateCounters();
                    break;
            }
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject);


        if (collision.TryGetComponent<Trash>(out Trash trash))
        {
            _trashObject = collision.gameObject;
            return;
        }
        else if (collision.TryGetComponent<Container>(out Container container))
        {
            _containerObject = collision.gameObject;
            return;
        }

        _trashObject = null;
        _containerObject = null;
    }

    private void onPickUp(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (_trashObject != null && _trashObject.TryGetComponent<Trash>(out Trash trash))
        {
            _trashObject.SetActive(false);
            switch (trash.itemType)
            {
                case ItemType.Plastic:
                    collectedPlastic++;
                    UpdateCounters();
                    break;
                case ItemType.Glass:
                    collectedGlass++;
                    UpdateCounters();
                    break;
                case ItemType.Metal:
                    collectedMetal++;
                    UpdateCounters();
                    break;
            }
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
