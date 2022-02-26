using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuiverScript : MonoBehaviour
{
    private GameObject quiver;

    private GameObject currentArrow;
	public GameObject arrowPrefab;

    public int maxArrowCount = 10;
	private List<GameObject> arrowList;

    UnityEngine.XR.InputDevice rightController;

    List<UnityEngine.XR.InputDevice> rightHandDevices;

    public AudioClip arrowSpawnSound;

    public AudioSource source;

    public GameObject knifePos;

    // Start is called before the first frame update
    void Awake()
    {
        quiver = GameObject.FindGameObjectWithTag("quiver");
        arrowList = new List<GameObject>();
        
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

        if (Vector3.Distance( this.transform.position, quiver.transform.position ) < 0.25f && ( currentArrow == null )) {
           
            
            rightController = rightHandDevices[0];
        

            bool gripValue;
            if (rightController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out gripValue) && gripValue) {
                //Debug.Log("pressed");
                currentArrow = InstantiateKnife();
                source.PlayOneShot(arrowSpawnSound);
            }
        }

        

    }

    private GameObject InstantiateKnife()
		{
			GameObject arrow = Instantiate( arrowPrefab, knifePos.transform.position, knifePos.transform.rotation ) as GameObject;
			arrow.name = "Bow Arrow";
			arrow.transform.parent = knifePos.transform;

			arrowList.Add( arrow );

			while ( arrowList.Count > maxArrowCount )
			{
				GameObject oldArrow = arrowList[0];
				arrowList.RemoveAt( 0 );
				if ( oldArrow )
				{
					Destroy( oldArrow );
				}
			}

			return arrow;
		}
}
