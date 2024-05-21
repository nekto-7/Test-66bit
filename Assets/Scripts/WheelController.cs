using UnityEngine;

public class WheelController : MonoBehaviour
{ 
    public AudioClip source;
    public AudioSource audioPlayer;
    [SerializeField] private Transform trPered_L;
    [SerializeField] private Transform trPered_R;
    [SerializeField] private Transform trBack_L;
    [SerializeField] private Transform trBack_R; 
    [SerializeField] private WheelCollider colPered_R;
    [SerializeField] private WheelCollider colPered_L;
    [SerializeField] private WheelCollider colBack_L;
    [SerializeField] private WheelCollider colBack_R;
    [SerializeField] private float Torque = 5000f;
    [SerializeField] private float saveForce;
    [SerializeField] private float scale = 3f;
    [SerializeField] private float maxAngle;
    private AudioSource engineSound;
    private float force;
    private void Start()
    {
        force = saveForce;
        engineSound = Progress.Inst.audioMassive[2];
        Progress.Inst.audioMassive[3].Play();
    }
    private void FixedUpdate()
    {
        // движение
        colBack_L.motorTorque = Input.GetAxis("Vertical") * force;
        colBack_R.motorTorque = Input.GetAxis("Vertical") * force;
        // ускорение
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            force *= 3f;
        }
        else 
        {
            force = saveForce;
        }
        // тормоз
        if (Input.GetKey(KeyCode.Space))
        {
            colPered_L.brakeTorque = Torque;
            colPered_R.brakeTorque = Torque;
            colBack_L.brakeTorque = Torque;
            colBack_R.brakeTorque = Torque;
        }
        else
        {
            colPered_L.brakeTorque = 0f;
            colPered_R.brakeTorque = 0f;
            colBack_L.brakeTorque = 0f;
            colBack_R.brakeTorque = 0f;
        }
        // поворт
        colPered_L.steerAngle = maxAngle * Input.GetAxis("Horizontal");
        colPered_R.steerAngle = maxAngle * Input.GetAxis("Horizontal");
        // анимация вращеня коколес
        RotateWheel(colPered_L, trPered_L);
        RotateWheel(colPered_R, trPered_R);
        RotateWheel(colBack_L, trBack_L);
        RotateWheel(colBack_R, trBack_R);
        // воспроизведение звука
        UpdateEngineSound();
    }
   
    private void RotateWheel(WheelCollider collider, Transform transform)
    {
        Vector3 position;
        Quaternion rotation;
 
        collider.GetWorldPose(out position, out rotation);
 
        transform.rotation = rotation;
        transform.position = position;
    }
    private void UpdateEngineSound()
    {
        float speed = Mathf.Abs(colBack_L.rpm + colBack_R.rpm) / 2;
        engineSound.pitch = Mathf.Clamp(1 + speed / 1000, 1, 2);

        if (Input.GetAxis("Vertical") != 0)
        {
            if (!engineSound.isPlaying)
            {
                engineSound.Play();
            }
        }
        else
        {
            engineSound.Pause();
        }
    }
}