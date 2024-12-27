using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TeamMilkGame.Controllers;
using Microsoft.Xna.Framework.Graphics;
using TeamMilkGame.UtilityClasses;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TeamMilkGame
{
    public class MouseController
    {
        private MouseState mouseState;
        private Vector2 mouseLocation;
        private Vector2 itemPosition;
        private bool hasInput;
        private Texture2D cursorItem;
        private IGameObject clonedItem;
        private IGameObject objectSelected;

        private List<Vector2> spaceOccupied;

        private Queue<Vector2> RemoveSpace;
        private Queue<Vector2> AddSpace;

        private bool disableInput;

        public MouseController(MilkGame game)
        {
            objectSelected = null;
            disableInput = false;
            spaceOccupied = new List<Vector2>();
            AddSpace = new Queue<Vector2>();
            RemoveSpace = new Queue<Vector2>();
        }
        public void Update(GameTime gametime, IGameObject currentObject, Rectangle ButtonBanner)
        {
            hasInput = false;
            mouseState = Mouse.GetState();
            mouseLocation = new Vector2(mouseState.X, mouseState.Y);
            objectSelected = currentObject;

            if(ButtonBanner.Contains((int)mouseLocation.X, (int)mouseLocation.Y))
            {
                disableInput= true;
            }
            else
            {
                disableInput = false;
            }

            if (!disableInput)
            {
                //Change the position to be snapped to the grid
                int xOffset = (int)mouseLocation.X % GameUtility.Instance.GRID_SIZE;
                int yOffset = (int)mouseLocation.Y % GameUtility.Instance.GRID_SIZE;
                itemPosition = mouseLocation - new Vector2(xOffset, yOffset);

                //check if the left mouse button has been clicked
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    if (currentObject != null)
                    {
                        //DeleteItemIfIntersect(new Rectangle((int)itemPosition.X, (int)itemPosition.Y, currentObject.sprite.spriteWidth, currentObject.sprite.spriteHeight));
                        DeleteItemAtPos(itemPosition);
                        //DeleteItemAtPos(itemPosition);
                        //clones object and places it in scene
                        clonedItem = currentObject.Clone(itemPosition);
                        currentObject.Position = itemPosition;
                        LevelEditorObjectManager.Instance.ReqAdd(clonedItem);

                    }

                }
                //delete the item that's currently being hovered over
                else if (mouseState.RightButton == ButtonState.Pressed)
                {
                    DeleteItemAtPos(itemPosition);
                }
            }

            /*AddSpaces();
            RemoveSpaces();*/
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (!disableInput)
            {
                //consider creating check for if there's already an item there
                if (objectSelected != null)
                {
                    //make the item transluscent
                    Color itemColor = GameUtility.Instance.BUTTON_COLOR;
                    //draw the item
                    objectSelected.sprite.DrawWithColor(spriteBatch, itemPosition, itemColor);
                }
            }
        }
        public bool HasInput()
        {
            return hasInput;
        }

        private void DeleteItemAtPos(Vector2 pos)
        {
            //checks if there is a block at the current position and replaces it if there is
            foreach (IGameObject obj in LevelEditorObjectManager.Instance.ObjectList)
            {
                if (obj.Position == pos)
                {
                    //gets gridSizes for the sprite in order to check with objects
                    /*int gridSpacesX = (int)Math.Ceiling((decimal)obj.sprite.spriteWidth / GameUtility.Instance.GRID_SIZE);
                    int gridSpacesY = (int)Math.Ceiling((decimal)obj.sprite.spriteHeight / GameUtility.Instance.GRID_SIZE);


                    int posGridX = (int)pos.X / GameUtility.Instance.GRID_SIZE;
                    int posGridY = (int)pos.Y / GameUtility.Instance.GRID_SIZE;

                    int posEndX = (int)(posGridX + gridSpacesX);
                    int posEndY = (int)(posGridY + gridSpacesY);*/
                    LevelEditorObjectManager.Instance.ReqRemove(obj);

                    //remove spaces taken by sprite
                    /*for (int i = (int)posGridX; i <= posEndX; i++)
                    {
                        for (int j = (int)posGridY; j <= posEndY; j++)
                        {
                            ReqRemove(new Vector2(i, j));
                        }
                    }*/
                }
            }
        }


        //tried to implement something like the game 
        private void DeleteItemIfIntersect(Rectangle objectToCheck)
        {
            //checks if there is a block at the current position and replaces it if there is
            foreach (IGameObject obj in LevelEditorObjectManager.Instance.ObjectList)
            {
                Rectangle objRec = new Rectangle((int)obj.Position.X, (int)obj.Position.Y, obj.sprite.spriteWidth, obj.sprite.spriteHeight);
                if (objRec.Intersects(objectToCheck))
                {
                    LevelEditorObjectManager.Instance.ReqRemove(obj);
                }
            }
        }
        //tried to implement something like the game object manager to have objects be deleted even if they were at other positions
        //but intersecting with the object placed (tried the above solution and it was very VERY slow) but kinda gave up cuz we got
        //other stuff to do, so for now the object at the placed position will be deleted
        private void DeleteItemIfIntersectV2(Vector2 pos, ISprite sprite)
        {
            //gets gridSizes for the sprite in order to check with objects
            int gridSpacesX = (int)Math.Ceiling((decimal)sprite.spriteWidth / GameUtility.Instance.GRID_SIZE);
            int gridSpacesY = (int)Math.Ceiling((decimal)sprite.spriteHeight / GameUtility.Instance.GRID_SIZE);

            int posGridX = (int)pos.X / GameUtility.Instance.GRID_SIZE;
            int posGridY = (int)pos.Y / GameUtility.Instance.GRID_SIZE;

            int posEndX = (int)(posGridX + gridSpacesX);
            int posEndY = (int)(posGridY + gridSpacesY);


            //checks if there is an object at the current position and deletes it if there is
            foreach (Vector2 spaceTaken in spaceOccupied)
            {
                if (spaceTaken.Y >= posGridY && spaceTaken.Y < posEndY)
                {
                    if (spaceTaken.X >= posGridX && spaceTaken.X < posEndX)
                    {
                        DeleteItemAtPos(spaceTaken);
                    }
                }
            }

            //update spaces taken for new sprite
            for(int i = (int)posGridX; i <= posEndX; i++)
            {
                for(int j = (int)posGridY; j <= posEndY; j++)
                {
                    ReqAdd(new Vector2(i, j));
                }
            }
        }

        private void AddSpaces()
        {
            while(AddSpace.Count > 0)
            {
                spaceOccupied.Add(AddSpace.Dequeue());
            }
        }

        private void RemoveSpaces()
        {
            while (RemoveSpace.Count > 0)
            {
                /*IGameObject Obj = RemoveSpace.Dequeue();
                foreach (var ObjListPair in spaceOccupied)
                {
                    if (ObjListPair.Value.Contains(Obj))
                    {
                        ObjListPair.Value.Remove(Obj);
                    }
                }*/

                spaceOccupied.Remove(RemoveSpace.Dequeue());
            }
        }

        private void ReqAdd(Vector2 val)
        {
            AddSpace.Enqueue(val);
        }

        private void ReqRemove(Vector2 val)
        {
            RemoveSpace.Enqueue(val);
        }
    }
}
