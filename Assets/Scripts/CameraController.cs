using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform target;

    void Update()
    {
        if (target == null)
        {
            target = FindObjectOfType<PlayerBrain>()?.transform;
            return;
        }

        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
    }
}
