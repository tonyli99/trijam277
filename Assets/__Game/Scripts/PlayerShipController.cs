using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

namespace Game
{

    public class PlayerShipController : Ship
    {

        [field: SerializeField] public bool UseMouseControl { get; set; } = true;
        [field: SerializeField] private Toggle UseMouseControlToggle { get; set; }
        [field: SerializeField] public bool UseInstantStop { get; set; } = true;
        [field: SerializeField] private Toggle UseInstantStopToggle { get; set; }
        [field: SerializeField] private TextMeshProUGUI Coordinates { get; set; }
        [field: SerializeField] private Slider HealthSlider { get; set; }

        private Camera myCamera;
        private Transform myCameraTransform;
        private Transform myTransform;
        private Vector3 lastShownCoord;

        protected override void Awake()
        {
            base.Awake();
            myTransform = transform;
        }

        private void Start()
        {
            myCamera = Camera.main;
            myCameraTransform = myCamera.transform;
            UseMouseControlToggle.isOn = UseMouseControl;
            UseMouseControlToggle.onValueChanged.AddListener((x) => UseMouseControl = x);
            UseInstantStopToggle.isOn = UseInstantStop;
            UseInstantStopToggle.onValueChanged.AddListener((x) => UseInstantStop = x);
            HealthSlider.value = 1;
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

            if (vertical < 0 && UseInstantStop)
            {
                InstantStop();
            }
            else
            {
                RB.AddForce(myTransform.up * vertical * ThrustForce * Time.deltaTime);
            }

            if (Input.GetButton(InputAction.Fire) ||
                (UseMouseControl && Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject()))
            {
                TryFire();
            }

            myCameraTransform.position = myTransform.position + Vector3.back;

            var distance = Vector3.Distance(lastShownCoord, myTransform.position);
            if (distance > 0.01f)
            {
                lastShownCoord = myTransform.position;
                Coordinates.text = $"({lastShownCoord.x:0.00},{lastShownCoord.y:0.00})";
            }
        }

        public void InstantStop()
        {
            RB.velocity = Vector3.zero; 
        }

        protected override void OnHurt()
        {
            base.OnHurt();
            HealthSlider.value = (float)CurrentHealth / (float)MaxHealth;
        }

    }

}
