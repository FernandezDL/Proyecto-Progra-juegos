using UnityEngine;

public class LevelBounds : MonoBehaviour
{
    [SerializeField] private Transform orgTransform;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var cc = other.GetComponent<CharacterController>();
            if (cc != null) cc.enabled = false;

            other.transform.position = orgTransform.position;

            if (cc != null) cc.enabled = true;
        }
    }
}
