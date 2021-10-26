using UnityEngine;

public class InfoBox : MonoBehaviour
{
    private Camera cam;
    public GameObject oldTarget;
    public GameObject currentTarget;

    private void Awake() => cam = GameObject.Find("MainCamera").GetComponent<Camera>();
    void FixedUpdate() => RegisterTarget();
    bool RegisterTarget()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (!hit.collider.GetComponent<Node>())
            {
                return false;
            }
            currentTarget = hit.transform.gameObject;

            return true;
        }
        else currentTarget = null;
        return false;
    }
}
