using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour {

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

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "ArmaSecundaria")
        {
            jocControlador.armaSecundaria = false;
            jocControlador.barraArmaSecundaria.fillAmount = 0;
            jocControlador.barraArmaSecundaria.color = Color.blue;
        }
        Destroy(other.gameObject);
    }
}
