using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ColorCode : MonoBehaviour
{
    public int[] code;
    public List<int> attempt;
    public SpriteRenderer[] sp;
    public UnityEvent correctEv;
    public UnityEvent incorrectEv;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void correct()
    {
        correctEv?.Invoke();
    }
    public void incorrect()
    {
        incorrectEv?.Invoke();
    }
    public void addCode(int i) {
        attempt.Add(i);
        if (attempt.Count == code.Length) {
            checkCode();
        }
    }

    public void checkCode() {
        for (int i = 0; i < code.Length; i++) { 
        
        }

    }
}
