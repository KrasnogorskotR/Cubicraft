using UnityEngine;
using System.Collections;

public class DynamicDOF : MonoBehaviour
{

    public GameObject lookingPlace;

    public UnityStandardAssets.ImageEffects.DepthOfField dofScript;
    [Space(10)]
    public float maxRange = 30;
    [Space(10)]
    public float focalSize = 0.05F;
    public float aperture = 0.35F;
    [Space(10)]
    public bool active = true;

    // Use this for initialization
    void Start()
    {
        dofScript.focalTransform = lookingPlace.transform;
        dofScript.aperture = aperture;
        dofScript.focalSize = focalSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (active == true)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, maxRange))
            {
                Physics.Raycast(transform.position, transform.forward, out hit, maxRange);
                lookingPlace.transform.position = hit.point;
                dofScript.focalTransform = lookingPlace.transform;
                dofScript.aperture = aperture;
                dofScript.focalSize = focalSize;
            }
            else
            {
                dofScript.focalTransform = null;
                dofScript.aperture = 0;
                dofScript.focalSize = 2;
            }
        }
        else
        {
            dofScript.focalTransform = null;
            dofScript.aperture = 0;
            dofScript.focalSize = 2;
        }
    }
}
