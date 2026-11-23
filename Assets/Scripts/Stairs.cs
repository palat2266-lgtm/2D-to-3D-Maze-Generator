using UnityEngine;

public class Stairs : MonoBehaviour
{
    public float height = 5.5f;
    public float speed = 2f;
    public float switchHeight = 2.5f;

    private bool moving;
    private Vector3 targetPosition;
    private Transform player;
    private Rigidbody playerRb;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("MainCamera")) return;

        player = other.transform;
        playerRb = other.GetComponent<Rigidbody>();

        General.onStairs = true; // блокуємо нормальний рух

        // Зупиняємо будь-яку фізичну швидкість, яка могла бути у камери
        if (playerRb != null)
        {
            playerRb.linearVelocity = Vector3.zero;
            playerRb.angularVelocity = Vector3.zero;
        }

        float currentY = player.position.y;
        float dir = (currentY < switchHeight) ? 1f : -1f;

        // Фіксуємо позицію суворо по центру сходів (X та Z), щоб гравця не зміщувало вбік
        targetPosition = new Vector3(
            transform.position.x,
            currentY + (height * dir),
            transform.position.z
        );

        moving = true;
    }

    void Update()
    {
        if (!moving || player == null) return;

        player.position = Vector3.MoveTowards(
            player.position,
            targetPosition,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(player.position, targetPosition) < 0.05f)
        {
            player.position = targetPosition;

            // Знову про всяк випадок обнуляємо фізику після зупинки
            if (playerRb != null)
            {
                playerRb.linearVelocity = Vector3.zero;
                playerRb.angularVelocity = Vector3.zero;
            }

            moving = false;
            General.onStairs = false; // повертаємо контроль гравцю
        }
    }
}