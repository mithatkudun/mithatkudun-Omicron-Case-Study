using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingShotRubber : MonoBehaviour
{
    Touch touch;
    public Transform ForDots;
    public float power = 5f;
    private Vector3 startPos;
    private Vector3 endPos;
    private Vector3 forceAtPlayer;
    private GameObject[] trajectoryDots;
    public GameObject trajectoryDot;
    public int numberOfDots;
    public float forceFactor;
    public GameObject SlingShotRubberBand;
    public Rigidbody Manrb;
    Vector3 LastPosition;
    Vector3 FirstPosition;
    private Vector3 mOffset;
    private float mZCoord;
    public Animator anim;
    public GameObject Man2;
    public Rigidbody Man2rb;
    private bool Man2enable;
    [HideInInspector] public bool ShootisEnable;
    public GameObject TaptoStartPanel;
    public int shotscount = 2;
    
    private void Start()
    {
        ShootisEnable = true;
        FirstPosition = SlingShotRubberBand.transform.position;
        trajectoryDots = new GameObject[numberOfDots];
        SpawnTrajectoryDots();
        Man2enable = false;
    }
    public void Update()
    {
        if (TaptoStartPanel.activeInHierarchy==false)
        {
            StartCoroutine("UserInputDelay");
        }
    }
    public void UserInput()
    {          
            if (ShootisEnable == true)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    DrawStart();
                }
                if (Input.GetMouseButton(0))
                {
                    Release();
                }
                if (Input.GetMouseButtonUp(0))
                {
                    Shoot();
                }
            }
            if (ShootisEnable == false)
            {
                anim.Play("GetReady");
                StartCoroutine("Man2position");
            }
    }
    public Vector3 GetMouseAsWorldPoint()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    public void DrawStart()
    {
        
        mZCoord = Camera.main.WorldToScreenPoint(SlingShotRubberBand.transform.position).z;
        mOffset = SlingShotRubberBand.transform.position - GetMouseAsWorldPoint();
        startPos = mOffset+ new Vector3(0,1f,0f);
        TrajectoryDotsActiveState(true);
        
    }
    public void Release()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = mZCoord;
        endPos = Camera.main.ScreenToWorldPoint(mousePos);
        forceAtPlayer = (endPos - startPos);
        CalculateDotsPosition();
        SlingShotRubberBand.transform.position = LastPosition;
        transform.position = GetMouseAsWorldPoint() + mOffset;
 
    }
     public void Shoot()
    {
        Vector3 forceAtPlayer = (startPos - endPos);
        Manrb.isKinematic = false;
        Vector3 clampedForce = Vector3.ClampMagnitude(forceAtPlayer, forceFactor) * power;
        Manrb.AddForce(clampedForce, ForceMode.Impulse);
        TrajectoryDotsActiveState(false);
        SlingShotRubberBand.transform.position = (FirstPosition);
        ShootisEnable = false;
        if (Man2enable == true)
        {   
            Man2rb.isKinematic=false;
            Man2rb.AddForce(clampedForce, ForceMode.Impulse);
            ShootisEnable = true;
            StopCoroutine("Man2position");
            StopCoroutine("UserInputDelay");
        }
        shotscount--;
    }
    IEnumerator Man2position()
    {
        yield return new WaitForSeconds(0.4f);
        anim.enabled = false;
        Man2.transform.position = new Vector3(0.009f, 4.665f, 0.467f);
        Man2.transform.rotation = Quaternion.Euler(0, 0, 0);
        Man2.transform.parent = this.transform;
        ShootisEnable = true;
        Man2enable = true;
    }
    IEnumerator UserInputDelay()
    {
        yield return new WaitForSeconds(0.2f);
        UserInput();  
    }
    private void SpawnTrajectoryDots()
    {
        for (int i = 0; i < numberOfDots; i++)
        {
            trajectoryDots[i] = Instantiate(trajectoryDot, ForDots);
            (trajectoryDots[i] as GameObject).transform.parent = ForDots.transform;
        }
    }

    private void TrajectoryDotsActiveState(bool activeState)
    {
        for (int i = 0; i < numberOfDots; i++)
        {
            trajectoryDots[i].SetActive(activeState);
        }
    }

    private void CalculateDotsPosition()
    {
        for (int i = 0; i < numberOfDots; i++)
        {
            trajectoryDots[i].transform.position = CalculatePosition(i * -0.2f);
        }
    }

    private Vector3 CalculatePosition(float elapsedTime)
    {
        return ForDots.transform.position + new Vector3(forceAtPlayer.x * forceFactor, forceAtPlayer.y * forceFactor, -10) * elapsedTime + 0.1f * Physics.gravity * elapsedTime * elapsedTime;
    }
}



