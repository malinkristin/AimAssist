using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAssist : MonoBehaviour
{
    public static Vector3 aim;

    private UnityEngine.XR.Interaction.Toolkit.XRRayInteractor ray;
    // Start is called before the first frame update
    void Awake()
    {
        ray = this.GetComponent<UnityEngine.XR.Interaction.Toolkit.XRRayInteractor>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentTarget;
        //ray.transform.forward
    }

    public void setAim() {

    }
}
