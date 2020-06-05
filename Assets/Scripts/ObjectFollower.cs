using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollower : MonoBehaviour
{
    [SerializeField]
    private GameObject followed;

    [SerializeField]
    private float distanceOffset;

    [SerializeField]
    private float verticalDistanceOffset;

    [SerializeField]
    private Vector3 up;



    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, followed.transform.position + Vector3.up * verticalDistanceOffset - Vector3.forward * distanceOffset, Time.deltaTime);
        transform.LookAt(followed.transform.position, up.normalized);
    }

    private Vector3 XZPosition(GameObject gameObject)
    {
        return XZPosition(gameObject.transform.position);
    }
    private Vector3 XZPosition(Vector3 position)
    {
        return new Vector3(position.x, 0f, position.z);
    }
}
