using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{

    public float speed = 15f;
    public Transform target;
    public int damage = 0;
    public float radius = 0;
    public int AOEdamage = 8;
    public string type;

    public GameObject AOE;

    // Use this for initialization
    void Start()
    {
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
            if (type == "AOE")
            {
                GameObject Aoe = Instantiate(AOE, transform.position, transform.rotation) as GameObject;
            }

            Destroy(this.gameObject);
        }
    }


}