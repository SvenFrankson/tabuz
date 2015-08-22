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

	public TextMesh infoText;

	public float engineThrust = 10f;
	public GameObject leftEngine;
	public MeshRenderer leftEngineRenderer;
	private Vector3 leftEngineLocalPos;
	public GameObject rightEngine;
	public MeshRenderer rightEngineRenderer;
	private Vector3 rightEngineLocalPos;
	private Rigidbody cRigidbody;

	public float squareVelocityThreshold = 4f;

	private bool gameOver = false;

	void OnGUI () {
		GUI.TextArea (new Rect (10f, 10f, 200f, 30f), "Speed = " + this.cRigidbody.velocity.magnitude);
	}

	// Use this for initialization
	void Start () {
		Physics.gravity = 5f * Vector3.down;
		this.cRigidbody = this.GetComponent<Rigidbody> ();
		this.leftEngineLocalPos = this.leftEngine.transform.localPosition - this.transform.right;
		this.leftEngineRenderer = this.leftEngine.GetComponent<MeshRenderer> ();
		this.rightEngineLocalPos = this.rightEngine.transform.localPosition + this.transform.right;
		this.rightEngineRenderer = this.rightEngine.GetComponent<MeshRenderer> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (this.gameOver) {
			return;
		}
		if (Input.GetKey(KeyCode.Q)) {
			this.cRigidbody.AddForceAtPosition (this.engineThrust * this.transform.up, this.transform.position + this.leftEngineLocalPos);
			this.leftEngineRenderer.material.color = Color.red;
		}
		else {
			this.leftEngineRenderer.material.color = Color.blue;
		}

		if (Input.GetKey(KeyCode.D)) {
			this.cRigidbody.AddForceAtPosition (this.engineThrust * this.transform.up, this.transform.position +  this.rightEngineLocalPos);
			this.rightEngineRenderer.material.color = Color.red;
		}
		else {
			this.rightEngineRenderer.material.color = Color.blue;
		}
	}

	void OnCollisionEnter (Collision col) {
		if (this.gameOver) {
			return;
		}

		TerrainBlock block = col.gameObject.GetComponent<TerrainBlock> ();

		if (block == null) {
			return;
		}

		TerrainBlock.BlockType type = block.blockType;

		if (type == TerrainBlock.BlockType.Nature) {
			this.gameOver = true;
			StartCoroutine("WriteInfoTree");
		}
		else if (type == TerrainBlock.BlockType.Building) {
			this.gameOver = true;
			StartCoroutine("WriteInfoBuilding");
		}
		else if (type == TerrainBlock.BlockType.Ground) {
			if (this.cRigidbody.velocity.sqrMagnitude > this.squareVelocityThreshold) {
				this.gameOver = true;
				StartCoroutine("WriteInfoCrash");
			}
			else {
				this.gameOver = true;
				StartCoroutine("WriteInfoSuccess");
			}
		}
	}

	void OnTriggerEnter (Collider col) {
		if (this.gameOver) {
			return;
		}
		
		TerrainBlock block = col.gameObject.GetComponent<TerrainBlock> ();
		
		if (block == null) {
			return;
		}
		
		TerrainBlock.BlockType type = block.blockType;

		if (type == TerrainBlock.BlockType.Water) {
			this.gameOver = true;
			StartCoroutine("WriteInfoWater");
		}
	}

	IEnumerator WriteInfoTree () {
		this.infoText.text = "";
		string message = "FAILURE\nYou were supposed to land,\nnot to nest !";
		int i = 0;

		while (i < message.Length) {
			this.infoText.text += message[i];
			i++;
			yield return null;
		}
		
		yield return new WaitForSeconds(3f);
		Application.LoadLevel("main");
	}
	
	IEnumerator WriteInfoBuilding () {
		this.infoText.text = "";
		string message = "FAILURE\nOups !";
		int i = 0;
		
		while (i < message.Length) {
			this.infoText.text += message[i];
			i++;
			yield return null;
		}
		
		yield return new WaitForSeconds(3f);
		Application.LoadLevel("main");
	}
	
	IEnumerator WriteInfoWater () {
		this.infoText.text = "";
		string message = "FAILURE\nPlouf !";
		int i = 0;
		
		while (i < message.Length) {
			this.infoText.text += message[i];
			i++;
			yield return null;
		}
		
		yield return new WaitForSeconds(3f);
		Application.LoadLevel("main");
	}
	
	IEnumerator WriteInfoCrash () {
		this.infoText.text = "";
		string message = "FAILURE\nToo fast !\nSlow down !";
		int i = 0;
		
		while (i < message.Length) {
			this.infoText.text += message[i];
			i++;
			yield return null;
		}
		
		yield return new WaitForSeconds(3f);
		Application.LoadLevel("main");
	}
	
	IEnumerator WriteInfoSuccess () {
		this.infoText.text = "";
		string message = "WELL DONE !";
		int i = 0;
		
		while (i < message.Length) {
			this.infoText.text += message[i];
			i++;
			yield return null;
		}
		
		yield return new WaitForSeconds(3f);
		Application.LoadLevel("main");
	}
}
