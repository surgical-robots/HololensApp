using UnityEngine;
using System.Collections;

public class stable : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        if (IsTargetVisible())
        {
            UnityEngine.VR.WSA.HolographicSettings.SetFocusPointForFrame(gameObject.transform.position, -Camera.main.transform.forward);
        }
    }

    private bool IsTargetVisible()
    {
        // This will return true if the target's mesh is within the Main Camera's view frustums.
        Vector3 targetViewportPosition = Camera.main.WorldToViewportPoint(gameObject.transform.position);
        return (targetViewportPosition.x > 0.0 && targetViewportPosition.x < 1 &&
            targetViewportPosition.y > 0.0 && targetViewportPosition.y < 1 &&
            targetViewportPosition.z > 0);
    }
}
