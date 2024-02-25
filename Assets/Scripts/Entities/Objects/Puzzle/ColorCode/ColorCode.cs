using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ColorCode : MonoBehaviour
{
    public int[] code;
    public List<int> attempt;
    public SpriteRenderer[] disp;
    public UnityEvent correctEv;
    public UnityEvent incorrectEv;
    private bool checking = false;
    // Start is called before the first frame update
    void Start()
    {
        foreach (SpriteRenderer sr in disp)
        {
            sr.color = new Color(1, 1, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void correct()
    {
        foreach (SpriteRenderer sr in disp)
        {
            sr.color = new Color(0, 1, 0);
        }
        correctEv?.Invoke();
    }
    public void incorrect()
    {
        checking = false;
        attempt = new List<int>();
        foreach (SpriteRenderer sr in disp) {
            sr.color = new Color(1, 1, 1);
        }
        incorrectEv?.Invoke();
    }
    public void addCode(int i) {
        if (!checking)
        {
            attempt.Add(i);
            if (attempt.Count == code.Length)
            {
                StartCoroutine(checkCode());
            }
        }
    }
    public void addCode(SpriteRenderer r)
    {
        if (!checking)
        {
            disp[attempt.Count-1].color = r.color;
        }
    }
    public IEnumerator checkCode() {
        bool isCorrect = true;
        for (int i = 0; i < code.Length; i++)
        {
            if (code[i] != attempt[i])
            {
                isCorrect = false;
                break;
            }
        }

        yield return new WaitForSeconds(1);
        if (isCorrect)
        {
            correct();
        }
        else {
            incorrect();
        }
        
    }
}
