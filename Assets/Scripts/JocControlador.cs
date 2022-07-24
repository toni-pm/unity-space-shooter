using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JocControlador : MonoBehaviour {

    public GameObject hazard;
    public GameObject municio;
    public GameObject armes;
    public GameObject vida;
    public GameObject escut;
    public GameObject escutJugador;
    public Vector3 spawnValues;
    public int nivell;
    public int hazardCount;
    public int puntuacioMaxima;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    private int nWave;

    public GUIText scoreText;
    public GUIText restartText;
    public GUIText gameOverText;
    public GUIText videsText;
    public GUIText municioText;
    public Image barraArmaSecundaria;

    private bool gameOver;
    private bool restart;
    public bool guanyat;
    private int puntuacioActual;
    private int vidaMax;
    private int armesMax;
    public int municioMax;
    public int nVida;
    public int nArmes;
    public int nMunicio;
    public bool armaSecundaria = false;
    public bool teEscut = false;

    void Start()
    {
        gameOverText.text = "Nivell " + nivell;
        gameOver = false;
        restart = false;
        guanyat = false;
        escutJugador.GetComponent<Renderer>().enabled = false;
        nWave = 0;
        puntuacioActual = 0;
        vidaMax = 3;
        municioMax = 50;
        armesMax = 3;
        nVida = vidaMax;
        nMunicio = municioMax;
        nArmes = 1;
        restartText.text = "";
        videsText.text = "Vides: " + nVida + "/" + vidaMax;
        municioText.text = "Munició: " + nMunicio + "/" + municioMax;
        UpdateScore();
        StartCoroutine(SpawnWaves());
        StartCoroutine(SpawnColleccionables());
    }

    private void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
        if (guanyat)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (nivell != 3)
                {
                    SceneManager.LoadScene(nivell + 1);
                }
                else
                {
                    SceneManager.LoadScene(0);
                }
            }
        }
    }

    IEnumerator SpawnColleccionables()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            int rndVida = Random.Range(0, 3);
            int rndMunicio = Random.Range(0, 2);
            int rndArmes = Random.Range(0, 5);
            int rndEscut = Random.Range(0, 4);

            // Apareix vida
            if (rndVida == 0)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(vida, spawnPosition, spawnRotation);
            }

            // Apareix municio
            if (rndMunicio == 0)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(municio, spawnPosition, spawnRotation);
            }

            // Apareix arma
            if (rndArmes == 0)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(armes, spawnPosition, spawnRotation);
            }

            // Apareix escut
            if (rndEscut == 0)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(escut, spawnPosition, spawnRotation);
            }

            if (gameOver)
            {
                break;
            }

            if (guanyat)
            {
                break;
            }

            yield return new WaitForSeconds(waveWait);
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        gameOverText.text = "";
        while (true)
        {
            nWave++;
            for (int i = 0; i < hazardCount * nWave * nivell; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                break;
            }

            if (guanyat)
            {
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        puntuacioActual += newScoreValue;
        barraArmaSecundaria.fillAmount += 0.02f;
        if (barraArmaSecundaria.fillAmount == 1f)
        {
            armaSecundaria = true;
            barraArmaSecundaria.color = Color.red;
        }
        UpdateScore();
    }
    
    public void AfegirEscut()
    {
        escutJugador.GetComponent<Renderer>().enabled = true;
        teEscut = true;
        StartCoroutine(TreureEscut());
    }

    IEnumerator TreureEscut()
    {
        yield return new WaitForSeconds(5);
        escutJugador.GetComponent<Renderer>().enabled = false;
        teEscut = false;
    }

    public void AfegirVida(int newVida)
    {
        if (nVida <= vidaMax && nVida > 0)
        {
            nVida += newVida;
            if (nVida > vidaMax) nVida = vidaMax;
            videsText.text = "Vides: " + nVida + "/" + vidaMax;
        }
    }

    public void RestarMunicio(int newMunicio)
    {
        nMunicio += newMunicio;
        municioText.text = "Munició: " + nMunicio + "/" + municioMax;
    }

    public void AfegirMunicio()
    {
        nMunicio = municioMax;
        municioText.text = "Munició: " + nMunicio + "/" + municioMax;
    }

    public void AfegirArmes(int newArmes)
    {
        if (nArmes <= armesMax)
        {
            nArmes += newArmes;
            if (nArmes > armesMax) nArmes = armesMax;
        }
    }

    void UpdateScore ()
    {
        scoreText.text = "Score: " + puntuacioActual;
        if (puntuacioActual >= puntuacioMaxima)
        {
            Victoria();
        }
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
        restartText.text = "'R' per reiniciar";
        restart = true;
    }

    private void Victoria()
    {
        guanyat = true;
        if (nivell != 3)
        {
            gameOverText.text = "Has acabat el nivell!";
            restartText.text = "'Espai' per seguir";
        }
        else
        {
            gameOverText.text = "Has acabat el joc!";
            restartText.text = "'Espai' per tornar";
        }
    }
}
