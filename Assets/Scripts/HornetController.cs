using UnityEngine;

public class HornetController : MonoBehaviour
{
    public float speed = 5f; // Velocidad de hornet
    private Vector2 direction; // Dirección de movimiento

    private float spriteWidth;
    private float spriteHeight;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;

        // Dirección inicial aleatoria normalizada
        direction = Random.insideUnitCircle.normalized;

        // Obtener tamaño del sprite
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        spriteWidth = sr.bounds.size.x / 2;
        spriteHeight = sr.bounds.size.y / 2;
    }

    void Update()
    {
        // Mover
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        // Rotar en la dirección del movimiento
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);

        Vector2 pos = transform.position;

        // Calcular límites de cámara
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float cameraHeight = cam.orthographicSize;
        float cameraWidth = cameraHeight * screenRatio;

        // Rebote horizontal
        if (pos.x + spriteWidth > cameraWidth || pos.x - spriteWidth < -cameraWidth)
        {
            direction.x *= -1;
            pos.x = Mathf.Clamp(pos.x, -cameraWidth + spriteWidth, cameraWidth - spriteWidth);
            ChangeColor();
        }

        // Rebote vertical
        if (pos.y + spriteHeight > cameraHeight || pos.y - spriteHeight < -cameraHeight)
        {
            direction.y *= -1;
            pos.y = Mathf.Clamp(pos.y, -cameraHeight + spriteHeight, cameraHeight - spriteHeight);
            ChangeColor();
        }

        transform.position = pos;
    }

    void ChangeColor()
    {
        GetComponent<SpriteRenderer>().color = new Color(Random.value, Random.value, Random.value);
    }
}
