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

public class CloudGenerator : MonoBehaviour {

	private float t = 0f;
	public float T;
	public float a;
	public float A;
	public float v;
	public float V;
	public float lifeTime;

	public GameObject[] clouds;
	
	void Update () {
		this.t += Time.deltaTime;

		if (t > T) {
			this.PopCloud ();
			t = 0f;
		}
	}

	private void PopCloud () {
		int cIndex = Random.Range (0, clouds.Length - 1);

		GameObject go = GameObject.Instantiate (this.clouds[cIndex]);
		Cloud cloud = go.GetComponent<Cloud> ();

		cloud.v = Random.Range (v, V);
		cloud.lifeTime = this.lifeTime;
		cloud.transform.position = this.transform.position;
		cloud.transform.position += Vector3.up * Random.Range (a, A);
	}
}
