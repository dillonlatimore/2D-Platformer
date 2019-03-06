using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    public float waitToRespawn;
    public PlayerController thePlayer;

    public GameObject bloodSplatter;

    public int coinCount;

    public Text coinText;
    public AudioSource coinSound;

    public Image heart1;
    public Image heart2;
    public Image heart3;

    public Sprite heartFull;
    public Sprite heartHalf;
    public Sprite heartEmpty;

    public int maxHealth;
    public int currentHealth;

    private bool respawning;

    

    // Use this for initialization
    void Start () {
        thePlayer = FindObjectOfType<PlayerController>();

        coinText.text = "Coins: " + coinCount;

        currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {

        if (currentHealth <= 0 && !respawning)
        {
            respawning = true;
            Respawn();
        }
	}

    public void Respawn()
    {
        StartCoroutine("RespawnCo");  
    }

    public IEnumerator RespawnCo()
    {
        thePlayer.gameObject.SetActive(false);

        Instantiate(bloodSplatter, thePlayer.transform.position, thePlayer.transform.rotation);

        yield return new WaitForSeconds(waitToRespawn);

        currentHealth = maxHealth;
        UpdateHeartMeter();

        respawning = false;

        thePlayer.transform.position = thePlayer.respawnPosition;
        thePlayer.gameObject.SetActive(true);
    }

    public void AddCoins(int coinsToAdd)
    {
        coinCount += coinsToAdd;
        coinText.text = "Coins: " + coinCount;
        coinSound.Play();
    }

    public void HurtPlayer(int damageTaken)
    {
        currentHealth -= damageTaken;
        UpdateHeartMeter();

        thePlayer.Knockback();
    }

    public void GiveHealth(int healthToGive)
    {
        currentHealth += healthToGive;

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UpdateHeartMeter();

        coinSound.Play();
    }

    public void UpdateHeartMeter()
    {
        switch (currentHealth)
        {
            case 6:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartFull;
                return;

            case 5:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartHalf;
                return;

            case 4:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartEmpty;
                return;

            case 3:
                heart1.sprite = heartFull;
                heart2.sprite = heartHalf;
                heart3.sprite = heartEmpty;
                return;

            case 2:
                heart1.sprite = heartFull;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                return;

            case 1:
                heart1.sprite = heartHalf;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                return;

            case 0:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                return;

            default:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                return;
        }
    }
}
