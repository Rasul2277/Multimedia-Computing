using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class TransformInfo
{
    public Vector3 position;
    public Quaternion rotation;
}

public class Tree : MonoBehaviour
{
    public GameObject Branch;

    [SerializeField]
    private int generations = 2;

    [SerializeField]
    private float length = 10f;

    [SerializeField]
    private float angle = 30f;

    private Stack<TransformInfo> TransformStack;
    private Dictionary<char, string> rules;

    private const string axiom = "X";
    private string currentString = string.Empty;

    private void Start()
    {
        TransformStack = new Stack<TransformInfo>();

        rules = new Dictionary<char, string>
        {
            { 'X', "[<F+[[>X]-X]-<F[-F>X]+>X]"},
            { 'F', "FF" }

        };

        Generate();
    }

    private void Generate()
    {
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
        

        

        foreach (char c in currentString)
        {
            switch (c)
            {
                case 'F':
                    Vector3 InitialPosition = transform.position;
                    transform.Translate(Vector3.up * length);
                    GameObject treeSegment = Instantiate(Branch);
                    treeSegment.GetComponent<LineRenderer>().SetPosition(0, InitialPosition);
                    treeSegment.GetComponent<LineRenderer>().SetPosition(1, transform.position);

                    break;


                case 'X':
                    
                    
                    break;

                case '+':
                    transform.Rotate(Vector3.back * angle);
                    break;

                case '-':
                    transform.Rotate(Vector3.forward * angle);
                    break;

                case '<':
                    transform.Rotate(Vector3.up * angle);
                    break;

                case '>':
                    transform.Rotate(Vector3.down * angle);
                    break;

                case '[':
                   TransformStack.Push(new TransformInfo()
                   {
                        position = transform.position,
                        rotation = transform.rotation
                   });

                    break;
                case ']':
                    TransformInfo ti = TransformStack.Pop();
                    transform.position = ti.position;
                    transform.rotation = ti.rotation;
                    break;

                default:
                    break;
            }
        }
    }
}
