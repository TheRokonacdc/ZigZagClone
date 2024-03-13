using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    // Savestate
    private GameManager gameManager;
    
    // Char RigidBody
    private Rigidbody rb;

    // Walk direction
    private bool walkRight = true;

    // Fall detection
    public Transform rayStart;
    private Animator animator;
    private float airTime;

    // Crystal pickup
    public GameObject crystalEffect;
    
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        gameManager = FindAnyObjectByType<GameManager>();
    }

    private void FixedUpdate()
    {
        if (!gameManager.gameStarted)
        {
            return;
        }
        else 
        { 
            animator.SetTrigger("GameStart"); // Starts run animation
        }

        // Moves player the direction it is facing at constant speed during input
        rb.transform.position = transform.position + 2 * Time.deltaTime * transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0)) { SwitchDirection(); }
        
        RaycastHit hit;
        if(!Physics.Raycast(rayStart.position,-transform.up, out hit, Mathf.Infinity)) // Raycast down from player, if no hit...
        {
            animator.SetTrigger("FallTrigger");
        }
        else { animator.SetTrigger("FalseFall"); }

        if (transform.position.y < -2.5f)
        {
            gameManager.EndGame();
        }
    }

    private void SwitchDirection()
    {
        if (!gameManager.gameStarted) { return; }
        
        walkRight = !walkRight;

        if (walkRight) { transform.rotation = Quaternion.Euler(0, 45, 0); }
        else { transform.rotation = Quaternion.Euler(0, -45, 0); }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Crystal")
        {
            GameObject crystal = Instantiate(crystalEffect, transform.position + new Vector3(0,.5f,0), Quaternion.identity);
            Destroy(crystal, 2f);
            Destroy(other.gameObject);
            gameManager.IncreaseScore();
        }
    }
}
