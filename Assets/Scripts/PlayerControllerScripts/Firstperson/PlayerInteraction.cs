using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerControllerScripts.Firstperson
{
    public class PlayerInteraction : MonoBehaviour
    {
        public float radius, maxDistance;
        private float distance;
        public Transform lookTransform;
        public Vector3 originOffset;
        public InputActionMap inputActionMap; 
        public InputActionAsset inputActionAsset;
        public LayerMask interactables;
        public IInteractable[] iInteractable;
        public Vector3 closestInteractable;

        public LayerMask blockingLayers;
        // Start is called before the first frame update
        void Start()
        {
            inputActionMap = inputActionAsset.actionMaps[0];
            inputActionMap.actions[1].Enable(); 
            inputActionMap.actions[1].performed += OnInteractInput; 
        }

        // Update is called once per frame
        void FixedUpdate()
        {
         FindInteractables();
        }

        void OnInteractInput(InputAction.CallbackContext ctx)
        {
            foreach ( IInteractable interactable in iInteractable)
            {
                interactable.OnInteraction();
            }
        }

        void FindInteractables()
        {
            distance = maxDistance;
        
            Ray ray = new Ray(lookTransform.position + originOffset,lookTransform.forward);
            RaycastHit blockingHit;
            if (Physics.Raycast(ray,out blockingHit , maxDistance + radius, blockingLayers, QueryTriggerInteraction.Ignore))
            {
                distance = blockingHit.distance;
            }


            RaycastHit[] sphereCastHits =
                Physics.SphereCastAll(ray, radius, distance, interactables, QueryTriggerInteraction.Ignore);
            if (sphereCastHits.Length > 0)
            {
               

                iInteractable = ClosestToRay(sphereCastHits,ray).collider.gameObject.GetComponents<IInteractable>();
                //iInteractable = ClosestToPoint(sphereCastHits,blockingHit.point).collider.gameObject.GetComponents<IInteractable>();
               closestInteractable = ClosestToRay(sphereCastHits, ray).collider.gameObject.transform.position;
            }
            else
            {
                iInteractable = new IInteractable[0];
            }
        }

        float DistanceFromRay(Ray ray, Vector3 point)
        {
            return Vector3.Cross(ray.direction, point - ray.origin).magnitude;
        }

        
        //Does not work yet.
        RaycastHit ClosestToRay(RaycastHit[] raycastHits,Ray ray)
        {
            RaycastHit closestHit = raycastHits[0];
            float distancefromRay = DistanceFromRay(ray,closestHit.point);
            
            foreach (RaycastHit hit in raycastHits)
            {
                if (DistanceFromRay(ray,hit.point) < distancefromRay)
                {
                    closestHit = hit;
                    distancefromRay = DistanceFromRay(ray, closestHit.point);
                }
            }

            return closestHit;
        }
        RaycastHit ClosestToPoint(RaycastHit[] raycastHits,Vector3 point)
        {
            RaycastHit closestHit = raycastHits[0];
            float distancefromPoint = Vector3.Distance(closestHit.point,point);
            
            foreach (RaycastHit hit in raycastHits)
            {
                if ( Vector3.Distance(hit.point,point)  < distancefromPoint)
                {
                    closestHit = hit;
                    distancefromPoint = Vector3.Distance(closestHit.point,point);
                }
            }

            return closestHit;
        }
        

        private void OnDrawGizmosSelected()
        {
           // Gizmos.DrawWireSphere(lookTransform.position + originOffset + (lookTransform.forward * distance), radius);
           // Gizmos.DrawRay(lookTransform.position + originOffset,(lookTransform.forward * distance));
           // Gizmos.DrawSphere(lookTransform.position + originOffset + (lookTransform.forward * distance),0.01f);
            Gizmos.DrawWireSphere(closestInteractable,0.1f);
        }
    }
}
 