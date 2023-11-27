using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UIElements;

public class TransformInfo
{
    public Vector3 position;
    public Quaternion rotation;
    public int generation = 0;
}

public class Tree : MonoBehaviour
{
    public GameObject Branch;

    public int generations = 2;

    public float length = 10f;

    public float angle = 30f;
    public int maxTree;

    public float width = 1;
    public float coeficent = 1;
    public GameObject Trees;

    private Stack<TransformInfo> TransformStack;
    private Dictionary<char, string> rules;

    private const string axiom = "X";
    private string currentString = string.Empty;
    private bool newGeneration = true;

    private GameObject plant;
    public GameObject[] plants;


    private void Start()
    {
        plants = new GameObject[maxTree];
    }

    public void Generate(Vector3 position)
    {
        plant = Instantiate(new GameObject("Tree"), position, new Quaternion(), Trees.transform);
      
        TransformStack = new Stack<TransformInfo>();

        rules = new Dictionary<char, string>
        {
            { 'X', "[<F+[[>X]-X]-<F[-F>X]+>X]"},
            { 'F', "FF" }

        };

        currentString = axiom;
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < generations; i++)
        {
            foreach (char c in currentString)
            {
                sb.Append(rules.ContainsKey(c) ? rules[c] : c.ToString());
            }

            currentString = sb.ToString();

            sb = new StringBuilder();
        }
        


        int gen = 0;
        foreach (char c in currentString)
        {
            switch (c)
            {
                case 'F':
                    Vector3 InitialPosition = plant.transform.position;
                    plant.transform.Translate(Vector3.up * length);
                    GameObject treeSegment = Instantiate(Branch, plant.transform);
                    treeSegment.GetComponent<LineRenderer>().SetPosition(0, InitialPosition);
                    treeSegment.GetComponent<LineRenderer>().SetPosition(1, plant.transform.position);
                    
                    treeSegment.GetComponent<LineRenderer>().startWidth = newGeneration ? 
                        coeficent * (generations + 3 - (gen)) * width :
                        coeficent * (generations + 3 - (gen + 1)) * width;
                    treeSegment.GetComponent<LineRenderer>().endWidth = coeficent * (generations + 3 - (gen + 1)) * width;
                    newGeneration = false;
                    break;

                case 'X':
                    
                    
                    break;

                case '+':
                    plant.transform.Rotate(Vector3.back * angle);
                    break;

                case '-':
                    plant.transform.Rotate(Vector3.forward * angle);
                    break;

                case '<':
                    plant.transform.Rotate(Vector3.up * angle);
                    break;

                case '>':
                    plant.transform.Rotate(Vector3.down * angle);
                    break;

                case '[':
                    gen++;
                   TransformStack.Push(new TransformInfo()
                   {
                        position = plant.transform.position,
                        rotation = plant.transform.rotation,
                        generation = gen
                   });
                    newGeneration = true;
                    break;
                case ']':
                    gen--;
                    
                    TransformInfo ti = TransformStack.Pop();
                    plant.transform.position = ti.position;
                    plant.transform.rotation = ti.rotation;
                    
                    break;

                default:
                    break;
            }
        }

        for (int i = 0; i < maxTree; i++)
        {
            if (plants[i] == null)
            {
                plants[i] = plant;
                Debug.Log(i);
                break;
            }
        }
    }

    public void Delete()
    {
        for (int i = 0; i < plants.Length; i++)
        {
            Destroy(plants[i]);
            plants[i] = null;
        }
        
    }
}
