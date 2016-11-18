using UnityEngine;
using System.Collections;

public class OrientationTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 direction = Vector3.zero;

        switch (Input.deviceOrientation)
        {
            case DeviceOrientation.Portrait:
                direction = new Vector3(0, -1, 0);
                break;
            case DeviceOrientation.PortraitUpsideDown:
                direction = new Vector3(0, 1, 0);
                break;
            case DeviceOrientation.LandscapeLeft:
                direction = new Vector3(-1, 0, 0);
                break;
            case DeviceOrientation.LandscapeRight:
                direction = new Vector3(1, 0, 0);
                break;
        }

        this.transform.position += direction * Time.deltaTime * 2f;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -10, 10), Mathf.Clamp(transform.position.y, -10, 10));
	}
}
