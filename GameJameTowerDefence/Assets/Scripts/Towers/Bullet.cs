using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{

    public float speed = 15f;
    public Transform target;
    public int damage = 0;
    public int AOEdamage = 8;
    public string type;

    public GameObject AOE;

    // Use this for initialization
    void Start()
    {
        if (target == null)
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, target.position, speed * Time.deltaTime);
        }
        else
        {
            // Our enemy went away!
            Destroy(gameObject);
            return;
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
        if (col.gameObject.tag != "Enemy")
        {
            Destroy(this.gameObject);
        }
    }


}