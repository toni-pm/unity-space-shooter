using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue;
    private JocControlador jocControlador;

    private void Start()
    {
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

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary")
        {
            return;
        }
        if (other.tag != "Player")
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }
        if (other.tag == "Player")
        {
            if (playerExplosion != null)
            {
                Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
                if (!jocControlador.teEscut)
                {
                    if (!jocControlador.guanyat)
                    {
                        jocControlador.AfegirVida(-1);
                    }
                    if (jocControlador.nArmes > 1)
                    {
                        jocControlador.AfegirArmes(-1);
                        switch (jocControlador.nArmes)
                        {
                            case 1:
                                jocControlador.municioMax = 50;
                                break;
                            case 2:
                                jocControlador.municioMax = 200;
                                break;
                            case 3:
                                jocControlador.municioMax = 400;
                                break;
                            default:
                                jocControlador.municioMax = 500;
                                break;
                        }
                        jocControlador.AfegirMunicio();
                    }
                }
            }
            else
            {
                switch (tag)
                {
                    case "Escut":
                        jocControlador.AfegirEscut();
                        break;
                    case "Vida":
                        jocControlador.AfegirVida(1);
                        break;
                    case "Municio":
                        jocControlador.AfegirMunicio();
                        break;
                    case "Armes":
                        jocControlador.AfegirArmes(1);
                        switch (jocControlador.nArmes)
                        {
                            case 1:
                                jocControlador.municioMax = 50;
                                break;
                            case 2:
                                jocControlador.municioMax = 200;
                                break;
                            case 3:
                                jocControlador.municioMax = 400;
                                break;
                            default:
                                jocControlador.municioMax = 500;
                                break;
                        }
                        jocControlador.AfegirMunicio();
                        break;
                }
            }
        }
        else
        {
            jocControlador.AddScore(scoreValue);
        }
        if (playerExplosion != null)
        {
            if (jocControlador.nVida <= 0)
            {
                Destroy(other.gameObject);
                jocControlador.GameOver();
            }
        }
        Destroy(gameObject);
    }
}
