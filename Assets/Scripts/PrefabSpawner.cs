using UnityEngine;
using System.Collections;

//membuat benda jadi banyak
public class PrefabSpawner : MonoBehaviour {

    private float nextSpawn = 0; // kelas mebuat benda menjadi banyak
    public Transform prefabToSpawn; // memanggil benda jadi banyak atau memasukkan benda dari prefeb kaktus
   
   
    public AnimationCurve spawnCurve;
    public float curveLengthInSeconds = 30f;
    private float startTime;
    public float jitter = 0.25f;


	// Use this for initialization
	void Start () {
        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
	
        if (Time.time > nextSpawn)
       
        {
            Instantiate(prefabToSpawn, transform.position, Quaternion.identity); // dirum benda jadi banyak
            //nextSpawn = Time.time + spawnRate + Random.Range(0, randomDelay);

            float curvePos = (Time.time - startTime) / curveLengthInSeconds;
            if (curvePos > 1f)
            {
                curvePos = 1f;
                startTime = Time.time;
            }

            nextSpawn = Time.time + spawnCurve.Evaluate(curvePos) + Random.Range(-jitter, jitter);

        }

	}
}
