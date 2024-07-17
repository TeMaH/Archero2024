using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject arrow;
    public Transform spotForArrow;
    private GameObject target;
    private GameObject currentArrow;
    List<GameObject> arrowList;
    public float attackDistance = 10.0f;
    public float arrowSpeed = 3.0f;
    bool isMoving;
    public float frequency;
    private float time;
    CharacterController controller;
    public CharacterController CharacterController { get { return controller = controller ?? GetComponent<CharacterController>(); } }
    void Start()
    {
        time = frequency;
        arrowList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        SetTarget();
        if (target != null)
        {
            time += Time.deltaTime;
            if (time > frequency)
            {
                AttackTarget();
                time -= frequency;
            }
            if (currentArrow != null) { Shoot(); }
        }
    }
    public void CheckMoving() 
    {
        if (CharacterController.velocity.magnitude > 0.1)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }
    void AttackTarget() 
    {
        if (target != null && !isMoving)
        {
            Vector3 shotDirection = target.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(shotDirection, Vector3.up);

            currentArrow = Instantiate(arrow, spotForArrow.transform.position, rotation);
            arrowList.Add(currentArrow);
        }
    }
    
    void Shoot() 
    {
        for (int i = 0;i < arrowList.Count; i++) 
        {
            if (arrowList[i] != null)
            {
                arrowList[i].transform.position = Vector3.MoveTowards(arrowList[i].transform.position, target.transform.position, arrowSpeed * Time.deltaTime);
            }
        }
    }
    void SetTarget()
    {
        SearchTarget targetSearch = GetComponent<SearchTarget>();
        target = targetSearch.GetTarget();
    }
}
