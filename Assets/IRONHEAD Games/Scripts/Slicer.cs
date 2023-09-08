using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using Unity.VisualScripting;
using UnityEngine.Serialization;

public class Slicer : MonoBehaviour
{
    [FormerlySerializedAs("MaterialAfterSlice")] public Material materialAfterSlice;
    public LayerMask sliceMask;

    public bool isTouched;

    private void Update()
    {
        if (isTouched != true) return;
        isTouched = false;
        var objectsToBeSliced = Physics.OverlapBox(transform.position, new Vector3(1, 0.1f, 0.1f), transform.rotation, sliceMask);
        foreach (var objectToBeSliced in objectsToBeSliced)
        {
              
            var slicedObject = SliceObject(objectToBeSliced.gameObject, materialAfterSlice);

            var upperHullGameobject = slicedObject.CreateUpperHull(objectToBeSliced.gameObject, materialAfterSlice);
            var lowerHullGameobject = slicedObject.CreateLowerHull(objectToBeSliced.gameObject, materialAfterSlice);

            var position = objectToBeSliced.transform.position;
            AudioManager.instance.sliceSound.gameObject.transform.position = position;
            AudioManager.instance.sliceSound.Play();
                
            VibrationManager.instance.VibrateController(0.2f, 1, 0.3f, OVRInput.Controller.LTouch);
            ScoreManager.instance.AddScore(ScorePoints.SWORDCUBE_SCOREPOINT);

             
            upperHullGameobject.transform.position = position;
            lowerHullGameobject.transform.position = position;
               

            MakeItPhysical(upperHullGameobject, objectToBeSliced.gameObject.GetComponent<Rigidbody>().velocity);
            MakeItPhysical(lowerHullGameobject, objectToBeSliced.gameObject.GetComponent<Rigidbody>().velocity);

            Destroy(objectToBeSliced.gameObject);
        }

    }
    private void MakeItPhysical(GameObject obj, Vector3 _velocity)
    {
        obj.AddComponent<MeshCollider>().convex = true;
        obj.AddComponent<Rigidbody>();
        obj.GetComponent<Rigidbody>().velocity = -_velocity;

        var randomNumberX = Random.Range(0,2);
        var randomNumberY = Random.Range(0, 2);
        var randomNumberZ = Random.Range(0, 2);

        obj.GetComponent<Rigidbody>().AddForce(3*new Vector3(randomNumberX,randomNumberY,randomNumberZ),ForceMode.Impulse);       
        obj.AddComponent<DestroyAfterSeconds>();

    }

   

    private SlicedHull SliceObject(GameObject obj, Material crossSectionMaterial = null)
    {
        // slice the provided object using the transforms of this object
        var transform1 = transform;
        return obj.Slice(transform1.position, transform1.up, crossSectionMaterial);
    }

}
