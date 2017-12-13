using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Controller2DBehaviour))]
[RequireComponent(typeof(Animator))]
public class PlayerBehaviour : MonoBehaviour {

    private const string VELOCITY = "Velocity";
    private const string GROUND = "Ground";
    //private const string V_VELOCITY = "vVelocity";
    private const string FIRE = "Fire";

    private Controller2DBehaviour controller;
    private SpriteRenderer sprite;
    private Animator animator;

    private float jumpVelocity;
    private float gravity;
    private Vector2 velocity;
    private float timeAfterLastFire;

    [SerializeField]
    private float moveSpeed = 6;
    [SerializeField]
    private float jumpHight = 4f;
    [SerializeField]
    private float timeToJumpApex = .4f;
    [SerializeField]
    private float timeBetweenFire = 1f;


	void Start () {

        controller = GetComponent<Controller2DBehaviour>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        gravity = -((2 * jumpHight) / Mathf.Pow(timeToJumpApex, 2));
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
	}

    private void FixedUpdate()
    {
        controller.Move(velocity * Time.fixedDeltaTime);
    }

    private void Update () 
    {
        if(controller.GetCollisionInfo().above || controller.GetCollisionInfo().below) 
        {
            velocity.y = 0;    
        }

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if(Input.GetButtonDown("Jump") && controller.GetCollisionInfo().below)
        {
            velocity.y = jumpVelocity;
        }

        //timeAfterLastFire -= Time.deltaTime;
        if(Input.GetButtonDown("Fire1") )
        {
            animator.SetTrigger(FIRE);
            //timeAfterLastFire = timeBetweenFire;
        }

        velocity.x = input.x * moveSpeed;
        velocity.y += gravity * Time.deltaTime;

		if (input.x > 0)
		{
			sprite.flipX = false;
		}
		else if (input.x < 0)
		{
			sprite.flipX = true;
		}

        animator.SetBool(GROUND, controller.GetCollisionInfo().below);
        animator.SetFloat(VELOCITY, Mathf.Abs(velocity.x));

	}
}
