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
       
        if (target == null) { FindEnemy(); }
        time += Time.deltaTime;
        if (time > frequency)
        {
            AttackTarget();
            time -= frequency;
        }
        if (currentArrow != null) { Shoot(); }
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
    void FindEnemy() 
    {
        var objects = Physics.OverlapSphere(transform.position, attackDistance);

        for (int i = 0; i < objects.Length; i++)
        {
            if ( objects[i].gameObject.CompareTag("Enemy"))
            {
                target = objects[i].gameObject;
                Debug.Log(target.transform.position);
            }
        }
    }
    void Shoot() 
    {
        for (int i = 0;i < arrowList.Count; i++) 
        {
            if (arrowList[i] != null && target != null)
            {
                arrowList[i].transform.position = Vector3.MoveTowards(arrowList[i].transform.position, target.transform.position, arrowSpeed * Time.deltaTime);
            }
        }
    }
}
