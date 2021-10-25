using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public float damg;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<HealthSystem>() == true)
        {
            collision.gameObject.GetComponent<HealthSystem>().DealDamage(damg);
           Destroy(this.gameObject);
        }
    }
}
