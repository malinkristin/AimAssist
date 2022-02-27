using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonHitScript : MonoBehaviour
{

    public float lifetime = 15f;
    public bool burstOnLifetimeEnd = false;

    private float destructTime = 0f;
    public GameObject popPrefab;

    private bool bParticlesSpawned = false;

    public AudioClip balloonPop;

    private void Start() {
        destructTime = Time.time + lifetime + Random.value;
    }

    private void FixedUpdate() {
        if ( ( destructTime != 0 ) && ( Time.time > destructTime ) )
			{
				Destroy( gameObject );
			}
    }

    private void SpawnParticles( GameObject particlePrefab)
		{
			// Don't do this twice
			if ( bParticlesSpawned )
			{
				return;
			}

			bParticlesSpawned = true;

			if ( particlePrefab != null )
			{
				GameObject particleObject = Instantiate( particlePrefab, transform.position, transform.rotation ) as GameObject;
				particleObject.GetComponent<ParticleSystem>().Play();
				Destroy( particleObject, 2f );
			}
		}

        private void ApplyDamage()
		{
			SpawnParticles( popPrefab );
			Destroy( gameObject );
		}

    private void OnCollisionEnter(Collision other) {
        Debug.Log("collision detected");
        if (other.gameObject.tag == "projectile") {
            Debug.Log("hit the balloon");
            AudioSource audioSource = GetComponentInParent<AudioSource>();
            audioSource.PlayOneShot(balloonPop);

            ApplyDamage();
        }
    }
}
