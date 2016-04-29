using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{

    public float speed = 15f;
    public Transform target;
    public int damage = 0;
    public float radius = 0;

    // Use this for initialization
    void Start()
    {
        Debug.Log("Bullet is here");
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            // Our enemy went away!
            Destroy(gameObject);
            return;
        }


        Vector3 dir = target.position - this.transform.localPosition;

        float distThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distThisFrame)
        {
            // We reached the node
           // DoBulletHit();
        }
        else
        {
            transform.Translate(dir.normalized * distThisFrame, Space.World);
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, Time.deltaTime * 5);
        }

    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Debug.Log("BULLET HIT ENEMY");
            Destroy(this.gameObject);
        }
    }

 
}