using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeExplode : MonoBehaviour
{

    public GameObject shatteredObject;
    public GameObject mainCube;
    // Start is called before the first frame update
    void Start()
    {
        mainCube.SetActive(true);
        shatteredObject.SetActive(false);
    }

    private void IsShot()
    {
        Destroy(mainCube);
        shatteredObject.SetActive(true);
        var shatterAnimation = shatteredObject.GetComponent<Animation>().Play();
        VibrationManager.instance.VibrateController(0.2f, 1, 0.3f, OVRInput.Controller.RTouch);
        
        ScoreManager.instance.AddScore(ScorePoints.GUNCUBE_SCOREPOINT);
        Destroy(shatteredObject,1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            IsShot();
        }
    }


}
