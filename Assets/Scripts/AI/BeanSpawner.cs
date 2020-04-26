using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AI;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Random = UnityEngine.Random;

public class BeanSpawner : MonoBehaviour
{
    public Bean beanPrefab;

    public List<Bean> SpawnedBeans;

    public List<BeanQuestion> beanQuestions;
    private List<SpawnPoint> SpawnPoints;
    
    public LayerMask SpawnableSurfaces;
    public LayerMask SpawnObstructors;
    
    public int retryLimit = 30;

    public Vector3 precalculatedBeanExtents;
    public Vector3 ExtentModifier;

    public float spawnAmmount;

    public bool DoSpawn;
    public bool RemoveSpawns;
    public List<Color> colors;

    public List<BeanAnsweredQuestionSet> beanAnsweredQuestionSetPool;
    // Start is called before the first frame update
    public bool spawnOnStart = false;

    public bool randomColor = true;
    public bool randomAnswers = false;
    void Start()
    {
        Vector3 position = new Vector3(0,5,0);
        
        GameObject g = Instantiate(beanPrefab.gameObject, position,transform.rotation);
        precalculatedBeanExtents = g.GetComponent<Collider>().bounds.extents;

        Destroy(g);

        SpawnPoints = GetComponentsInChildren<SpawnPoint>().ToList();
        
        beanAnsweredQuestionSetPool = new List<BeanAnsweredQuestionSet>();
        List<BeanQuestionAndAvailableAnswers> beanQuestionAndAvailableAnswersList = new List<BeanQuestionAndAvailableAnswers>();
        foreach ( BeanQuestion b in beanQuestions)
        {
            beanQuestionAndAvailableAnswersList.Add(new BeanQuestionAndAvailableAnswers(b));
        }
        
        for (var i = 0; i < colors.Count; i++)
        {
            Color color = colors[i];
            List<BeanAnsweredQuestion> answeredQuestions = new List<BeanAnsweredQuestion>();
            {
                if (randomAnswers)
                {
                    foreach (BeanQuestionAndAvailableAnswers b in beanQuestionAndAvailableAnswersList)
                    {
                        int randomIndex = Random.Range(0, b.availableAnsers.Count);
                        answeredQuestions.Add(new BeanAnsweredQuestion(b.question, b.availableAnsers[randomIndex]));
                        b.availableAnsers.RemoveAt(randomIndex);
                    }
                }
                else
                {
                    foreach (BeanQuestion beanQuestion in beanQuestions)
                    {
                        answeredQuestions.Add((new BeanAnsweredQuestion(beanQuestion,beanQuestion.possibleAnswers[i])));
                    }
                }
                
                BeanAnsweredQuestionSet bAQS = new BeanAnsweredQuestionSet(answeredQuestions, color);
                beanAnsweredQuestionSetPool.Add(bAQS);
            }
        }

        if (spawnOnStart)
        {
            DoSpawn = true;
        }
    }
    

    public void Spawn()
    {
        List<Bounds> spawnBounds = new List<Bounds>();

        foreach (SpawnPoint spawnPoint in SpawnPoints)
        {
            spawnBounds.Add(spawnPoint.bounds);
        }
        
        Quaternion rotation = Quaternion.Euler(new Vector3(0, Random.Range(0f, 360f), 0));
        Vector3 position = RandomGroundPointInBounds(precalculatedBeanExtents + ExtentModifier,spawnBounds,rotation);

        if (position != transform.position)
        {
            GameObject g = Instantiate(beanPrefab.gameObject, position, rotation);
            SpawnedBeans.Add(g.GetComponent<Bean>());

            Debug.Log("bean spawned at " + position);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (DoSpawn)
        { 
            SpawnBeans();
            DoSpawn = false;
        }

        if (RemoveSpawns)
        {

            DestroyBeans();
            RemoveSpawns = false;
        }

    }

    public void SpawnBeans()
    {
        for (int i = 0; i < spawnAmmount; i++)
        {
            Spawn();
        }
        StartCoroutine(UpdateBeans());
    }

    public IEnumerator UpdateBeans()
    {
        yield return new WaitForEndOfFrame();
        
        int colorIndex = 0;
        foreach (Bean bean in SpawnedBeans)
        {
            //bean.setMaterialToColor(beanAnsweredQuestionSetPool[colorIndex].color);
            if (randomColor)
            {
                bean.setMaterialToColor(colors[Random.Range(0, colors.Count)]);
            }
            else
            { 
                bean.setMaterialToColor(beanAnsweredQuestionSetPool[colorIndex].color);
            }
            
            bean.dialogue = beanAnsweredQuestionSetPool[colorIndex].answeredQuestions;
            bean.helpful = ( bean.startColor == colors[1]);
            if (colorIndex < beanAnsweredQuestionSetPool.Count - 1)
            {
                colorIndex++;
            }
            else
            {
                colorIndex = 0;
            }
            
        }
    }

    public void DestroyBeans()
    {
        foreach (Bean bean in SpawnedBeans)
        {
            Destroy(bean.gameObject);
        }
        SpawnedBeans.Clear();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(transform.position + new Vector3(0,5,0),(precalculatedBeanExtents + ExtentModifier)*2);
    }

    public Vector3 RandomGroundPointInBounds(Vector3 beanExtents, List<Bounds> spawnBoundsList,Quaternion rotation)
    {
        
        bool clear = false;
        int attempts = 0; 
        Vector3 p = transform.position;
        while (!clear && attempts <= retryLimit)
        {
            Bounds spawnBounds = spawnBoundsList[Random.Range(0, SpawnPoints.Count)]; 
            attempts++;
            float randX = Random.Range(spawnBounds.min.x, spawnBounds.max.x);
            float randZ = Random.Range(spawnBounds.min.z, spawnBounds.max.z);
            Vector3 o = new Vector3(randX,spawnBounds.max.y,randZ);
            Ray ray = new Ray(o,Vector3.down);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit,spawnBounds.size.y *2,SpawnableSurfaces,QueryTriggerInteraction.Ignore))
            {
                Vector3 offsetPosition = hit.point + new Vector3(0,+beanExtents.y,0);
                Bounds prespawnCheckBounds = new Bounds( offsetPosition,beanExtents);


                while (prespawnCheckBounds.Contains(hit.point))
                {
                    offsetPosition += Vector3.up *0.1f; 
                    prespawnCheckBounds.center = offsetPosition;
                }

                if (!Physics.CheckBox(prespawnCheckBounds.center, prespawnCheckBounds.extents,rotation,SpawnObstructors,QueryTriggerInteraction.Ignore))
                {
                    p = offsetPosition;
                    clear = true;  
                }

            }
        }
        return p;
    }
}
