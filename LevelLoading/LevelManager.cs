using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Emit;
using TeamMilkGame.Collision;
using TeamMilkGame.Controllers;

namespace TeamMilkGame
{
    //used to access level loading at will
    public class LevelManager
    {
        private static LevelManager instance = new LevelManager();

        public static LevelManager Instance
        {
            get
            {
                return instance;
            }
        }

        private string[] levelXMLPaths;
        private Stack<string> previousLevels;
        private LevelLoader loader;
        private string directory;
        private List<Controller> controlls;
        private MilkGame game;
        private Camera camera;
        public HUD playHUD { get; set; }

        //previous game object lists to save state in levels at runtime
        private Dictionary<string, Dictionary<string, List<IGameObject>>> LevelGameObjectLists;

        private Dictionary<string, bool> disableCamera;
        private Dictionary<string, Color> backgroundColors;

        private Vector2 loadMarioLocation;

        private bool willLoadLevel;
        private bool initControllers;
        private bool resetPlayers;
        private bool initHud;

        public bool isLevelLoadedCurrently { get; private set; }

        public Color backgroundColor { get; private set; }

        private ContentManager content;
        private int playerCount;
        private bool createPlayers;


        private LevelManager()
        {
            this.loader = new LevelLoader();
            willLoadLevel = false;
            directory = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "./LevelXMLFiles/");
            levelXMLPaths = Directory.GetFiles(directory);
            isLevelLoadedCurrently = false;
            backgroundColor = Color.CornflowerBlue;
            this.initControllers = false;
            this.resetPlayers = false;
            this.initHud = false;
        }

        public void SetDirectory(string directory)
        {
            this.directory = directory;
        }

        public string GetDirectory()
        {
            return this.directory;
        }

        public string[] GetLevelXMLPaths()
        {
            return this.levelXMLPaths;
        }

        public void Initialize(MilkGame game, Camera camera, List<Controller> controllers, ContentManager content)
        {
            this.game = game;
            this.camera = camera;
            this.controlls = controllers;
            this.content = content;
        }


        //loads in and resets all data that was saved
        public void LoadWorld(string xmlFile, int playerNum = 1)
        {
            this.playerCount = playerNum;
            this.backgroundColors = new Dictionary<string, Color>();
            this.disableCamera = new Dictionary<string, bool>();
            this.previousLevels = new Stack<string>();
            this.LevelGameObjectLists = new Dictionary<string, Dictionary<string, List<IGameObject>>>();
            isLevelLoadedCurrently = false;
            this.createPlayers = true;
            this.initControllers = true;
            this.resetPlayers = true;
            this.initHud = true;

            //starts mario at this location in grid
            Vector2 gridStart = new Vector2(189, 11);
            Vector2 StartLoc = new Vector2(gridStart.X * 64, 1000 - (64 * gridStart.Y + 1));


            Load(xmlFile, StartLoc);
            //this.previousLevels.Push(xmlFile);
            //loader.Load(directory + this.previousLevels.Peek());
        }

        //loads in level specified and saves previous game object list
        public void Load(string xmlFile, Vector2 playerOutputLoc)
        {
            //queues clearing of lists
            GameObjectManager.Instance.Clear(this.resetPlayers);
            this.resetPlayers = false;

            //saves state data
            loadMarioLocation = playerOutputLoc;
            willLoadLevel = true;
            if (isLevelLoadedCurrently)
            {
                this.LevelGameObjectLists[this.GetCurrentLevel()] = new Dictionary<string, List<IGameObject>>(GameObjectManager.Instance.allLists);
                //this.LevelGameObjectLists[this.GetCurrentLevel()] = GameObjectManager.Instance.AllLists.ToDictionary(entry => entry.Key, entry => entry.Value.Clone());
            }

            //pushes
            this.previousLevels.Push(xmlFile);
        }

        //goes back to the previous level
        public void GoBack()
        {
            this.previousLevels.Pop();
            willLoadLevel = true;
        }

        public string GetCurrentLevel()
        {
            return this.previousLevels.Peek();
        }

        public void Update(GameTime gametime)
        {
            //Debug.WriteLine(GameObjectManager.Instance.willClear);
            if (willLoadLevel && !GameObjectManager.Instance.willClear)
            {
                string level = this.GetCurrentLevel();

                if (LevelGameObjectLists.ContainsKey(level))
                {
                    //loads in saved state if loaded in level before
                    GameObjectManager.Instance.allLists = this.LevelGameObjectLists[level];

                    //gets rid of old mario objects
                    //GameObjectManager.Instance.ClearMarioObjects();
                }
                else
                {
                    //load new level if not loaded previous
                    loader.Load(directory + level, content, createPlayers, playerCount);
                    if (createPlayers)
                    {
                        createPlayers = false;
                    }

                    //adds controllers for player
                    if (this.initControllers)
                    {
                        this.controlls.Clear();
                        int playerNum = 0;
                        foreach (IMario Mario in GameObjectManager.Instance.Players)
                        {
                            controlls.Add(new Controller(Mario, game, playerNum));
                            playerNum++;
                        }
                        this.initControllers= false;
                    }

                    if(this.initHud)
                    {
                        playHUD = new HUD(loader.worldNumber, loader.timer);
                        this.initHud = false;
                    }


                    //sets camera variables

                    disableCamera[level] = loader.disableCamera;

                    //sets background color variables
                    backgroundColors[level] = loader.backgroundColor;

                    //adds saved players to level
                    //PlayerManager.Instance.AddPlayersToGameObjectManager();

                }

                //sets player position
                int positionOffset = 1 * 64;
                int i = 0;
                foreach (IMario player in GameObjectManager.Instance.Players)
                {
                    player.Position = new Vector2(loadMarioLocation.X + (i * positionOffset), loadMarioLocation.Y);
                    i++;
                }


                willLoadLevel = false;
                isLevelLoadedCurrently = true;

                CollisionDetection.Instance.LoadContent(20000); // Temporary number right now
                //sets background and camera variables
                camera.ResetCamera(700f);
                camera.disableCamera = disableCamera[level];
                backgroundColor = backgroundColors[level];

            }
        }
    }
}
