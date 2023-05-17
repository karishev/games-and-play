using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject decalPrefab;
    public AudioSource fireSound;
    public Transform fps_cam;
    public float range = 20;
    GameObject[] totalD;
    int actual_d = 0;
    // Start is called before the first frame update
    void Start()
    {
        totalD = new GameObject[10];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(fps_cam.position, fps_cam.forward, out hit, 20))
            {
                Debug.Log("works fine");
                Destroy(totalD[actual_d]);
                totalD[actual_d] = GameObject.Instantiate(decalPrefab, hit.point + hit.normal * 0.01f, Quaternion.FromToRotation(Vector3.forward,-hit.normal)) as GameObject;
                actual_d++;
                if (actual_d == 10) actual_d = 0;
            }
        }
    }
}
