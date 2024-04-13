using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    private Rigidbody rigidbody;
    private MeshRenderer meshrenderer;
    private Collider collider;
    private ObstacleController obstacleController;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        meshrenderer = GetComponent<MeshRenderer>();
        obstacleController = transform.parent.GetComponent<ObstacleController>();
         collider = GetComponent<Collider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Shatter()
    {
        rigidbody.isKinematic = false;
        collider.enabled = false;

        Vector3 forcePoint=transform.parent.position;
        float parentXpos = transform.parent.position.x;
        float xPos=meshrenderer.bounds.center.x;

        Vector3 subbdir = (parentXpos - xPos < 0) ? Vector3.right : Vector3.left;
        Vector3 dir = (Vector3.up * 1.5f + subbdir).normalized;
        float force = Random.Range(20, 35);
        float torqe = Random.Range(110, 180);
        rigidbody.AddForceAtPosition(dir * force, forcePoint, ForceMode.Impulse);
        rigidbody.AddTorque(Vector3.left * torqe);
        rigidbody.velocity = Vector3.down;
    }
}
