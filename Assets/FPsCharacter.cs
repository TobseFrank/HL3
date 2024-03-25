using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class FPsCharacter : MonoBehaviour
{
    public GameObject boxPrefab;
    public GameObject spherePrefab;
    public GameObject bombPrefab;
    public float throwForce;
    public float pullForce;
    public Camera cam;
    public GameObject hand;
    public float maxDistance;
    public LayerMask mask;
    public float forceDistance;

    private GameObject objInHand;

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.G))
        {
            GameObject box = Instantiate(boxPrefab,hand.transform.position,Quaternion.identity);
            box.GetComponent<Rigidbody>().AddForce(throwForce*cam.transform.forward,ForceMode.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            GameObject ball = Instantiate(spherePrefab,hand.transform.position,Quaternion.identity);
            ball.GetComponent<Rigidbody>().AddForce(throwForce*cam.transform.forward,ForceMode.Impulse);
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            if (objInHand == null) {
                Ray ray = new Ray(cam.transform.position,cam.transform.forward);
                Debug.DrawLine(ray.origin, ray.GetPoint(maxDistance), Color.blue);

                if (Physics.Raycast(ray,out RaycastHit hitInfo,maxDistance,mask)){
                    if (Vector3.Distance(hand.transform.position,hitInfo.transform.position) > forceDistance) {
                        GameObject obj = hitInfo.transform.gameObject;
                        Vector3 normArrtDir = (hand.transform.position - obj.transform.position).normalized;

                        obj.GetComponent<Rigidbody>().AddForce(normArrtDir * pullForce,ForceMode.Impulse);
                    } else {
                        objInHand = hitInfo.transform.gameObject;
                        objInHand.transform.position = hand.transform.position;
                        objInHand.GetComponent<Rigidbody>().isKinematic = true;
                        objInHand.transform.parent = hand.transform;
                    }
                    
                   
                }
            }       
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (objInHand)
            {
                objInHand.transform.parent = null;
                Rigidbody rigidBody = objInHand.GetComponent<Rigidbody>();
                rigidBody.isKinematic = false;
                rigidBody.AddForce(throwForce*cam.transform.forward,ForceMode.Impulse);
                objInHand = null;
            } else {
                GameObject bomb = Instantiate(bombPrefab,hand.transform.position,Quaternion.identity);
                bomb.GetComponent<Rigidbody>().AddForce(throwForce*cam.transform.forward,ForceMode.Impulse);
            }
        }
    }
}
