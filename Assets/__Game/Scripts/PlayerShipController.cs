using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Game
{

    public class PlayerShipController : Ship
    {

        [field: SerializeField] public bool UseMouseControl { get; set; } = true;
        [field: SerializeField] private Toggle UseMouseControlToggle { get; set; }

        private Camera myCamera;
        private Transform myCameraTransform;
        private Transform myTransform;
        private Rigidbody2D rb;

        private void Awake()
        {
            myTransform = transform;
            rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            myCamera = Camera.main;
            myCameraTransform = myCamera.transform;
            UseMouseControlToggle.isOn = UseMouseControl;
            UseMouseControlToggle.onValueChanged.AddListener((x) => UseMouseControl = x);
        }

        private void FixedUpdate()
        {
            if (UseMouseControl)
            {
                var mouseWorldPos = myCamera.ScreenToWorldPoint(Input.mousePosition);
                var dir = mouseWorldPos - transform.position;
                myTransform.rotation = Quaternion.LookRotation(Vector3.forward, dir);
            }

            var horizontal = Input.GetAxis(InputAction.Horizontal);
            var vertical = Input.GetAxis(InputAction.Vertical);

            transform.Rotate(0, 0, -horizontal * RotationSpeed * Time.deltaTime);
            rb.AddForce(myTransform.up * vertical * ThrustForce * Time.deltaTime);

            if (Input.GetButton(InputAction.Fire) ||
                (UseMouseControl && Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject()))
            {
                TryFire();
            }

            myCameraTransform.position = myTransform.position + Vector3.back;
        }

    }

}
