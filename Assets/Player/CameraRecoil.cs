using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRecoil : MonoBehaviour
{
    public float recoilSpeed = 1f;
    public float speed = 1;
    Quaternion original;
    
    bool shooting = false;
   
    
    // Start is called before the first frame update
    void Start()
    {
        original = transform.rotation;
        
    }

    // Update is called once per frame
    void Update()
    {

       if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            shooting = true;
           // StopCoroutine(MoveBack());
            StartCoroutine(MoveToPosition());


        } 
       if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            shooting = false;
            //StopCoroutine(MoveToPosition());
            StartCoroutine(MoveBack());
        }



     
    }

    IEnumerator MoveToPosition()
    {
        while (shooting)
        {
            transform.Rotate(Vector3.left * speed);
            yield return new WaitForSeconds(recoilSpeed);
        }

    }

    IEnumerator MoveBack()
    {
        while (transform.localRotation.x < 0)
        {
            transform.Rotate(Vector3.right * speed);
            yield return new WaitForSeconds(0.01f);
        } 
       
    }
    void recoilBack()
    {
        StartCoroutine(MoveToPosition());
    }

    void recoilForward()
    {
        //StopCoroutine(MoveToPosition(-0.2f));
        transform.rotation = original;
        
    }
}

