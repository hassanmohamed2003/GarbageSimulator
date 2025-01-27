using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class AIChase : MonoBehaviour
{
    public GameObject player;
    private CircleCollider2D detectionRange;

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
        detectionRange = gameObject.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Debug.Log(_playerDetected);

        if (_playerDetected)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, _speed * Time.deltaTime);
        }
        else
        {
            Roaming();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.GetComponent<PlayerActions>())
        {
            Vector3 moveDirection = (transform.position - collision.transform.position).normalized;

            transform.position = Vector3.MoveTowards(transform.position, transform.position + moveDirection, _speed * Time.deltaTime);
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
            if (Vector2.Distance(transform.position, player.transform.position) > detectionRange.radius)
            {
                _playerDetected = false;
            }
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
