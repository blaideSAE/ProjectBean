using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerControllerScripts.Firstperson
{
    public class PlayerInteraction : MonoBehaviour
    {
        public float radius, maxDistance;
        private float distance;
        public Transform LookTransform;
        public Vector3 originOffset;
        public InputActionMap inputActionMap; 
        public InputActionAsset inputActionAsset;
        public LayerMask interactables;
        public IInteractable[] iInteractable;

        public LayerMask blockingLayers;
        // Start is called before the first frame update
        void Start()
        {
            inputActionMap = inputActionAsset.actionMaps[0];
            inputActionMap.actions[1].Enable(); 
            inputActionMap.actions[1].performed += OnInteractInput; 
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    
        void OnInteractInput(InputAction.CallbackContext ctx)
        {
            distance = maxDistance;
        
            Ray ray = new Ray(LookTransform.position + originOffset,LookTransform.forward);
            RaycastHit blockingHit;
            if (Physics.Raycast(ray,out blockingHit , maxDistance + radius, blockingLayers, QueryTriggerInteraction.Ignore))
            {
                distance = blockingHit.distance;
            }


            RaycastHit[] sphereCastHits =
                Physics.SphereCastAll(ray, radius, distance, interactables, QueryTriggerInteraction.Ignore);
            if (sphereCastHits.Length > 0)
            {
                RaycastHit closestHit = sphereCastHits[0];
        
                foreach (RaycastHit hit in sphereCastHits)
                {
                    if (hit.distance < closestHit.distance)
                    {
                        closestHit = hit;
                    }
                }

                iInteractable = closestHit.collider.gameObject.GetComponents<IInteractable>();

                foreach ( IInteractable interactable in iInteractable)
                {
                    interactable.OnInteraction();
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (distance > maxDistance || distance == 0)
            {
                distance = maxDistance;
            }

            Gizmos.DrawWireSphere(LookTransform.position + originOffset + (LookTransform.forward * distance), radius);
        }
    }
}
 