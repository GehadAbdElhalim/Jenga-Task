using UnityEngine;

namespace JengaTask
{
    public class MouseClickChecker : MonoBehaviour
    {
        private Camera mainCamera;

        private void Awake()
        {
            mainCamera = Camera.main;
        }

        private void Update()
        {
            if (GameManager.Instance.IsShowingBlockDetails()) return;

            if (Input.GetMouseButtonDown(1))
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100))
                {
                    IClickable clickable = hit.collider.GetComponentInParent<IClickable>();

                    if (clickable == null) return;

                    clickable.OnClick();
                }
            }
        }
    }
}