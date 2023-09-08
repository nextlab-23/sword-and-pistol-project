using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Shooting : MonoBehaviour
{
   
    public float fireRate = 0.1f;
    public GameObject bulletPrefab;

    float _elapsedTime;

    public Transform nozzleTransform;

 
    public Animator gunAnimator;

    [FormerlySerializedAs("ShootingButton")] public OVRInput.Button shootingButton;
    public GameObject slicerGameObject;

    // Update is called once per frame
    void Update()
    {
        //elapsed time
        _elapsedTime += Time.deltaTime;

        if (!Input.GetMouseButtonDown(0) && !OVRInput.GetDown(shootingButton, OVRInput.Controller.RTouch)) return;
        if (!(_elapsedTime > fireRate)) return;
        Shoot();
                
        _elapsedTime = 0;

    }

    private void Shoot()
    {
        //Play sound
        var position = nozzleTransform.position;
        AudioManager.instance.gunSound.gameObject.transform.position = position;
        AudioManager.instance.gunSound.Play();

        //Play animation
        gunAnimator.SetTrigger("Fire");

      
        //Create the bullet
        var bulletGameobject = Instantiate(bulletPrefab, position, Quaternion.Euler(0, 0, 0));
        bulletGameobject.transform.forward = nozzleTransform.forward;
        Physics.IgnoreCollision(bulletGameobject.GetComponent<Collider>(), slicerGameObject.GetComponent<Collider>());
    }

   


}
