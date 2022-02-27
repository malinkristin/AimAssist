using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BaloonSpawn : MonoBehaviour
{

    public float minSpawnTime = 5f;
		public float maxSpawnTime = 15f;
		private float nextSpawnTime;
		public GameObject balloonPrefab;

		public bool autoSpawn = true;

        public AudioClip balloonPop;

        public AudioSource source;

        public float scale = 1f;

        void Start()    
		{
			if ( balloonPrefab == null )
			{
				return;
			}

			if ( autoSpawn)
			{
				SpawnBalloon();
				nextSpawnTime = Random.Range( minSpawnTime, maxSpawnTime ) + Time.time;
			}
		}

        void FixedUpdate()
		{
			if ( balloonPrefab == null )
			{
				return;
			}

			if ( ( Time.time > nextSpawnTime ) && autoSpawn )
			{
				SpawnBalloon();
				nextSpawnTime = Random.Range( minSpawnTime, maxSpawnTime ) + Time.time;
			}
		}

        public GameObject SpawnBalloon()
		{
			if ( balloonPrefab == null )
			{
				return null;
			}
			GameObject balloon = Instantiate( balloonPrefab, transform.position, transform.rotation ) as GameObject;
            balloon.transform.parent = transform;
			balloon.transform.localScale = new Vector3( scale, scale, scale );
			
			balloon.GetComponentInChildren<Rigidbody>().AddForce(Vector3.up);
			
			return balloon;
		}



}
