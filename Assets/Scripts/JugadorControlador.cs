using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class JugadorControlador : MonoBehaviour {

    private Rigidbody rb;
    public float velocitat;
    public float tilt;
    public Boundary boundary;

    public GameObject shot1;
    public GameObject shot2;
    public GameObject shot3;
    public GameObject shotSecundari;
    public Transform shotSpawn;
    public float fireRate;

    private float nextFire;
    private float nextFireSecundaria;
    private AudioSource audioSource;
    public AudioClip audioFileArmaSecundaria;

    private JocControlador jocControlador;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        GameObject jocControladorObject = GameObject.FindWithTag("GameController");
        if (jocControladorObject != null)
        {
            jocControlador = jocControladorObject.GetComponent<JocControlador>();
        }
        if (jocControlador == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (jocControlador.nMunicio > 0)
        {
            if (Input.GetButton("Fire1") && Time.time > nextFire)
            {
                jocControlador.RestarMunicio(-1);
                jocControlador.municioText.text = "Munició: " + jocControlador.nMunicio + "/" + jocControlador.municioMax;
                switch (jocControlador.nArmes)
                {
                    case 1:
                        nextFire = Time.time + fireRate;
                        Instantiate(shot1, shotSpawn.position, shotSpawn.rotation);
                        break;
                    case 2:
                        nextFire = Time.time + fireRate / 2f;
                        Instantiate(shot2, shotSpawn.position, shotSpawn.rotation);
                        break;
                    case 3:
                        nextFire = Time.time + fireRate / 3f;
                        Instantiate(shot3, shotSpawn.position, shotSpawn.rotation);
                        break;
                }
                audioSource.Play();
            }
            else if (Input.GetButton("Fire2") && Time.time > nextFireSecundaria && jocControlador.armaSecundaria)
            {
                jocControlador.armaSecundaria = false;
                nextFireSecundaria = Time.time + 2;
                Instantiate(shotSecundari, shotSpawn.position, shotSpawn.rotation);
                AudioSource.PlayClipAtPoint(audioFileArmaSecundaria, transform.position);
            }
        }
    }
    
    void FixedUpdate()
    {
        float movimentX = Input.GetAxis("Horizontal");
        float movimentZ = Input.GetAxis("Vertical");

        Vector3 moviment = new Vector3(movimentX, 0.0f, movimentZ);
        rb.velocity = moviment * velocitat;
        
        rb.position = new Vector3
        (
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        );
        
        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }
}
