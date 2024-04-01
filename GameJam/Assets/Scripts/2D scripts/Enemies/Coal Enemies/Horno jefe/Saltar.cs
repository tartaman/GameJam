using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Saltar : StateMachineBehaviour
{
    [SerializeField] float velocidad;
    [SerializeField] float duracionMaxima;
    [SerializeField] float duracionMinima;
    float tiempoDecido;

    Transform player;
    Horno boss;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        boss = animator.GetComponent<Horno>();
        tiempoDecido = Random.Range(duracionMinima, duracionMaxima);
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.atacando = true;
        if (tiempoDecido <= 0)
        {
            animator.SetBool("saltando", false);
           
            boss.StartCoroutine(boss.WaitToAttack());
        }
        else
        {
            tiempoDecido -= Time.deltaTime;
        }
        Vector2 direccion = new Vector2(player.position.x, animator.transform.position.y);
        animator.transform.position = Vector2.MoveTowards(animator.transform.position, direccion, velocidad * Time.deltaTime);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
