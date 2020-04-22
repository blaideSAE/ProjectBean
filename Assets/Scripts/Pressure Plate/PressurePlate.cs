using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{
    private List<Bean> beanList;
    public UnityEvent OnBeanRequirementMet;

    public int BeansRequired;

    public int BeansPresent;

    public SkinnedMeshRenderer skinnedMeshRenderer;

    public float belndShapeValue;
    // Start is called before the first frame update
    void Start()
    {
        beanList = new List<Bean>();
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        skinnedMeshRenderer.SetBlendShapeWeight(0,belndShapeValue *100 );
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Bean>() != null)
        {
            Bean b = other.gameObject.GetComponent<Bean>();
            if (!beanList.Contains(b))
            {
                beanList.Add(b);
                if (beanList.Count >= BeansRequired)
                {
                    OnBeanRequirementMet?.Invoke();
                }

                BeansPresent = beanList.Count;
                belndShapeValue = (BeansPresent +0f)/ BeansRequired;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Bean>() != null)
        {
            Bean b = other.gameObject.GetComponent<Bean>();
            if (beanList.Contains(b))
            {
                beanList.Remove(b);
                BeansPresent = beanList.Count;
                belndShapeValue = (BeansPresent+0f)/BeansRequired;
            }
        }
    }
}
