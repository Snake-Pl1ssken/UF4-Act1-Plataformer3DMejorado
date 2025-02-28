using UnityEngine;

public class Projectile : MonoBehaviour
{
    public enum SpeedMode
    { 
        ApplyOnStart,
        ApplyAllTheTime,
    }
    [SerializeField] SpeedMode speedMode = SpeedMode.ApplyOnStart;
    [SerializeField] Vector3 speed = Vector3.forward;
    [SerializeField] float lifeTime = 10f;
    [SerializeField] GameObject[] resultsPrefab;
    [SerializeField] bool destroyOnCollision;
    [SerializeField] bool instantiateResultsOnColision = true;
    [SerializeField] bool instantiateResultsOnLifeTimeEnd = false;
    
    Rigidbody rigidbody;
    float consumedLifeTime;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        consumedLifeTime = 0f;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (speedMode == SpeedMode.ApplyOnStart)
        { 
            rigidbody.linearVelocity = transform.TransformDirection(speed);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (speedMode == SpeedMode.ApplyAllTheTime)
        { 
            rigidbody.linearVelocity = transform.TransformDirection(speed);
        }
        consumedLifeTime = Time.deltaTime;
        if(consumedLifeTime >= lifeTime)
        {
            Destroy(gameObject);
            if(instantiateResultsOnLifeTimeEnd)
            {
                InstantiateResults();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (destroyOnCollision)
        {
            Destroy(gameObject);
            if (instantiateResultsOnColision)
            {
                InstantiateResults();
            }
        }
    }

    bool resultsAllreadyInstantiated = false;

    void InstantiateResults()
    {
        if (!resultsAllreadyInstantiated)
        { 
            resultsAllreadyInstantiated = true;
            foreach (GameObject result in resultsPrefab)
            {
                Instantiate(result, transform.position, transform.rotation);
            }
        }
    }
}
