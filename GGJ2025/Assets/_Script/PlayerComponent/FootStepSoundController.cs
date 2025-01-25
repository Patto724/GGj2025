using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FootstepSoundController : MonoBehaviour
{
    public AudioClip[] footstepSounds; // Array of footstep sounds
    public float baseStepInterval = 0.5f; // Base interval between steps at normal speed
    public float speedThreshold = 0.01f; // Minimum speed to play footstep sounds

    public CharacterController _characterController;
    private AudioSource _audioSource;
    private float _stepTimer;

    void Start()
    {
        //_characterController = GetComponent<CharacterController>();
        _audioSource = GetComponent<AudioSource>();
        _stepTimer = 0f;
    }

    void Update()
    {
        PlayFootstepSounds();
    }

    void PlayFootstepSounds()
    {
        // Calculate the character's speed
        float speed = new Vector3(_characterController.velocity.x, 0, _characterController.velocity.z).magnitude;
        // Check if the character is moving above the speed threshold
        if (speed > speedThreshold)
        {
            // Calculate the interval between steps based on speed
            float stepInterval = baseStepInterval / speed;

            // Increment the step timer
            _stepTimer += Time.deltaTime;

            // Play a footstep sound if the timer exceeds the step interval
            if (_stepTimer > stepInterval || _stepTimer == Time.deltaTime)
            {
                PlayRandomFootstep();
                _stepTimer = 0.1f; // Reset the timer
            }
        }
        else
        {
            // Reset the timer if the character is not moving
            _stepTimer = 0f;
        }
    }

    void PlayRandomFootstep()
    {
        if (footstepSounds.Length > 0)
        {
            Debug.Log("play footstep");
            // Select a random footstep sound
            int index = Random.Range(0, footstepSounds.Length);
            _audioSource.PlayOneShot(footstepSounds[index]);
        }
    }
}