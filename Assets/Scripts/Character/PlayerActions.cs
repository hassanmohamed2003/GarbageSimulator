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

    private float _totalItems;
    private float _totalPlastic;
    private float _totalMetal;
    private float _totalGlass;

    private float _collectedPlastic;
    private float _collectedMetal;
    private float _collectedGlass;
    public TextMeshProUGUI plasticText;
    public TextMeshProUGUI glassText;
    public TextMeshProUGUI metalText;
    public TextMeshProUGUI totalText;
    [SerializeField] private GameManager gameManager;
    public float CollectedPlastic { get => _collectedPlastic; set => _collectedPlastic = value; }
    public float CollectedMetal { get => _collectedMetal; set => _collectedMetal = value; }
    public float CollectedGlass { get => _collectedGlass; set => _collectedGlass = value; }

    private void Awake()
    {
        _Input = new PlayerInput();

        _Input.Gameplay.PickingUp.performed += onPickUp;
        _Input.Gameplay.Drop.performed += onDrop;
        circleCollider = GetComponent<CircleCollider2D>();

        _totalPlastic = gameManager.levelRequirements.Plastic;
        _totalGlass = gameManager.levelRequirements.Glass;
        _totalMetal = gameManager.levelRequirements.Metal;
        _totalItems = gameManager.levelRequirements.Total;
        UpdateCounters();
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
        Debug.Log(_collectedPlastic + ": amount plastic");
        // Update the text of each TextMeshPro element
        plasticText.text = $"Plastic: {_collectedPlastic} of the {_totalPlastic}";
        glassText.text = $"Glass: {_collectedGlass} of the {_totalGlass}";
        metalText.text = $"Metal: {_collectedMetal} of the {_totalMetal}";
        totalText.text = $"Left: {_totalItems}";
        _trashObject = null;
        _containerObject = null;

        if(_totalItems == 0)
        {
            totalText.text = $"Completed!";
        }
    }

    private void onDrop(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (_containerObject != null && _containerObject.TryGetComponent<Container>(out Container container))
        {
            switch (container.acceptedItem)
            {
                case ItemType.Plastic:
                    _totalPlastic -= _collectedPlastic;
                    _collectedPlastic = 0;
                    UpdateCounters();
                    break;
                case ItemType.Glass:
                    _totalGlass -= _collectedGlass;
                    _collectedGlass = 0;
                    UpdateCounters();
                    break;
                case ItemType.Metal:
                    _totalMetal -= _collectedMetal;
                    _collectedMetal = 0;
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
            _totalItems--;
            _trashObject.SetActive(false);
            switch (trash.itemType)
            {
                case ItemType.Plastic:
                    _collectedPlastic++;
                    UpdateCounters();
                    break;
                case ItemType.Glass:
                    _collectedGlass++;
                    UpdateCounters();
                    break;
                case ItemType.Metal:
                    _collectedMetal++;
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
