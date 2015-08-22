//	The MIT License (MIT)
//	
//	Copyright (c) 2015 SvenFrankson
//		
//		Permission is hereby granted, free of charge, to any person obtaining a copy
//		of this software and associated documentation files (the "Software"), to deal
//		in the Software without restriction, including without limitation the rights
//		to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//		copies of the Software, and to permit persons to whom the Software is
//		furnished to do so, subject to the following conditions:
//		
//		The above copyright notice and this permission notice shall be included in all
//		copies or substantial portions of the Software.
//		
//		THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//		IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//		FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//		AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//		LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//		OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//		SOFTWARE.

using UnityEngine;
using System.Collections;

public class Soucoup : MonoBehaviour {

	public float engineThrust = 10f;
	public GameObject leftEngine;
	public MeshRenderer leftEngineRenderer;
	private Vector3 leftEngineLocalPos;
	public GameObject rightEngine;
	public MeshRenderer rightEngineRenderer;
	private Vector3 rightEngineLocalPos;
	private Rigidbody rigidbody;

	// Use this for initialization
	void Start () {
		Physics.gravity = 5f * Vector3.down;
		this.rigidbody = this.GetComponent<Rigidbody> ();
		this.leftEngineLocalPos = this.leftEngine.transform.localPosition - this.transform.right;
		this.leftEngineRenderer = this.leftEngine.GetComponent<MeshRenderer> ();
		this.rightEngineLocalPos = this.rightEngine.transform.localPosition + this.transform.right;
		this.rightEngineRenderer = this.rightEngine.GetComponent<MeshRenderer> ();
	}

	void OnGUI () {
		GUI.TextArea (new Rect (10f, 10f, 100f, 30f), this.transform.up + "");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetKey(KeyCode.Q)) {
			this.rigidbody.AddForceAtPosition (this.engineThrust * this.transform.up, this.transform.position + this.leftEngineLocalPos);
			this.leftEngineRenderer.material.color = Color.red;
		}
		else {
			this.leftEngineRenderer.material.color = Color.blue;
		}

		if (Input.GetKey(KeyCode.D)) {
			this.rigidbody.AddForceAtPosition (this.engineThrust * this.transform.up, this.transform.position +  this.rightEngineLocalPos);
			this.rightEngineRenderer.material.color = Color.red;
		}
		else {
			this.rightEngineRenderer.material.color = Color.blue;
		}
	}
}
