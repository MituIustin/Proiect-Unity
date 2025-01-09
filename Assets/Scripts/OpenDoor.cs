using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public bool isLocked = true;
    bool isOpened = false;

    Rigidbody rb;
    HingeJoint joint;
    JointLimits jointLimits;
    float currentLim = 0f;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        joint = GetComponent<HingeJoint>();
    }

    void Update()
    {
        if (!isLocked)
        {
            if (!isOpened)
            {
                isOpened = true;
                currentLim = 85f;
                rb.AddRelativeTorque(new Vector3(0, 0, 30f));
            }
        }
    }

    private void FixedUpdate()
    {
        jointLimits.max = currentLim;
        jointLimits.min = -currentLim;
        joint.limits = jointLimits;
    }
}
