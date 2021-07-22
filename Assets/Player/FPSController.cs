using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GravityBody))]
public class FPSController : MonoBehaviour
{
	// public vars
	public float mouseSensitivityX = 1;
	public float mouseSensitivityY = 1;
	public float walkSpeed = 6;
	public float jumpForce = 220;
	public LayerMask groundedMask;
	public LayerMask atmo;

	// System vars
	public bool grounded;
	Vector3 moveAmount;
	Vector3 smoothMoveVelocity;
	float verticalLookRotation;
	public Transform cameraTransform;
	Rigidbody rigidbody;

	public GameObject impactEffect;
	public ParticleSystem muzzleFlash;
	public GameObject lasers;
	public AudioSource gunShot;
	public AudioSource gunShot2;
	bool shot;
	

	public PlayerManager playerManager;
	public Animator animator;

	public Transform tracerSpawn;

	public float fireRate = 1f;
	float fireTimer;

	public BuildMenu buildMenu;

	


	void Awake()
	{
		fireTimer = fireRate;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		rigidbody = playerManager.rb;
		shot = false;
		
		
	}

	void Update()
	{
		fireTimer -= Time.deltaTime;

		// Look rotation:
		if (buildMenu.isOpen == false)
		{
			transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensitivityX);
			verticalLookRotation += Input.GetAxis("Mouse Y") * mouseSensitivityY;
			verticalLookRotation = Mathf.Clamp(verticalLookRotation, -60, 60);
			cameraTransform.localEulerAngles = Vector3.left * verticalLookRotation;
		}
		// Calculate movement:
		float inputX = Input.GetAxisRaw("Horizontal");
		float inputY = Input.GetAxisRaw("Vertical");

		Vector3 moveDir = new Vector3(inputX, 0, inputY).normalized;
		Vector3 targetMoveAmount = moveDir * walkSpeed;
		moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, .15f);

		/*
		// Jump
		if (Input.GetButtonDown("Jump"))
		{
			if (grounded)
			{
				rigidbody.AddForce(transform.up * jumpForce);
			}
		}
		*/

		// Grounded check
		Ray ray = new Ray(transform.position, -transform.up);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, 1 + .1f, groundedMask))
		{
			grounded = true;
		}
		else
		{
			grounded = false;
		}

		if (Input.GetKey(KeyCode.Mouse0) && fireTimer <= 0 && buildMenu.isOpen == false)
		{
			Shoot();
			animator.SetBool("isShooting", true);
			fireTimer = fireRate;


        }
        else
        {
			animator.SetBool("isShooting", false);
        }


		Read();

	}

	void FixedUpdate()
	{
		// Apply movement to transform
		Vector3 localMove = transform.TransformDirection(moveAmount) * Time.fixedDeltaTime;
		transform.position = transform.position + localMove;
	}

	void Shoot()
	{
		muzzleFlash.Play();
		var laserObj = Instantiate(lasers, tracerSpawn.position, tracerSpawn.rotation);
		laserObj.transform.parent = transform.parent;
		Destroy(laserObj, 2);
		RaycastHit hit;
		Physics.Raycast(cameraTransform.position, cameraTransform.transform.TransformDirection(Vector3.forward), out hit, 20, ~atmo);

		if (hit.transform != null)
		{
			IDamageable damageable = hit.transform.GetComponent<IDamageable>();


			if (damageable != null)
			{
				damageable.Damage();
				var impactObj = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
				impactObj.transform.parent = transform.parent;
				Destroy(impactObj, 2f);

			}
			else
			{
				var impactObj = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
				impactObj.transform.parent = transform.parent;
				Destroy(impactObj, 2f);
			}
		}

		if (shot)
        {
			gunShot.Play();
        }
        else
        {
			gunShot2.Play();
        }

		shot = !shot;

		

	}


	void Read()
    {
		RaycastHit hit;
		Physics.Raycast(cameraTransform.position, cameraTransform.transform.TransformDirection(Vector3.forward), out hit, 10, ~atmo);

		if (hit.transform != null)
		{
			if (hit.transform.GetComponent<HoverMessage>() != null)
			{
				GameInfo.Instance.text.text = hit.transform.GetComponent<HoverMessage>().text;
            }
            else
            {
				GameInfo.Instance.text.text = "";

			}
        }
        else
        {
			GameInfo.Instance.text.text = "";
		}

        
		
	}


}
