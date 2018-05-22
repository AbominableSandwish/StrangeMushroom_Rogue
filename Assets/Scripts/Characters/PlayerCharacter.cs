using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacter : MonoBehaviour
{
    private const int PERCENT = 100;
    private const int MAX_HEALT = 1000;
    int Healt = 1000;
    public Rigidbody2D Body { get; private set; }
    private Rigidbody2D _rigid;
    private Vector2 VELOCITY_MAX = new Vector2(1.5f, 1.5f);

    [SerializeField] private float speed;
    Text Life;
    // Use this for initialization
    void Start()
    {
        Body = GetComponent<Rigidbody2D>();
        _rigid = gameObject.GetComponent<Rigidbody2D>();
        Life = GameObject.Find("LifeText").GetComponent<Text>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector3(moveHorizontal, moveVertical);

        if(moveHorizontal != 0 && moveVertical != 0)
        {
            movement /= 2;
        }

        _rigid.AddRelativeForce(movement * speed);
        _rigid.velocity = movement*speed;
    }

    public void TakeDamage()
    {
        Healt--;
        Life.text = ((int)Healt / MAX_HEALT * PERCENT).ToString();
    }
}
