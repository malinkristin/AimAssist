using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAssist : MonoBehaviour
{
    public static Vector3 aim;

    UnityEngine.XR.InputDevice leftController;

    List<UnityEngine.XR.InputDevice> leftHandDevices;

    private UnityEngine.XR.Interaction.Toolkit.XRRayInteractor ray;
    // Start is called before the first frame update
    void Awake()
    {
        ray = this.GetComponent<UnityEngine.XR.Interaction.Toolkit.XRRayInteractor>();
    }

    // Update is called once per frame
    void Update()
    {
        leftHandDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.LeftHand, leftHandDevices);
        if (leftHandDevices.Count > 0 && leftController == null) {
            //Debug.Log(string.Format("Device name '{0}' with role '{1}'", rightHandDevices[0].name, rightHandDevices[0].role.ToString()));
            leftController = leftHandDevices[0];
        }
        leftController = leftHandDevices[0];
        leftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.devicePosition, out Vector3 pos);
        leftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceRotation, out Quaternion rot);

       
        if(Physics.Raycast(pos, rot * Vector3.forward, out RaycastHit collisions, 50f)){
            if(collisions.collider.tag == "target") {
                //Debug.Log("this is a target " + collisions.collider.gameObject.name);
                bool triggerValue;
                if (leftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && triggerValue) {
                    Debug.Log("Aim set to " + collisions.collider.gameObject.name);
                    SetAim(collisions.point);
                }
            
                //SetAim(collisions.collider.gameObject.transform.position);
            } else {
                SetAim(new Vector3(0,0,0));
            }
        };
        
    }

    public void SetAim(Vector3 targetPos) {
        aim = targetPos;
    }

    public static Vector3 GetAim() {
        return aim;
    }
}
