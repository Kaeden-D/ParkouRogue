using UnityEngine;

public class ObjectPauser : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;

    [SerializeField]
    private Pausable script;

    // Store physics states to restore them properly later
    private Vector3 savedVelocity;
    private Vector3 savedAngularVelocity;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        FindFirstObjectByType<PauserHandler>().AddPauser(this);
    }

    public void PauseObject()
    {
        // 1. Disable custom update logic & inputs
        if (script != null)
        {
            script.SetPause(true);
            script.enabled = false;
        }

        // 2. Pause animations
        if (animator != null) animator.speed = 0f;

        // 3. Freeze Physics (Save velocity first, then make it kinematic)
        if (rb != null && !rb.isKinematic)
        {
            savedVelocity = rb.linearVelocity; // Note: Use rb.velocity in older Unity versions
            savedAngularVelocity = rb.angularVelocity;
            rb.isKinematic = true;
        }
    }

    public void ResumeObject()
    {
        // 1. Re-enable custom scripts
        if (script != null)
        {
            script.enabled = false;
            script.SetPause(false);
        }

        // 2. Restore animation speed
        if (animator != null) animator.speed = 1f;

        // 3. Restore Physics
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.linearVelocity = savedVelocity;
            rb.angularVelocity = savedAngularVelocity;
        }
    }

}