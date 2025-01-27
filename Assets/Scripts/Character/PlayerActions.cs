using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.Rendering.DebugUI;

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
    private float _collectionLimit;
    private float _collectionLimitCounter;
    public UnityEvent onCompleted;

    private bool firstPickUp = true;
    private bool firstDrop = true;
    private bool firstBagFull = true;
    private bool bagTextShowing = false;
    public UnityEvent onFirstPickup;
    public UnityEvent onFirstDrop;
    public UnityEvent onFirstBagFull;
    public UnityEvent removeBagText;

    public TextMeshProUGUI plasticText;
    public TextMeshProUGUI glassText;
    public TextMeshProUGUI metalText;
    public TextMeshProUGUI limitText;
    public TextMeshProUGUI totalLeftText;
    [SerializeField] private GameManager gameManager;
    
    public GameObject pickUpVFX; 
    public AudioClip dropSound;  
    private AudioSource audioSource;
    
    public float CollectedPlastic { get => _collectedPlastic; set => _collectedPlastic = value; }
    public float CollectedMetal { get => _collectedMetal; set => _collectedMetal = value; }
    public float CollectedGlass { get => _collectedGlass; set => _collectedGlass = value; }

    private void Awake()
    {
        _Input = new PlayerInput();

        _Input.Gameplay.PickingUp.performed += onPickUp;
        _Input.Gameplay.Drop.performed += onDrop;
        circleCollider = GetComponent<CircleCollider2D>();
        
        audioSource = GetComponent<AudioSource>();

        _totalPlastic = gameManager.levelRequirements.Plastic;
        _totalGlass = gameManager.levelRequirements.Glass;
        _totalMetal = gameManager.levelRequirements.Metal;
        _totalItems = gameManager.levelRequirements.Total;
        _collectionLimit = gameManager.levelRequirements.CollectLimit;
        _collectionLimitCounter = gameManager.levelRequirements.CollectLimit;

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
        plasticText.text = $"<sprite=2>: {_collectedPlastic}";
        glassText.text = $"<sprite=0>: {_collectedGlass}";
        metalText.text = $"<sprite=1>: {_collectedMetal}";
        limitText.text = $"Space left: {_collectionLimitCounter}";

        totalLeftText.text = $"Trash Left:{_totalItems}";
        if (_collectionLimitCounter == 0)
        {
            limitText.text = $"Bag Full!";
        }

        _trashObject = null;
        _containerObject = null;

        bool hasNoItemsCollected = _collectedGlass == 0 && _collectedPlastic == 0 && _collectedMetal == 0;
        if(_totalItems == 0 && hasNoItemsCollected)
        {
            limitText.text = $"Completed!";
            onCompleted.Invoke();
        }
    }

    private void onDrop(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {


        if (_containerObject != null && _containerObject.TryGetComponent<Container>(out Container container))
        {
            if (firstDrop)
            {
                firstDrop = false;
                onFirstDrop.Invoke();
            }
            
            if (dropSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(dropSound);
            }

            switch (container.acceptedItem)
            {
                case ItemType.Plastic:
                    emptyBag(_collectedPlastic);
                    _totalPlastic -= _collectedPlastic;
                    _collectedPlastic = 0;
                    UpdateCounters();
                    break;
                case ItemType.Glass:
                    emptyBag(_collectedGlass);
                    _totalGlass -= _collectedGlass;
                    _collectedGlass = 0;
                    UpdateCounters();
                    break;
                case ItemType.Metal:
                    emptyBag(_collectedMetal);
                    _totalMetal -= _collectedMetal;
                    _collectedMetal = 0;
                    UpdateCounters();
                    break;
            }
        }


    }

    private void emptyBag(float amount)
    {

        if (bagTextShowing)
        {
            bagTextShowing = false;
            removeBagText.Invoke();
        }

        _collectionLimitCounter = Mathf.Clamp(_collectionLimitCounter + amount, 0, gameManager.levelRequirements.CollectLimit);
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

        if (_collectedPlastic + _collectedMetal + _collectedGlass >= _collectionLimit)
        {
            if (firstBagFull)
            {
                firstBagFull = false;
                bagTextShowing = true;
                onFirstBagFull.Invoke();
            }

            return;
        }

        if (_trashObject != null && _trashObject.TryGetComponent<Trash>(out Trash trash))
        {
            if (firstPickUp)
            {
                firstPickUp = false;
                onFirstPickup.Invoke();
            }
            
            
            if (pickUpVFX != null)
            {
                Instantiate(pickUpVFX, _trashObject.transform.position, Quaternion.identity);
            }

            _totalItems--;
            _trashObject.SetActive(false);
            switch (trash.itemType)
            {
                case ItemType.Plastic:
                    _collectedPlastic++;
                    _collectionLimitCounter--;
                    UpdateCounters();
                    break;
                case ItemType.Glass:
                    _collectedGlass++;
                    _collectionLimitCounter--;
                    UpdateCounters();
                    break;
                case ItemType.Metal:
                    _collectedMetal++;
                    _collectionLimitCounter--;
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
