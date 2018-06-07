using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class WebCamScript : MonoBehaviour {

	private WebCamTexture webCameraTexture; 
	private Texture2D texture;
	public Color32[] data;
	public bool Tracking = true;


	// Use this for initialization
	void Start () {
		WebCamDevice[] cam_devices = WebCamTexture.devices;
		webCameraTexture = new WebCamTexture(cam_devices[0].name, 480, 640, 30);

		webCameraTexture.Play();

		if (Application.isMobilePlatform) {
			GameObject cameraParent = new GameObject ("camParent");
			cameraParent.transform.position = this.transform.position;
			this.transform.parent = cameraParent.transform;
			cameraParent.transform.Rotate (Vector3.right, 90);      
		}
		Input.gyro.enabled = true;
	}

	// Update is called once per frame
	void Update () {
		texture = new Texture2D(webCameraTexture.width, webCameraTexture.height, TextureFormat.ARGB32, false);
		Color32[] textureData = webCameraTexture.GetPixels32();

		texture.SetPixels32(textureData);
		texture.Apply();


		for (int i =0 ; i < 100; i++) {
			for (int j = 0; j < 100; j++) {
				texture.SetPixel (i, j, Color.blue);
			}
		}
		GetComponent<Renderer> ().material.mainTexture = texture;
		texture.Apply ();
	
	}
}
