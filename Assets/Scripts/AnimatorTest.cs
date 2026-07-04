using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorTest : MonoBehaviour
{
    public Animator animator;

    public string triggerToPlay = "Fly";
    public KeyCode keyToTrigger = KeyCode.A;
    public KeyCode keyToExit = KeyCode.S;


    private void OnValidate()
    {
        if (animator == null) animator = GetComponent<Animator>();    
    }

    //PARA O TRIGGER FLY 
    /*void Update()
    {
        if (Input.GetKeyDown(keyToTrigger))
        {
            animator.SetTrigger(triggerToPlay);
        }
    }*/

    //PARA ATIVAR E ENCERRAR O TRIGGER FLY_BOOL COM DUAS TECLAS
    /*void Update()
    {
        if (Input.GetKeyDown(keyToTrigger))
        {
            animator.SetBool(triggerToPlay, true);
        }
        else if (Input.GetKeyUp(keyToExit))
        {
            animator.SetBool(triggerToPlay, false);

        }
    }*/

    //PARA ATIVAR E ENCERRAR O TRIGGER FLY_BOOL COM A MESMA TECLA
    void Update()
    {
        if (Input.GetKeyDown(keyToTrigger))
        {
            animator.SetBool(triggerToPlay, !animator.GetBool(triggerToPlay));
        }
    }

}
