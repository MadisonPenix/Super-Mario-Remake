using System.Collections.Generic;

namespace TeamMilkGame
{
    public class GameObjectManager
    {
        // New Implementation -------------------------------------------------------------

        public static GameObjectManager Instance { get; } = new GameObjectManager();

        private GameObjectManager()
        {
            allLists = new Dictionary<string, List<IGameObject>>
            {
                ["MovingCollidables"] = new List<IGameObject>(),
                ["NonMovingCollidables"] = new List<IGameObject>(),
                ["NonCollidables"] = new List<IGameObject>(),
                ["Updatable"] = new List<IGameObject>(),
                ["Drawable"] = new List<IGameObject>(),
                ["Players"] = new List<IGameObject>(),
            };
            willClear = false;
            AddQueue = new Queue<KeyValuePair<string, IGameObject>>();
            RemoveQueue = new Queue<IGameObject>();
            StarQueue = new Queue<KeyValuePair<KeyValuePair<IGameObject, int>, bool>>();
        }
        public bool willClear;
        public bool resetPlayersOnClear;
        private Queue<KeyValuePair<string, IGameObject>> AddQueue;
        private Queue<IGameObject> RemoveQueue;
        private Queue<KeyValuePair<KeyValuePair<IGameObject, int>, bool>> StarQueue;
        public Dictionary<string, List<IGameObject>> allLists;
        public List<IGameObject> MovingCollidables
        {
            get
            {
                return allLists["MovingCollidables"];
            }
        }
        public List<IGameObject> NonMovingCollidables
        {
            get
            {
                return allLists["NonMovingCollidables"];
            }
        }
        public List<IGameObject> NonCollidables
        {
            get
            {
                return allLists["NonCollidables"];
            }
        }
        public List<IGameObject> Updatable
        {
            get
            {
                return allLists["Updatable"];
            }
        }
        public List<IGameObject> Drawable
        {
            get
            {
                return allLists["Drawable"];
            }
        }
        public List<IGameObject> Players
        {
            get
            {
                return allLists["Players"];
            }
        }

        public void Clear(bool resetPlayers = false)
        {
            willClear = true;
            resetPlayersOnClear = resetPlayers;
        }

        public void ReqAdd(string str, IGameObject obj)
        {
            AddQueue.Enqueue(new KeyValuePair<string, IGameObject>(str, obj));
        }

        public void ReqRemove(IGameObject obj)
        {
            RemoveQueue.Enqueue(obj);
        }

        public void ReqStarChange(IGameObject Obj, bool ToStar, int Timer)
        {
            KeyValuePair<IGameObject, int> StarKey = new KeyValuePair<IGameObject, int>(Obj, Timer);
            StarQueue.Enqueue(new KeyValuePair<KeyValuePair<IGameObject, int>, bool>(StarKey, ToStar));
        }


        public void Update()
        {
            Remove();
            Add();
            StarChange();
        }

        private void Remove()
        {
            if (willClear)
            {
                ClearLists();
            }
            else
            {
                if (RemoveQueue.Count > 0)
                {
                    IGameObject Obj = RemoveQueue.Dequeue();
                    foreach (var ObjListPair in allLists)
                    {
                        if (ObjListPair.Value.Contains(Obj))
                        {
                            ObjListPair.Value.Remove(Obj);
                        }
                    }
                }
            }

        }

        private void StarChange()
        {
            while (StarQueue.Count > 0)
            {
                KeyValuePair<KeyValuePair<IGameObject, int>, bool> Pair = StarQueue.Dequeue();
                bool ToStar = Pair.Value;
                IMario Mario = (IMario)Pair.Key.Key;
                int Timer = Pair.Key.Value;
                IMario MarioToStar = new StarMario(Mario, Timer);

                foreach (var ObjList in allLists)
                {
                    int Index = ObjList.Value.IndexOf(Mario);
                    if (ToStar && Index >= 0)
                    {
                        ObjList.Value[Index] = MarioToStar;
                    }
                    else if (Index >= 0)
                    {
                        IMario decMario = (Mario as StarMario).decMario;
                        ObjList.Value[Index] = decMario;
                    }
                }
            }
        }

        private void Add()
        {
            while (AddQueue.Count > 0)
            {
                KeyValuePair<string, IGameObject> Pair = AddQueue.Dequeue();
                if (Pair.Value is IMario)
                {
                    allLists["Players"].Add(Pair.Value);
                }
                allLists[Pair.Key].Add(Pair.Value);
                Updatable.Add(Pair.Value);
                Drawable.Add(Pair.Value);
            }
        }
        //removes everything except for mario objects
        public void ClearLists()
        {
            if (resetPlayersOnClear)
            {
                allLists["MovingCollidables"] = new List<IGameObject>();
                allLists["NonMovingCollidables"] = new List<IGameObject>();
                allLists["NonCollidables"] = new List<IGameObject>();
                allLists["Updatable"] = new List<IGameObject>();
                allLists["Drawable"] = new List<IGameObject>();
                allLists["Players"] = new List<IGameObject>();
                AddQueue = new Queue<KeyValuePair<string, IGameObject>>();
                RemoveQueue = new Queue<IGameObject>();
            }
            else
            {

                Queue<KeyValuePair<string, IGameObject>> savedPlayers = SaveMarioObjects();
                allLists["MovingCollidables"] = new List<IGameObject>();
                allLists["NonMovingCollidables"] = new List<IGameObject>();
                allLists["NonCollidables"] = new List<IGameObject>();
                allLists["Updatable"] = new List<IGameObject>();
                allLists["Drawable"] = new List<IGameObject>();
                allLists["Players"] = new List<IGameObject>();
                AddQueue = savedPlayers;
                RemoveQueue = new Queue<IGameObject>();
            }
            willClear = false;
        }

        public void ClearPlayers()
        {
            allLists["Players"] = new List<IGameObject>();
        }

        //creates a new addqueue with all of the player's previous attributes
        public Queue<KeyValuePair<string, IGameObject>> SaveMarioObjects()
        {
            Queue<KeyValuePair<string, IGameObject>> players = new Queue<KeyValuePair<string, IGameObject>>();
            foreach (KeyValuePair<string, IGameObject> AddedObject in AddQueue)
            {
                if (AddedObject.Value is IMario)
                {
                    players.Enqueue(AddedObject);
                }
            }
            foreach (IMario player in Players)
            {
                string strType = "";
                if (MovingCollidables.Contains(player))
                {
                    strType = "MovingCollidables";
                }
                else
                {
                    strType = "NonCollidables";
                }
                players.Enqueue(new KeyValuePair<string, IGameObject>(strType, player));
            }
            return players;
        }
    }
}
