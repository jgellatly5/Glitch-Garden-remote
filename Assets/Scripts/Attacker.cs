using UnityEngine;
using System.Collections;

[RequireComponent(typeof (Rigidbody2D))]
public class Attacker : MonoBehaviour {

    [Tooltip("Average number of seconds between appearances")]
    public float seenEverySeconds;

    private float currentSpeed;
    private GameObject currentTarget;
    private Health health;
    private Animator anim;


    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!currentTarget)
        {
            anim.SetBool("IsAttacking", false);
        }
        transform.Translate(Vector3.left * currentSpeed * Time.deltaTime);
	}
    void OnTriggerEnter2D(Collider2D collider)
    {
        // reference
    }

    public void SetSpeed(float speed)
    {
        currentSpeed = speed;
    }

    //called from animator at time of actual blow
    public void StrikeCurrentTarget(float damage)
    {
        if (currentTarget)
        {
            Health health = currentTarget.GetComponent<Health>();
            if (health)
            {
                health.DealDamage(damage);
            }
        }
    }
    public void Attack(GameObject obj)
    {
        currentTarget = obj;
    }
}
