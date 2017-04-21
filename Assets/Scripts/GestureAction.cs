using UnityEngine;

/// <summary>
/// GestureAction performs custom actions based on 
/// which gesture is being performed.
/// </summary>
public class GestureAction : MonoBehaviour
{
    [Tooltip("Rotation max speed controls amount of rotation.")]
    public float RotationSensitivity = 10.0f;

    private Vector3 manipulationPreviousPosition;

    private float rotationFX;
    private float rotationFY;
    private float rotationFZ;

    void Update()
    {
        PerformRotation();
    }

    private void PerformRotation()
    {
        if (GestureManager.Instance.IsNavigating &&
            (!ExpandModel.Instance.IsModelExpanded ||
            (ExpandModel.Instance.IsModelExpanded && HandsManager.Instance.FocusedGameObject == gameObject)))
        {
            /* TODO: DEVELOPER CODING EXERCISE 2.c */

            // 2.c: Calculate rotationFactor based on GestureManager's NavigationPosition.X and multiply by RotationSensitivity.
            // This will help control the amount of rotation.
            rotationFX = GestureManager.Instance.NavigationPosition.x * RotationSensitivity;
            rotationFY = GestureManager.Instance.NavigationPosition.y * RotationSensitivity;
            rotationFZ = GestureManager.Instance.NavigationPosition.z * RotationSensitivity;

            // 2.c: transform.Rotate along the Y axis using rotationFactor.
            GameObject newG = GameObject.Find("Managers");
            ManifyModel newMa = newG.GetComponent<ManifyModel>();
            newMa.rotateFx = rotationFX;
            newMa.rotateFy = rotationFY;
            newMa.rotateFz = rotationFZ;


        }
    }

    void PerformManipulationStart(Vector3 position)
    {
        manipulationPreviousPosition = position;
    }

    void PerformManipulationUpdate(Vector3 position)
    {
        if (GestureManager.Instance.IsManipulating)
        {
            /* TODO: DEVELOPER CODING EXERCISE 4.a */

            Vector3 moveVector = Vector3.zero;
            // 4.a: Calculate the moveVector as position - manipulationPreviousPosition.
            moveVector = position - manipulationPreviousPosition;
            // 4.a: Update the manipulationPreviousPosition with the current position.
            manipulationPreviousPosition = position;

            // 4.a: Increment this transform's position by the moveVector.

            GameObject newG = GameObject.Find("Managers");
            ManifyModel newMa = newG.GetComponent<ManifyModel>();
            int cm = newMa.currentModel;//current model no.
            if (cm == 0)
            {
                GameObject newG1 = GameObject.Find("video");
                newG1.transform.position += moveVector;
            }
            if (cm == 1)
            {
                GameObject newG1 = GameObject.Find("body");
                newG1.transform.position += moveVector;
            }
            if (cm == 2)
            {
                GameObject newG1 = GameObject.Find("robot");
                newG1.transform.position += moveVector;
            }

        }
    }
}