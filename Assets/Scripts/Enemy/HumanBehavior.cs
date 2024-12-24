using UnityEngine;

public class HumanBehavior : MonoBehaviour
{

    [SerializeField] private Transform _player;
    [SerializeField, Range(0, 100)] private float _minimumDistance;
    [SerializeField] private float _moveSpeed;
    private CircleCollider2D _circleCollider;
    private bool _followPlayer;
    private Rigidbody2D _rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _circleCollider = GetComponent<CircleCollider2D>();
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        while (_followPlayer)
        {
            Vector3 _direction = transform.position - _player.position;
            _rb.AddForce(_direction * _moveSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(transform.up * _moveSpeed * Time.deltaTime);
        if(collision.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement))
        {
            _followPlayer = true;
            // Move the object towards the player if the trigger is activated
            transform.position = Vector2.MoveTowards(transform.position, collision.transform.position, _moveSpeed);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement))
        {
            _followPlayer = false;
            // Move the object towards the player if the trigger is activated
        }
    }
}
