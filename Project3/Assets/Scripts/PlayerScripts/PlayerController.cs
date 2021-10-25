using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 10.0f;
    public float rotaionSpeed = 50.0f;
    Animator animator;
    GameObject instantiatedProjectile;
    [SerializeField] private GameObject target;
    public int bulletsFired = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        animator.SetBool("Idling", true);
    }

    // Update is called once per frame
    void Update()
    {
        float translation = Input.GetAxis("Vertical") * speed;
        float rotaion = Input.GetAxis("Horizontal") * rotaionSpeed;
        translation *= Time.deltaTime;
        rotaion *= Time.deltaTime;
        Quaternion turn = Quaternion.Euler(0f, rotaion, 0f);
        rb.MovePosition(rb.position + this.transform.forward * translation);
        rb.MoveRotation(rb.rotation * turn);

        if (translation != 0)
        {
            animator.SetBool("Idling", false);
        }
        else
        {
            animator.SetBool("Idling", true);
        }
    }

    public void Shoot(int damg, GameObject bulletPrefab, float speed)
    {
        instantiatedProjectile = Instantiate(bulletPrefab,
           new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z),
           target.transform.rotation) as GameObject;
        instantiatedProjectile.GetComponent<Rigidbody>().AddForce(transform.forward * (speed*60));
        instantiatedProjectile.GetComponent<Bullet>().damg = damg;

        Destroy(instantiatedProjectile, 5f);
        bulletsFired++;
    }
}
