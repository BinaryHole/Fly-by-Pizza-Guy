﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    // reference to different types of pizzas
    public GameObject pepperoniPizza;
    public GameObject hawaiianPizza;
    public GameObject spicyPizza;

    // reference to parent gameObject to spawn pizzas in
    public GameObject spawnPizzasFrom;

    // used to determine the speed of the pizza thrown (~1-100)
    public double pizzaSpeed;

    // reference to the player
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // do player pizza throwing
        checkForShoot();
    }

    // Handles pizza throwing (checks if user is pressing left-mouse)
    void checkForShoot()
    {
        // check if the player is clicking the left mouse
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // instantiate the projectile pizza
            GameObject pizza = Instantiate(pepperoniPizza, player.transform.position, Quaternion.identity, spawnPizzasFrom.transform) as GameObject;

            // initialize the camera-mouse ray, the ray-collision marker, and the final vector direction
            Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Vector3 direction;
            
            // draw a visual ray in blue for 2 seconds (only visible in the viewport)
            Debug.DrawRay(Camera.main.transform.position, cameraRay.direction * 1000, Color.blue, 2);

            // if the ray has collided with an object
            if (Physics.Raycast(cameraRay, out hit))
            {
                // create a direction from the initialized pizza to the collided hitpoint
                direction = (hit.point - pizza.transform.position).normalized;

            // if the ray has not collided with an object
            } else
            {
                // create a direction from the camera-mouse ray
                direction = cameraRay.direction.normalized;
            }

            // draw a visual ray in red for 2 seconds (only visible in the viewport)
            Debug.DrawRay(pizza.transform.position, direction*1000, Color.red, 2);

            // apply the force to the initialize pizza 
            pizza.GetComponent<Rigidbody>().AddForce(direction * (float) (pizzaSpeed*100));
        }
    }
}