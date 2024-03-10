using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{

    private Rigidbody rb;
    private bool walkRight = true;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // Moves player the direction it is facing at constant speed during input
        rb.transform.position = transform.position + transform.forward * 2 * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) { SwitchDirection(); }
    }

    private void SwitchDirection()
    {
        walkRight = !walkRight;

        if (walkRight) { transform.rotation = Quaternion.Euler(0, 45, 0); }
        else { transform.rotation = Quaternion.Euler(0, -45, 0); }
    }
}
