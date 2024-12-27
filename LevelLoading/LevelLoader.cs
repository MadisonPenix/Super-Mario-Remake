using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace TeamMilkGame
{
    class LevelLoader
    {
        private XMLHandler xmlHandler;
        public bool disableCamera;
        public Color backgroundColor;
        public Background background;
        public string worldNumber;
        public double timer;
        public Dictionary<string, List<IGameObject>> objects { get; set; }

        public LevelLoader()
        {
            objects = new Dictionary<string, List<IGameObject>>();
            disableCamera = false;
            backgroundColor = Color.White;
        }
        public void Load(string xmlFile, ContentManager content, bool clearPlayers = false, int playerCount = 1)
        {
            xmlHandler = new XMLHandler(objects);
            xmlHandler.readXML(xmlFile);
            background = xmlHandler.levelBackground;
            if (background != null)
            {
                //Temporarily only this background, change when we add other background options
                try
                {
                    background.backgroundTexture = content.Load<Texture2D>(xmlHandler.levelFile);
                    GameObjectManager.Instance.Drawable.Add((IGameObject)background);
                }
                catch { }
            }

            timer = xmlHandler.timer;
            worldNumber = xmlHandler.worldNumber;



            foreach (IGameObject collidable in objects["Collidable"])
            {
                if (collidable is IBlocks && collidable is not BrickBlock)
                {
                    GameObjectManager.Instance.ReqAdd("NonMovingCollidables", collidable);
                }
                else
                {
                    GameObjectManager.Instance.ReqAdd("MovingCollidables", collidable);
                }
            }

            foreach (IGameObject nonCollidable in objects["Not_Collidable"])
            {
                GameObjectManager.Instance.ReqAdd("NonCollidables", nonCollidable);
            }

            if (clearPlayers)
            {
                GameObjectManager.Instance.ClearPlayers();

                //starts mario at this location in grid
                Vector2 gridStart = new Vector2(2, 2);
                Vector2 StartLoc = new Vector2(gridStart.X * 64, 1000 - (64 * gridStart.Y + 1));
                for (int i = 0; i < playerCount; i++)
                {
                    //alternates between placing mario and luigi
                    if (i % 2 == 0)
                    {
                        GameObjectManager.Instance.ReqAdd("MovingCollidables", new Mario((int)StartLoc.X, (int)StartLoc.Y));
                    }
                    else
                    {
                        GameObjectManager.Instance.ReqAdd("MovingCollidables", new Luigi((int)StartLoc.X, (int)StartLoc.Y));
                    }
                }
            }

            GameObjectManager.Instance.Update();
            disableCamera = xmlHandler.cameraDisabled;
            disableCamera = xmlHandler.cameraDisabled;
            backgroundColor = xmlHandler.backgroundColor;
            objects = new Dictionary<string, List<IGameObject>>();
        }

    }
}
