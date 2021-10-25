using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damg;

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.GetComponent<EnemyHealthSystem>() == true)
        {
            collision.gameObject.GetComponent<EnemyHealthSystem>().DealDamage(damg);
            Destroy(this.gameObject);
        }
    }
}
