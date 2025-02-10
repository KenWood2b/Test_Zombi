using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Персонаж, за которым следит камера
    public Vector3 offset; // Смещение камеры относительно персонажа
    public float smoothSpeed = 0.125f; // Скорость плавного следования

    void LateUpdate()
    {
        if (target != null)
        {
            // Целевая позиция камеры
            Vector3 desiredPosition = target.position + offset;

            // Плавное следование за персонажем
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Обновляем позицию камеры
            transform.position = smoothedPosition;
        }
    }
}
