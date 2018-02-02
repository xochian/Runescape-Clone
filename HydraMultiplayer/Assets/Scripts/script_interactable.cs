using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ActionType { Cancel, MoveTo, Smelt, Craft, Cut, Mine, Attack, Forge } 

public class script_interactable : MonoBehaviour
{
    public List<ActionType> availableActions;
    
}

public class script_executeActions
{
    public void Execute(GameObject player, ActionType action, GameObject target = null)
    {
        switch (action)
        {
            case ActionType.Attack:
                Attack(player, target);
                break;
            case ActionType.Mine:
                break;
            case ActionType.Cut:
                break;
            case ActionType.Craft:
                break;
            case ActionType.Smelt:
                break;
            case ActionType.MoveTo:
                break;
            case ActionType.Cancel:
                break;
            default:
                break;
        }
    }

    void Attack(GameObject player, GameObject target)
    {

    }

    void Mine(GameObject player, GameObject target)
    {
        //target.GetComponent<Forge>().smelt(player);
    }

    void Cut(GameObject player, GameObject target)
    {

    }

    void Smelt(GameObject player, GameObject target)
    {
        //target.GetComponent<Forge>().smelt(player);
    }

    void Forge(GameObject player, GameObject target)
    {
        //target.GetComponent<Forge>().smelt(player);

    }

    void MoveTo(GameObject player, GameObject target)
    {
        player.GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
    }

}
