using UnityEngine;

public class ChekFlipCar : MonoBehaviour
{
    [SerializeField] private float flipThresholdAngle = 45f; 
    [SerializeField] private float resetHeight = 1.0f;       

    private void Update()
    {
        if (IsCarFlipped())
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                ResetCarPosition();
            }
        }
    }

    private bool IsCarFlipped()
    {
        // Получаем угол между вектором вверх машины и вектором вверх мира
        float angle = Vector3.Angle(transform.up, Vector3.up);
        return angle > flipThresholdAngle;
    }

    private void ResetCarPosition()
    {
        transform.position += Vector3.up * resetHeight;
        transform.rotation = Quaternion.identity;
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
