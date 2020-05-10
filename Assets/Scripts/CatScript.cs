using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CatScript : MonoBehaviour {

    private Rigidbody2D myRigidBody;
    private Animator myAnim;
    public float catJump = 500f;
    private float catHurt = -1; // u mngulur waktu ketika cat kena kaktus
    private Collider2D myCollider; //sth transup
    public Text scoreText;
    private float startCoin;
    private int jumpsLeft = 2; // mengatur jumlah lompatan
    public AudioSource jumpSound;
    public AudioSource deathSound;
    public float speed;


	// Use this for initialization
	void Start () {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myCollider = GetComponent<Collider2D>();

        startCoin = Time.time;
    }
	
	// Update is called once per frame
	void Update () {

        float move = Input.GetAxis("Horizontal");
        myRigidBody.velocity = new Vector2(speed * move , myRigidBody.velocity.y);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
                       
        }


       if (catHurt == -1)
        {
            if ((Input.GetButtonUp("Jump") || Input.GetButtonUp("Fire1")) && jumpsLeft > 0)
            {
                if (myRigidBody.velocity.y < 0) // u melihat cat y kec naik-turun < 0
                {
                    myRigidBody.velocity = Vector2.zero; //berarti telah jatuh
                }

                if (jumpsLeft == 1)// lompatan angkat = 1
                {
                    myRigidBody.AddForce(transform.up * catJump * 0.75f); // kita melakukan angkatan kedua dngna kekuatan tidak penh
                }
                else
                {
                    myRigidBody.AddForce(transform.up * catJump); // loncat apabila tdk =1 loncatan penuh
                }

                jumpsLeft--; //ininya jika melompat dia bisa melompat lagi yh kedua dari atas dengan kecepatan yg utuh; lom 2 tidak sekiat lomp pertama

                jumpSound.Play();
            }

            myAnim.SetFloat("vVelocity", myRigidBody.velocity.y); // meloncat
            //scoreText.text = (Time.time - startCoin).ToString("0");
        }
        else
        {
            if (Time.time > catHurt + 2) // waktu saat ini (detik)> waktu ketika mati;maksudnya ketika cat kena katus animasinya berubah jadi hurt stlah 2 detik kembali seperti awal
            {
                SceneManager.LoadScene("GameOver");
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);                
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision) //memanggil untuk bertabrakan dg benda
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Enemy")) //memanggil musuh itu ada. kena cat maka bendanya balik dari awal
        {
            foreach (PrefabSpawner spawner in FindObjectsOfType<PrefabSpawner>())
            {
                spawner.enabled = false;
            }

            foreach (MoveLeft moveLefter in FindObjectsOfType<MoveLeft>())
            {
                moveLefter.enabled = false;
            }


            catHurt = Time.time;
            myAnim.SetBool("catHurt", true); // mengatur anim cat kena kaktus terus mati
            myRigidBody.velocity = Vector2.zero; //menghentikan kec cat
            myRigidBody.AddForce(transform.up * catJump); // ketika kena kactus dia mati dg lompat
            myCollider.enabled = false; //nonaktifkan collider agar cat kena katus pas mati langsung kebawah

            deathSound.Play();

            float currentBestScore = PlayerPrefs.GetFloat("BestScore", 0);
            float currentScore = Time.time - startCoin;
  
            if (currentScore > currentBestScore)
            {
                PlayerPrefs.SetFloat("BestScore", currentScore);
            }
    
        }
        else if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            jumpsLeft = 2; // apabila menyentuh ground lompatan isa dua kali
           
        }
         
    }
}

// note
// supaya jatuhnya cat di layer pling depan ganti ankga di layer cat jd 1