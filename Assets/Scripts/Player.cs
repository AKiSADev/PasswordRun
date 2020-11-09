using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public float moveSpeed;
    public float jumpForce;
    public LayerMask layerMask;
    public DataStorage dataStorage;
    public GameObject deathPanel;
    public AudioClip collectSound;
    public Text passwordText;
    public Text weaknessText;
    public Text fakePassDisplay;
    public Text bonusT;

    public bool isPaused = false;
    public bool doubleJump = true;

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private Vector2 v2 = new Vector2();
    private float accelerator;
    private string playerName;
    private bool endGame = false;

    void Start()
    {
        Debug.Log("TIME DELTA TIME: " + Time.deltaTime);
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        accelerator = moveSpeed;
    }



    void FixedUpdate()
    {
        if (!isPaused)
        {

            if (rb.transform.position.y < -1)
                EndGame();

            v2.x = moveSpeed * Time.deltaTime + accelerator;
            v2.y = rb.velocity.y;

            rb.velocity = v2;

            bool grounded = isGrounded();

            if (grounded)
                doubleJump = true;

            if ((Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0)) 
            {
                if (grounded)
                {
                    v2.y = jumpForce;
                    rb.velocity = v2;
                }
                else
                {
                    if (doubleJump)
                    {
                        v2.y = jumpForce;
                        rb.velocity = v2;
                        doubleJump = false;

                    }
                }

            }
        }
        else
        {
            v2.x = 0;
            v2.y = 0;
            if (rb != null)
                rb.velocity = v2;
        }
    }


    public bool isGrounded()
    {
        float extraHeightText = .1f;

        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, extraHeightText, layerMask);

        Debug.Log(raycastHit.collider);

        return raycastHit.collider != null;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Letter"))
        {
            string letter = collision.gameObject.GetComponent<SpriteRenderer>().sprite.name;
            dataStorage.updatePointsAndPassword(letter);
            Destroy(collision.gameObject);
            
            displayPass();

            AudioSource.PlayClipAtPoint(collectSound, transform.position);
            
            accelerator = accelerator + 0.5f;
        }
        else if (collision.gameObject.CompareTag("Spike"))
        {
            EndGame();
        }


    }

    public void setIsPaused(bool isPaused)
    {
        this.isPaused = isPaused;
    }

    void EndGame()
    {
        if (!endGame)
        {

            string pass = dataStorage.getPassword();
            Debug.Log(pass);
            passwordText.text = pass;

            switch (DataStorage.CheckStrength(pass))
            {
                case 0:
                    {
                        weaknessText.text = "Inesistente";
                        dataStorage.points += 0;
                        bonusT.text = "+0";
                        break;
                    }
                case 1:
                case 2:
                case 3:
                    {
                        weaknessText.text = "Non adeguata. Lo so che puoi fare di meglio";
                        dataStorage.points += 100;
                        bonusT.text = "+100";
                        break;
                    }
                case 4:
                    {
                        weaknessText.text = "Buono ma ti manca ancora qualcosa...";
                        dataStorage.points += 200;
                        bonusT.text = "+200";
                        break;
                    }
                case 5:
                    {
                        weaknessText.text = "Ottimo!";
                        dataStorage.points += 500;
                        bonusT.text = "+500";
                        break;
                    }
                case 6:
                    {
                        weaknessText.text = "Questa password è perfetta!";
                        dataStorage.points += 1000;
                        bonusT.text = "+1000";
                        break;
                    }
            }

            playerName = PlayerPrefs.GetString("PlayerName");
            //playerName = "UnityTester2";
            PlayerScore ps = new PlayerScore();
            ps.playerName = playerName;
            ps.points = dataStorage.points;

            UnityWebRequest www = UnityWebRequest.Put("https://password-run.herokuapp.com/passwordrun/scores", JsonUtility.ToJson(ps));
            www.SetRequestHeader("Content-Type", "application/json");
            www.SendWebRequest();

            Debug.Log("RESPONSE CODE: " + www.responseCode);


            if (rb != null)
                Destroy(rb);
            isPaused = true;
            deathPanel.SetActive(true);
            endGame = true;

            
        }
    }

    public void displayPass()
    {
        fakePassDisplay.text = fakePassDisplay.text + "*";
    }

    private class PlayerScore
    {
        public string playerName;

        public int points;
    }

}
