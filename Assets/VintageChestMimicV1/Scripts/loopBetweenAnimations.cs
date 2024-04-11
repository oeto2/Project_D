/// <summary>
/// 
/// Loop between animations.
/// By: Sergi Nicolas
/// 
/// Description:
/// 
/// Small script to make all the animations repeating in a infinite loop. We use a generic list because it is an easy way to 
/// go trough all the animations array by index.
/// 
/// Intructions:
/// 
/// Just put this script to the GameObject where the animation component is attached.
/// 
/// Then, you can check/uncheck serialized field "looping" in order to activate/deactivate looping effect.
/// 
/// </summary>


using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class loopBetweenAnimations : MonoBehaviour {
	
	private List<string> animations = new List<string>();
	private int numClips;
	private int index = 0; //Current animation clip
	
	[SerializeField] private bool looping;
	

	void Start () {
		
		
		//Let's store all the animation strings into the list
		foreach(AnimationState state in GetComponent<Animation>())
			
   		{
      		animations.Add(state.name);
		
  		 }
		
	
		numClips = animations.Count-1;
		
		//And now, it is time to begin with the clip loop 
		StartCoroutine (repeatAnims ());
	
	}
	
	private IEnumerator repeatAnims(){

		yield return new WaitForSeconds (1.5f);

		while (index <= 1){
	
			for (int x = index; x <= numClips; ++x){
			
				GetComponent<Animation>().Play (animations[index]);
				
				yield return new WaitForSeconds (GetComponent<Animation>()[animations[index]].clip.length);
				Debug.Log (index);
				
				
				if ( !looping && index == numClips) {
				
					//End of loop. Clear list and leave coroutine...
					animations.Clear();
					yield break;
					
				}else if (looping && index == numClips) {
				
					index = 0;
					break;
					
				}
				index++;

			}
	
		}

	
	}


}
