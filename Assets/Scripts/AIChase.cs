using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class AIChase : MonoBehaviour
{
    public GameObject player;
    public CircleCollider2D detectionRange;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _minimumDistance;

    private float _distance;

    [SerializeField]
    private float _roamingDistance;
    private bool _isRoamingRight;
    private float currentPosition;
    private bool _playerDetected;

    void Start()
    {
        _isRoamingRight = true;
        currentPosition = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        _distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (_playerDetected)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, _speed * Time.deltaTime);
        }
        else
        {
            Roaming();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent("PlayerActions"))
        {

            _playerDetected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent("PlayerActions"))
        {
            _playerDetected = false;
        }
    }
    void Roaming()
    {
        float moveDirection = _isRoamingRight ? 1 : -1;


        transform.position = new Vector3(transform.position.x + moveDirection * _speed * Time.deltaTime, transform.position.y, transform.position.z);

        if (transform.position.x > currentPosition + _roamingDistance)
        {
            _isRoamingRight = false;
            currentPosition = transform.position.x;
        }
        else if (transform.position.x < currentPosition - _roamingDistance)
        {
            _isRoamingRight = true;
            currentPosition = transform.position.x;
        }
    }
}
