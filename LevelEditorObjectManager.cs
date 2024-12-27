using System.Collections.Generic;

namespace TeamMilkGame
{
    public class LevelEditorObjectManager
    {
        public static LevelEditorObjectManager Instance { get; } = new LevelEditorObjectManager();

        private LevelEditorObjectManager()
        {
            objectList = new List<IGameObject>();
            willClear = false;
            AddQueue = new Queue<IGameObject>();
            RemoveQueue = new Queue<IGameObject>();
        }
        public bool willClear;
        public bool resetPlayersOnClear;
        private Queue<IGameObject> AddQueue;
        private Queue<IGameObject> RemoveQueue;
        public List<IGameObject> objectList;

        public List<IGameObject> ObjectList
        {
            get
            {
                return objectList;
            }
        }

        public void ReqAdd(IGameObject obj)
        {
            AddQueue.Enqueue(obj);
        }

        public void ReqRemove(IGameObject obj)
        {
            RemoveQueue.Enqueue(obj);
        }

        public void Update()
        {
            Remove();
            Add();
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
                    if (this.objectList.Contains(Obj))
                    {
                        this.objectList.Remove(Obj);
                    }
                }
            }

        }

        private void Add()
        {
            while (AddQueue.Count > 0)
            {
                IGameObject obj = AddQueue.Dequeue();
                objectList.Add(obj);
            }
        }
        public void ClearLists()
        {
            objectList = new List<IGameObject>();
            AddQueue = new Queue<IGameObject>();
            RemoveQueue = new Queue<IGameObject>();
            willClear = false;
        }
    }
}
