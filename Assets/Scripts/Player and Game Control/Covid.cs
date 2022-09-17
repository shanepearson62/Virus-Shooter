using System.Timers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;

public class Covid : MonoBehaviour
{
    public Sprite[] sprites;
    private SpriteRenderer _spriteRenderer;
    public float startTime;
    public float currTime;
    public float interval = 2.0f;
    public float speed = 50.0f;
    public float size = 1.0f;
    public float minSize = 0.5f;
    public float maxLifetime = 30.0f;
    public float maxSize = 1.5f;
    public int variant;
    // variant = 0 regular covid
    // variant = 1 tracker covid
    // variant = 2 speeder covid
    public float pointMultiplier;
    private Rigidbody2D _rigidbody;

    PhotonView view;

    public TextMeshProUGUI scoreText;
    public int scoreValue;
    public AudioSource splat;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        view = GetComponent<PhotonView>();

        startTime = Time.time;
        this.transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
        this.transform.localScale = Vector3.one * this.size;

        _rigidbody.mass = this.size;
    }

    void Update()
    {
        currTime = Time.time;
        if (currTime - startTime > interval)
        {
            startTime = currTime;
            speed += 2.0f;
        }
    }

    public void SetVariant(int n)
    {
        variant = n;
        switch (variant)
        {
            case 0: // normal
                pointMultiplier = 1.0f;
                this.GetComponent<SpriteRenderer>().material.color = Color.white;
                break;

            case 1: // tracker
                gameObject.AddComponent<EnemyFollow>();
                pointMultiplier = 1.6f;
                this.GetComponent<SpriteRenderer>().material.color = Color.red;
                break;

            case 2: // speeder
                speed *= 2;
                pointMultiplier = 2.0f;
                this.GetComponent<SpriteRenderer>().material.color = Color.blue;
                break;
        }
    }

    public void setTrajectory(Vector2 direction)
    {
        if (variant != 1)
        {
            _rigidbody.AddForce(direction * this.speed);
        }

        Destroy(this.gameObject, this.maxLifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        splat.Play();
        if (collision.gameObject.tag == "Vax") //the bullet tag is Vax

        {
            if (variant == 0 && this.size * 0.5f >= this.minSize)
            {
                CreateSplit();
                CreateSplit();
            }

            BulletScript bullet = collision.gameObject.GetComponent<BulletScript>();
            int pointEarned = 0;
            if (this.size < 0.8f)
            {
                pointEarned = 100;
            }
            else if (this.size < 1.2f)
            {
                pointEarned = 50;
            }
            else if (this.size >= 1.2f)
            {
                pointEarned = 25;
            }
            
            FindObjectOfType<GameManager>().CovidDestroyed(this, bullet.player, (int)(pointEarned * pointMultiplier));
            Destroy(this.gameObject);
        }
        if (collision.gameObject.tag == "Player")
        {
            PlayerScript player = collision.gameObject.GetComponent<PlayerScript>();
            FindObjectOfType<GameManager>().CovidDestroyed(this, player, -75);
            Destroy(this.gameObject);
        }
    }

    private void CreateSplit()
    {
        Vector2 position = this.transform.position;
        position += Random.insideUnitCircle * 0.5f;

        Covid half = Instantiate(this, position, this.transform.rotation);
        half.size = this.size * 0.5f;
        half.setTrajectory(Random.insideUnitCircle.normalized);
    }
}

