using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnbasedCombat
{



    public class CombatManager : MonoBehaviour
    {




        public static CombatManager _instance;
        public static CombatManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = GameObject.FindObjectOfType<CombatManager>();

                    if (_instance == null)
                    {
                        GameObject container = new GameObject("CombatManager");
                        _instance = container.AddComponent<CombatManager>();
                    }
                }

                return _instance;
            }
        }

        public int nTurns;
        public List<Turn> allTurns;
        public Turn currentTurn;


        //Team actors must contain TurnbasedActorComponent
        public TurnbasedActor[] actorsTeam1;
        public TurnbasedActor[] actorsTeam2;

        public void NextTurn()
        {
            currentTurn = new Turn(allTurns.Count);
            allTurns.Add(currentTurn);
        }

        public void doAction(Action newAction)
        {
            currentTurn.actions.AddLast(newAction);
        }

        // Use this for initialization
        void Start()
        {
            allTurns = new List<Turn>();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }


    //Turn stores the actions taken in a single turn 
    public class Turn
    {
        public bool mainActionComplete;

        public Turn(int id)
        {
            TurnID = id;
            actions = new LinkedList<Action>();
        }

        public int TurnID;
        public LinkedList<Action> actions;
    }

    //Action is the base class for all actions (attacking, abilities,...)
    [System.Serializable]
    public class Action
    {
        public float probability;
        public bool canTargetEnemies;
        public bool canTargetFriendlies;
        public bool canTargetSelf;

        //TODO: Maybe return result in an Object?
        public virtual ActionResult Do() {
            return null;
        }
        //Base components that are shared between all actions,
    }

    [System.Serializable]
    public class AttackAction : Action
    {
        
        public int damageMin;
        public int damageMax;

        public ActionResult.Effect attackEffect;

        public AttackAction(int _damageMin, int _damageMax, ActionResult.Effect _attackEffect, float _probability)
        {
            //Damage is random between min and max
            this.damageMin = _damageMin;
            this.damageMax = _damageMax; //
            this.attackEffect = _attackEffect;
            this.probability = _probability;
        }

        public override ActionResult Do()
        {
            if (Random.Range(0, 1) < probability)
            {
                return new ActionResult(attackEffect, 0, (int)(damageMin + ((damageMax + 1) - damageMin) * Random.Range(0f, 1f)));
            } else
            {
                return new ActionResult(ActionResult.Effect.miss, 0, 0);
            }
        }
    }

    [System.Serializable]
    public class ItemAction : Action
    {

        public GameObject item;

        public ActionResult itemresult;

        //TODO: setup item system
        //TODO: RemoveItem
        public ItemAction(GameObject _item)
        {
            this.item = _item;
            //TODO: Assigen Item Action Result from item;
        }


    }

    [System.Serializable]
    public class ActionResult {

        //Todo: Add action effects and do something with them lol
        public enum Effect { nothing, miss, poison, weakened, frozen, burning};
        public Effect actionEffect;
        public int healing;
        public int damage;

        //public statchanges? 

        public ActionResult(Effect effect, int _healing, int _damage)
        {
            actionEffect = effect;
            this.healing = _healing;
            this.damage = _damage;
        }

    }

 

}
