using UnityEngine;

public class AIChase : MonoBehaviour
{
    public GameObject player;
    public GameObject container;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _minimumDistance;

    private float _distance;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if(_minimumDistance >= _distance)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, _speed * Time.deltaTime);
        }

        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }
}
