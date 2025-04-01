using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickmanBehaviourBase : MonoBehaviour
{
    public delegate void OnGroundStatus(bool s);
    public event OnGroundStatus OnGroundStatusChanged;

    public void BroadCastDelegate(bool s)
    {
        OnGroundStatusChanged(s);
    }
}

public class StickmanController : StickmanBehaviourBase
{
    [SerializeField]
    private List<StickmanBone> Muscles;

    public bool Right, Left;

    public Vector2 WalkRightVector, WalkLeftVector;
    public Rigidbody2D rbLeftLeg, rbRightLeg, rbBody,rbRightArm, rbLeftArm;

    public float JumpForce;
    private float moveDelayPointer;
    public float MoveDelay;

    private bool onGround;

    private PlayerInputGiver ourInput;

    private void Start()
    {
        OnGroundStatusChanged += GetGroundedStatus;

        ourInput = GetComponent<PlayerInputGiver>();
    }

    private void Update()
    {
        foreach(StickmanBone muscle in Muscles)
        {
            muscle.ActivateMuscle();
        }

        if(Input.GetKeyDown(ourInput.ThisPlayerData.ThisPlayerControllScheme.Jump) && onGround)
        {
            rbBody.AddForce(Vector3.up * JumpForce, ForceMode2D.Impulse);
            onGround = false;
        }
        if(Input.GetKeyDown(ourInput.ThisPlayerData.ThisPlayerControllScheme.Attack))
        {
            rbRightArm.AddForce(WalkRightVector, ForceMode2D.Impulse);
            rbLeftArm.AddForce(WalkLeftVector, ForceMode2D.Impulse);
        }
        if(Input.GetKeyDown(ourInput.ThisPlayerData.ThisPlayerControllScheme.Right))
        {
            Right = true;
        }
        if(Input.GetKeyDown(ourInput.ThisPlayerData.ThisPlayerControllScheme.Left))
        {
            Left = true;
        }

        if(Input.GetButtonUp("Horizontal"))
        {
            Left = false;
            Right = false;
        }

        while(Right && Left == false && Time.time > moveDelayPointer)
        {
            Invoke("Step1Right", 0f);
            Invoke("Step2Right", 0.085f);
            moveDelayPointer = Time.time + MoveDelay;
        }

        while (Left && Right == false && Time.time > moveDelayPointer)
        {
            Invoke("Step1Left", 0f);
            Invoke("Step2Left", 0.085f);
            moveDelayPointer = Time.time + MoveDelay;
        }
    }

    public void Step1Right()
    {
        rbRightLeg.AddForce(WalkRightVector, ForceMode2D.Impulse);
        rbLeftLeg.AddForce(WalkRightVector * -0.5f, ForceMode2D.Impulse);
    }

    public void Step2Right()
    {
        rbLeftLeg.AddForce(WalkRightVector, ForceMode2D.Impulse);
        rbRightLeg.AddForce(WalkRightVector * -0.5f, ForceMode2D.Impulse);
    }

    public void Step1Left()
    {
        rbRightLeg.AddForce(WalkLeftVector, ForceMode2D.Impulse);
        rbLeftLeg.AddForce(WalkLeftVector * -0.5f, ForceMode2D.Impulse);
    }

    public void Step2Left()
    {
        rbLeftLeg.AddForce(WalkLeftVector, ForceMode2D.Impulse);
        rbRightLeg.AddForce(WalkLeftVector * -0.5f, ForceMode2D.Impulse);
    }

    public void GetGroundedStatus(bool s)
    {
        onGround = s;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var colliders = GetComponentsInChildren<Collider2D>();
        for(int i = 0; i < colliders.Length; i++)
        {
            int c = colliders[i].GetContacts(colliders);
            if (c < 1)
            { 
//                onGround = true;
            }
        }
    }

    private void OnDisable()
    {
        OnGroundStatusChanged -= GetGroundedStatus;
    }
}




[System.Serializable]
public class StickmanBone
{   
    public string name;
    public Rigidbody2D Bone;
    public float RestRotation;
    public float Force = 150f;

    public void ActivateMuscle()
    {
        Bone.MoveRotation(Mathf.LerpAngle(Bone.rotation, RestRotation, Force * Time.deltaTime));
    }

    
}