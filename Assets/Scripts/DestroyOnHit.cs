using UnityEngine;
using System.Collections;
//untuk menghilangkan objek yang berjalan ke kiri setelah melewati cat
public class DestroyOnHit : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject); // untuk menghancurkan objek jika cat lompat
    }
}
//trigger membuat batas luar area dari gamenya