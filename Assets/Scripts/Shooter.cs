using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour {

    public GameObject projectile, gun;

    private GameObject projectileParent;
    private Animator animator;
    private AttackerSpawner myLaneSpawner;

    void Start()
    {
        animator = GameObject.FindObjectOfType<Animator>();
        projectileParent = GameObject.Find("Projectiles");

        //creates a parent if necessary
        if(!projectileParent)
        {
            projectileParent = new GameObject("Projectiles");
        }
        SetMyLaneSpawner();
        print(myLaneSpawner);
    }
    void Update()
    {
        if (IsAttackerAheadInLane())
        {
            animator.SetBool("IsAttacking", true);
        }
        else
        {
            animator.SetBool("IsAttacking", false);
        }
    }

    // Look through all spawners, and set myLaneSpawner
    void SetMyLaneSpawner()
    {
        AttackerSpawner[] spawnerArray = GameObject.FindObjectsOfType<AttackerSpawner>();
        foreach (AttackerSpawner spawner in spawnerArray)
        {
            if(spawner.transform.position.y == transform.position.y)
            {
                myLaneSpawner = spawner;
                return;
            }
        }
        Debug.LogError(name + "can't find spawner in lane");
    }
	
    private void FireGun()
    {
        GameObject newProjectile = Instantiate(projectile) as GameObject;
        newProjectile.transform.parent = projectileParent.transform;
        newProjectile.transform.position = gun.transform.position;
    }
    bool IsAttackerAheadInLane()
    {
        // exit if no attacker in lane
        if(myLaneSpawner.transform.childCount <= 0)
        {
            return false;
        }
        // if there are attackers, are they ahead?
        foreach(Transform attacker in myLaneSpawner.transform)
        {
            if(attacker.transform.position.x > transform.position.x)
            {
                return true;
            }
        }
        // attacker in lane but behind us
        return false;
    }
}
