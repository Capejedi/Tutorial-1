using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI helText;
    public GameObject winTextObject;
    public GameObject perTextObject;
    public GameObject endTextObject;
    public GameObject player;

    private Rigidbody rb;
    private int count;
    private int health;
    private float movementX;
    private float movementY;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        health = 3;

        SetCountText();
        SetHealthText();
        winTextObject.SetActive(false);
        perTextObject.SetActive(false);
        endTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 21)
        {
            winTextObject.SetActive(true);
        }
        if (count >= 24)
        {
            perTextObject.SetActive(true);
        }

    }

    void SetHealthText()
    {
        helText.text = "Health: " + health.ToString();
        if (health <= 0)
        {
            player.gameObject.SetActive(false);
            endTextObject.SetActive(true);
        }
    }
    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;

            SetCountText();
        }
        if (other.gameObject.CompareTag("Trap"))
        {
            other.gameObject.SetActive(false);
            health = health - 1;

            SetHealthText();
        }
        if (count == 14)
        {
            transform.position = new Vector3(50.0f, 0.0f, 0.0f);
        }

    }
}
