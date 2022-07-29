using UnityEngine;

namespace MuckShields
{

	public class AnimateShield : MonoBehaviour
	{
		public void Update() {
			if (Input.GetMouseButton(1)) {
         		transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(-1.0926f, -1.136f, 1.1921f) , 3*Time.deltaTime);
			}	
			else {
         		transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(-1.729f, -1.7651f, 1.1921f) , 4*Time.deltaTime);
			}
		}
	}
}
