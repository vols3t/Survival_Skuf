using UnityEngine;
using UnityEngine.UI;

public class movee : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 0.5f;
    private Vector2 moveVector;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveVector.x = Input.GetAxis("Horizontal");
        moveVector.y = Input.GetAxis("Vertical");
        Flip();
        rb.MovePosition(rb.position + moveVector * speed * Time.deltaTime);
    }

    void Flip()
    {
        if (Input.GetAxis("Horizontal") < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        if (Input.GetAxis("Vertical") > 0)
        {
            transform.localRotation = Quaternion.Euler(180, 0, 0);
        }
        if (Input.GetAxis("Vertical") < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}