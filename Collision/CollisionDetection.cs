using Microsoft.Xna.Framework;
using System.Collections.Generic;
using TeamMilkGame.UtilityClasses;

namespace TeamMilkGame.Collision
{

    public class CollisionDetection : ICollideSide
    {
        private static CollisionDetection instance = new CollisionDetection();

        public static CollisionDetection Instance
        {
            get
            {
                return instance;
            }
        }

        private CollisionDetection()
        {

        }

        private Dictionary<int, List<ICollidable>> BlockCols;
        private Dictionary<int, List<ICollidable>> CollidersCols;
        private int NumCols;
        private int COLUMN_WIDTH = GameUtility.Instance.COL_WIDTH;

        public void LoadContent(int LevelLength)
        {
            // Create the 2D array of blocks in their respective columns
            NumCols = LevelLength / COLUMN_WIDTH;
            CollidersCols = new Dictionary<int, List<ICollidable>>();
            FillCols();
        }

        private void FillCols()
        {
            BlockCols = new Dictionary<int, List<ICollidable>>();
            for (int i = 0; i < NumCols; i++)
            {
                List<ICollidable> CurCol = new List<ICollidable>();
                foreach (IGameObject Block in GameObjectManager.Instance.NonMovingCollidables)
                {
                    if (GetColumnIDs(Block) == i)
                    {
                        CurCol.Add((ICollidable)Block);
                    }
                }
                BlockCols.Add(i, CurCol);
            }
        }

        public void Detect()
        {
            FillColliders();
            foreach (ICollidable Collider in GameObjectManager.Instance.MovingCollidables)
            {
                if (Collider is not IBlocks)
                {
                    CheckCollisions(Collider);
                }
            }
        }

        private void FillColliders()
        {
            CollidersCols.Clear();
            foreach (ICollidable Collider in GameObjectManager.Instance.MovingCollidables)
            {
                int ColID = GetColumnIDs(Collider);
                if (!CollidersCols.TryGetValue(ColID, out List<ICollidable> Objs))
                {
                    Objs = new List<ICollidable>();
                }
                Objs.Add(Collider);
                CollidersCols[ColID] = Objs;
            }
        }

        //Takes a moveable gameObject as a parameter and creates a list of nearby objects from its own column, the one before, and the one after
        private List<ICollidable> GetNearbyObjects(ICollidable gameObject)
        {
            List<ICollidable> nearbyObjects = new List<ICollidable>();
            int columnID = GetColumnIDs(gameObject);
            if (columnID < 0 || columnID >= NumCols)
            {
                GameObjectManager.Instance.ReqRemove(gameObject);
                return new List<ICollidable>();
            }
            nearbyObjects.AddRange(BlockCols[columnID]);
            if (CollidersCols.TryGetValue(columnID, out List<ICollidable> Objs))
            {
                nearbyObjects.AddRange(Objs);
            }
            if (columnID - 1 > -1)
            {
                nearbyObjects.AddRange(BlockCols[columnID - 1]);
                if (CollidersCols.TryGetValue(columnID - 1, out List<ICollidable> ObjList))
                {
                    nearbyObjects.AddRange(ObjList);
                }
            }
            if (columnID + 1 < NumCols)
            {
                nearbyObjects.AddRange(BlockCols[columnID + 1]);
                if (CollidersCols.TryGetValue(columnID + 1, out List<ICollidable> ObjList))
                {
                    nearbyObjects.AddRange(ObjList);
                }
            }

            return nearbyObjects;
        }

        //Gets List of nearby objects and checks intersections, will call Handle Collision accordingly
        //May change IGameObject into ICollidable and update methods accordingly for easy collision response calling
        private void CheckCollisions(ICollidable Collider)
        {
            List<ICollidable> nearbyObjects = GetNearbyObjects(Collider);
            Rectangle ColliderBox = Collider.BoundingBox;
            foreach (ICollidable Collided in nearbyObjects)
            {
                if (Collider.Equals(Collided))
                {
                    continue;   // Skip case where it is the same object
                }
                Rectangle CollidedBox = Collided.BoundingBox;
                if (ColliderBox.Intersects(CollidedBox))
                {
                    Rectangle overlap = Rectangle.Intersect(ColliderBox, CollidedBox);
                    ICollideSide.Side collisionSide = DetermineCollisionSide(ColliderBox, CollidedBox);
                    //Debug.WriteLine(Collider.GetType().Name + ", " + Collided.GetType().Name);
                    CollisionResponse.Instance.HandleCollision(Collider, Collided, collisionSide, overlap);
                    ColliderBox = Collider.BoundingBox;
                }
            }
        }

        //HashID is object position divided by the column width to get index of correct column
        private int GetColumnIDs(IGameObject gameObject)
        {
            return (int)(gameObject.Position.X / COLUMN_WIDTH);
        }

        private ICollideSide.Side DetermineCollisionSide(Rectangle Collider, Rectangle Collided)
        {
            ICollideSide.Side Side;

            Rectangle Overlap = Rectangle.Intersect(Collider, Collided);

            if (Overlap.Height > Overlap.Width + GameUtility.Instance.COLLISION_WIDTH_OFFSET)  // Left or Right Collision
            {
                Side = (Collider.Left > Collided.Left) ? ICollideSide.Side.Right : ICollideSide.Side.Left;
            }
            else
            {
                Side = (Collider.Top > Collided.Top) ? ICollideSide.Side.Down : ICollideSide.Side.Top;
            }
            return Side;
        }
    }
}
