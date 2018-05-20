using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour {

    public Transform[] parallaxedBackgrounds;
    private float[] parallaxScales;
    public float smoothing = 1f;

    private Transform cam;
    private Vector3 previousCamPos;

    void Awake ()
    {
        cam = Camera.main.transform;
    }

    // Use this for initialization
    void Start()
    {
        previousCamPos = cam.position;

        parallaxScales = new float[parallaxedBackgrounds.Length];

        for (int i = 0; i < parallaxedBackgrounds.Length; i++)
        {
            parallaxScales[i] = parallaxedBackgrounds[i].position.z * -1;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		for (int i = 0; i < parallaxedBackgrounds.Length; i++)
        {
            float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];

            float backgroundTargetPosX = parallaxedBackgrounds[i].position.x + parallax;

            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, parallaxedBackgrounds[i].position.y, parallaxedBackgrounds[i].position.z);

            parallaxedBackgrounds[i].position = Vector3.Lerp(parallaxedBackgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }
        previousCamPos = cam.position;
	}
}
