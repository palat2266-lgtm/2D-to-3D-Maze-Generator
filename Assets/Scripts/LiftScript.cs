using UnityEngine;

public class LiftScript : MonoBehaviour
{
    public float speed = 2f;

    float targetY;
    bool move = false;

    public Transform player; // гравець - камера

    void Start()
    {
        targetY = transform.position.y;
    }

    void Update()
    {
        if (move)
        {
            float oldY = transform.position.y;

            // рух ліфта
            transform.position = Vector3.MoveTowards(
                transform.position,
                new Vector3(transform.position.x, targetY, transform.position.z),
                speed * Time.deltaTime
            );

            float deltaY = transform.position.y - oldY;

            // якщо гравець стоїть на ліфті — рухаємо його разом
            if (Mathf.Abs(player.position.y - transform.position.y) < 1f)
            {
                player.position += new Vector3(0, deltaY, 0);
            }

            // якщо доїхав — зупиняємось
            if (Mathf.Abs(transform.position.y - targetY) < 0.01f)
            {
                move = false;
            }
        }
    }

    public void GoUp()
    {
        targetY = 3.5f; // другий поверх
        move = true;
    }

    public void GoDown()
    {
        targetY = 0.5f; // перший поверх
        move = true;
    }
}
