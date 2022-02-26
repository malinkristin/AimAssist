using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyHitScript : MonoBehaviour
{
    Animator anim;
    int counter = 0;
    bool isDead = false;

    List<GameObject> stuckArrows = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     void OnCollisionEnter(Collision other)
        {
            if (!isDead && other.gameObject.tag == "projectile") {
                if(counter == 2) {
                    foreach(GameObject arrow in stuckArrows) {
                        Destroy(arrow);
                    }
                    anim.SetBool("isDead", true);
                    isDead = true;
                    Debug.Log("dead");
                    return;
                }

                other.rigidbody.isKinematic = true;
                stuckArrows.Add(other.gameObject);
                Debug.Log("hit");
                counter++;
            }
        }
}
