using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    /*private float length, startPos;
    public GameObject cam;
    public float parallaxEffect;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float temp = cam.transform.position.x * (1 - parallaxEffect);
        float distance = (cam.transform.position.x * parallaxEffect);
        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);
        if(temp > startPos)
        {
            startPos += length;
        } else if (temp < startPos)
        {
            startPos -= length;
        }
    }*/

    public Transform[] backgrounds;
    private float[] parallaxScales;
    public float smoothing = 1f;

    private Transform cam;
    private Vector3 previousCamPos;

    //called before Start(). Great for references
    void Awake()
    {
        // set up camera reference
        cam = Camera.main.transform;
    }

    void Start()
    {
        // Previous frame had current frame's camera position
        previousCamPos = cam.position;

        //assigning coresponding parallaxScales
        parallaxScales = new float[backgrounds.Length];
        for(int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z * -1;
        }
    }

    //called once per frame
    void FixedUpdate()
    {
        // for each background
        for (int i = 0; i < backgrounds.Length; i++)
        {
            //the parallax is the opposite of the camera movment because previous frame multiplied scale
            float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];

            // set a target x position which is the current position plus the parallax
            float backgroundTargetPosX = backgrounds[i].position.x + parallax;

            // create a target position which is the backgorund's current position wwith it's target x position.
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            // fade between current position and the target position usin lerp
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        // set the previous CamPos to the camera's position at the end of the frame
        previousCamPos = cam.position;
    }

}




