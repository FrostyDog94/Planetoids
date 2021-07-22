using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
	public float xSpread;
	public float zSpread;
	public float yOffset;
	public float startPos;
	public Transform centerPoint;

	public float rotSpeed;
	public bool rotateClockwise;

	public float rotateSpeed = 10;

	float timer = 0;

	public PlanetManager planetManager;

    private void Start()
    {
		xSpread = planetManager.orbitRadius;
		zSpread = planetManager.orbitRadius;
		rotSpeed = planetManager.orbitSpeed;
		startPos = planetManager.startPos;
		centerPoint = planetManager.orbitPoint;
    }

  
    void FixedUpdate()
	{
		timer += Time.deltaTime * rotSpeed;
		if (planetManager.orbitPoint != null)
		{
			Rotate();
		}
	}

	void Rotate()
	{
		if (rotateClockwise)
		{
			float x = -Mathf.Cos(timer) * xSpread;
			float z = Mathf.Sin(timer) * zSpread;
			Vector3 pos = new Vector3(x, yOffset, z);
			transform.position = pos + centerPoint.position;
		}
		else
		{
			float x = Mathf.Cos(timer + startPos) * xSpread;
			float z = Mathf.Sin(timer + startPos) * zSpread;
			Vector3 pos = new Vector3(x, yOffset, z);
			transform.position = pos + centerPoint.position;
		}

		//transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
	}
}
