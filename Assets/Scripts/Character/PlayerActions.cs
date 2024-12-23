using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    PlayerInput _Input;
    float _pickUpRange = 3f;
    CircleCollider2D circleCollider;
    GameObject trashObject;
    List<GameObject> trashCollected = new List<GameObject>();

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

    private void onDrop(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject);
        if(collision.gameObject.tag == "Trash")
        {
            trashObject = collision.gameObject;
        }
    }

    private void onPickUp(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if(trashObject != null)
        {
            trashCollected.Add(trashObject);
            trashObject.SetActive(false); // Deactivate the object instead of destroying
            Debug.Log("Picked up: " + trashObject.name);
            trashObject = null; // Reset the reference
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
