using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piston : MonoBehaviour
{
    [SerializeField]
    private float extendVelocity;

    [SerializeField]
    private float upDelay;

    [SerializeField]
    private float retractTime;

    [SerializeField]
    private float downDelay;

    [SerializeField]
    private float height;

    [SerializeField]
    private float bumpForce;

    private float initialHeight;

    private float timer = 0f;

    private State state;

    private Vector3 initialPosition;

    private List<Collider> inPlatform;

    private enum State
    {
        EXTENDING,
        EXTENDED,
        RETRACTING,
        DOWN,
    }

    void Awake()
    {
        initialPosition = transform.localPosition;
        inPlatform = new List<Collider>();
    }

    void OnCollisionEnter(Collision other)
    {
        inPlatform.Add(other.collider);
    }
    void OnCollisionExit(Collision other)
    {
        inPlatform.Remove(other.collider);
    }

    void FixedUpdate()
    {
        timer += Time.deltaTime;
        
        switch(state)
        {
            case State.EXTENDING:
                float scaled = extendVelocity * timer;
                transform.localPosition = Vector3.Lerp(initialPosition, initialPosition + Vector3.up * height, scaled);
                if (scaled > 1f)
                {
                    inPlatform.ForEach(collider => collider.GetComponent<Rigidbody>().AddForce(Vector3.up * bumpForce));
                    state = State.EXTENDED;
                    timer = 0f;
                }
                break;
            case State.EXTENDED:
                if (timer > upDelay)
                {
                    state = State.RETRACTING;
                    timer = 0f;
                }
                break;
            case State.RETRACTING:
                transform.localPosition = Vector3.Lerp(initialPosition + Vector3.up * height, initialPosition, timer);
                if (timer > 1f)
                {
                    state = State.DOWN;
                    timer = 0f;
                }
                break;

            case State.DOWN:
                if (timer > downDelay)
                {
                    state = State.EXTENDING;
                    timer = 0f;
                }
                break;

        }


    }
}
