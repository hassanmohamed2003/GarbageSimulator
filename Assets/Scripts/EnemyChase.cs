using System.Collections;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public GameObject player;
    private CircleCollider2D detectionRange;
    private Animator animator;

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

    [SerializeField]
    private float slowDownDuration = 3f;
    [SerializeField]
    private float slowDownSpeed = 1f;

    private bool isSlowedDown = false;

    [SerializeField]
    private float coolDownTime = 2f;
    private float lastHitTime = -Mathf.Infinity;

    void Start()
    {
        animator = GetComponent<Animator>();
        _isRoamingRight = true;
        currentPosition = transform.position.x;
        detectionRange = gameObject.GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        _distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();

        float horizontal = direction.x;
        float vertical = direction.y;

        if (_playerDetected && !isSlowedDown)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, _speed * Time.deltaTime);
            animator.SetFloat("Horizontal", horizontal);
            animator.SetFloat("Vertical", vertical);
            animator.SetFloat("Speed", _speed);
        }
        else if (_playerDetected && isSlowedDown)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, slowDownSpeed * Time.deltaTime);
            animator.SetFloat("Horizontal", horizontal);
            animator.SetFloat("Vertical", vertical);
            animator.SetFloat("Speed", slowDownSpeed);
        }
        else
        {
            Roaming();
            animator.SetFloat("Horizontal", _isRoamingRight ? 1 : -1);
            animator.SetFloat("Vertical", 0);
            animator.SetFloat("Speed", _speed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerActions>() && Time.time >= lastHitTime + coolDownTime)
        {
            StartCoroutine(SlowDown());
            lastHitTime = Time.time;
        }
        else
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

    private IEnumerator SlowDown()
    {
        isSlowedDown = true;
        float originalSpeed = _speed;
        _speed = slowDownSpeed;

        yield return new WaitForSeconds(slowDownDuration);

        _speed = originalSpeed;
        isSlowedDown = false;
    }
}
