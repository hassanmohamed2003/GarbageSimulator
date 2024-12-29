using System.Collections;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{

    [SerializeField] private Transform _player;
    [SerializeField, Range(0, 100)] private float _minimumDistance;
    [SerializeField] private float _moveSpeed;
    private CircleCollider2D _circleCollider;
    private bool _followPlayer;
    private Rigidbody2D _rb;
    [SerializeField] private float _waitTime = 3;
    [SerializeField] private GameObject _pointA;
    [SerializeField] private GameObject _pointB;
    private bool _isVisitingA = true;
    Vector3 _visitingPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _circleCollider = GetComponent<CircleCollider2D>();
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_isVisitingA)
        {
            _visitingPosition = _pointA.transform.position;
        }
        else
        {
            _visitingPosition = _pointB.transform.position;
        }

        transform.position = Vector2.MoveTowards(this.transform.position, _visitingPosition, _moveSpeed * Time.deltaTime);
        checkPosition();
    }

    private void checkPosition()
    {
        Debug.Log(transform.position + ": transform me");
        Debug.Log(_pointA.transform.position + ": transform me");
        float distanceToTarget = Vector3.Distance(transform.position, _visitingPosition);
        if (distanceToTarget < 1.5f)
        {
            StartCoroutine(waitOnPoint());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

    }

    private IEnumerator waitOnPoint()
    {
        yield return new WaitForSeconds(_waitTime);
        _isVisitingA = !_isVisitingA;
    }
}
