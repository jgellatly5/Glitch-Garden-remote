﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof (Attacker))]
public class Fox : MonoBehaviour {

    private Attacker attacker;
    private Animator anim;

    // Use this for initialization
    void Start()
    {
        attacker = GetComponent<Attacker>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject obj = collider.gameObject;

        // Leave the method if not colliding with defender;
        if (!obj.GetComponent<Defender>())
        {
            return;
        }
        if (obj.GetComponent<Stone>())
        {
            anim.SetTrigger("JumpTrigger");
        }
        else
        {
            anim.SetBool("IsAttacking", true);
            attacker.Attack(obj);
        }
    }
}
