using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuiverScript : MonoBehaviour
{
    private GameObject quiver;

    private GameObject currentKnife;
	public GameObject knifePrefab;

    public int maxArrowCount = 10;
	private List<GameObject> knifeList;

    UnityEngine.XR.InputDevice rightController;

    List<UnityEngine.XR.InputDevice> rightHandDevices;

    public AudioClip knifeSpawnSound;

    public AudioSource source;

    public GameObject knifePos;

    // Start is called before the first frame update
    void Awake()
    {
        quiver = GameObject.FindGameObjectWithTag("quiver");
        knifeList = new List<GameObject>();
        
    }

    // Update is called once per frame
    void Update()
    {
        rightHandDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.RightHand, rightHandDevices);
        if (rightHandDevices.Count > 0 && rightController == null) {
            //Debug.Log(string.Format("Device name '{0}' with role '{1}'", rightHandDevices[0].name, rightHandDevices[0].role.ToString()));
            rightController = rightHandDevices[0];
        }

        if (Vector3.Distance( this.transform.position, quiver.transform.position ) < 0.25f && ( currentKnife == null )) {
           
            
            rightController = rightHandDevices[0];
        

            bool gripValue;
            if (rightController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out gripValue) && gripValue) {
                //Debug.Log("pressed");
                currentKnife = InstantiateKnife();
                source.PlayOneShot(knifeSpawnSound);
            }
        }
    }

    private GameObject InstantiateKnife()
		{
            rightController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceRotation, out Quaternion rotR);
			GameObject knife = Instantiate( knifePrefab, knifePos.transform.position, rotR) as GameObject;
			//arrow.transform.parent = knifePos.transform;

			knifeList.Add( knife );

			while ( knifeList.Count > maxArrowCount )
			{
				GameObject oldKnife = knifeList[0];
				knifeList.RemoveAt( 0 );
				if ( oldKnife )
				{
					Destroy( oldKnife );
				}
			}

			return knife;
		}

    public void ClearKnife() {
        currentKnife = null;
    }

    public void OnReleaseKnife() {
        Vector3 aim = AimAssist.GetAim();
        if (aim.Equals(Vector3.zero)) {
            Debug.Log("there was no aim");
            ClearKnife();
            return;
        }
        
        Debug.Log("got aim, now gonna push");
        Rigidbody rigBod = currentKnife.gameObject.GetComponent<Rigidbody>();

        rigBod.AddForce((aim - currentKnife.transform.position)* 2, ForceMode.Impulse);

        ClearKnife();
    }
}
