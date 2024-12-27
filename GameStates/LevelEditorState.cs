using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TeamMilkGame.States;
using TeamMilkGame.UtilityClasses;

namespace TeamMilkGame.GameStates
{
    public class LevelEditorState : State
    {
        public Camera Camera { get; set; }

        private DragDrop moveItem;
        private MouseController mouseController;

        //Variables related to Menu Button
        private SpriteFont buttonFont;
        private State pauseState;
        Button pauseButton;
        Button allListsButton;
        Button enemyListButton;
        Button blockListButton;
        private Texture2D pauseTexture;
        private bool paused;

        //Variables related to Inventory/Items
        //Thinking about loading banner, items, floor, and grid overlay with XML file
        private Texture2D banner;
        private Texture2D brickBlockTexture;
        private List<IComponent> blocksMenu;
        private Texture2D currentTexture;
        private IGameObject currentObject;

        private List<IGameObject[]> ALL_LIST;
        private IGameObject[] BLOCK_LIST;
        private IGameObject[] ENEMY_LIST;
        private IGameObject[] ITEM_LIST;

        private Vector2 bannerPos;
        private Vector2 bannerSpecs;

        private Rectangle buttonBannerRec;

        public LevelEditorState(MilkGame Game, GraphicsDevice GraphicsDevice, ContentManager Content) : base(Game, GraphicsDevice, Content)
        {
            moveItem = null;
            mouseController = new MouseController(Game);

            //Initialize Menu Button needs
            buttonFont = content.Load<SpriteFont>("Fonts/Font");
            pauseTexture = content.Load<Texture2D>("pauseButton");
            pauseState = new LevelEditorPauseState(game, graphicsDevice, Content);
            paused = false;

            bannerPos = new Vector2(0, 570);
            bannerSpecs = new Vector2(1500, 150);

            buttonBannerRec = new Rectangle((int)bannerPos.X, (int)bannerPos.Y, (int)bannerSpecs.X, (int)bannerSpecs.Y);

            pauseButton = new Button(buttonFont)
            {
                buttonPosition = GameUtility.Instance.PAUSE_POS,
                buttonText = "  ",
            };

            allListsButton = new Button(buttonFont)
            {
                //sets the text to be centered in the screen (horizontally)
                buttonPosition = bannerPos + new Vector2(10, 10),
                buttonText = "All",
            };

            pauseButton.Click += PauseButton_Click;

            //Initialize Inventory Needs
            banner = content.Load<Texture2D>("banner");

            //inits all lists to use (tried to use GameUtility but class but it wont work for some reason)
            IGameObject[] BLOCK_LIST = {new BrickBlock(1, 2), new PipeBlock(1, 2), new CrackedBrickBlock(1, 2), new PipeSegment(1, 2),
                new QuestionBlock(1, 2), new SolidBlock(1, 2)};
            this.BLOCK_LIST = BLOCK_LIST;

            IGameObject[] ENEMY_LIST = { new Koopa(1, 2), new Goomba(1, 2), new Bowser(1, 2) };
            this.ENEMY_LIST = ENEMY_LIST;

            IGameObject[] ITEM_LIST = { new Coin(1, 2), new ExtraLifeMushroom(1, 2), new FireFlower(1, 2), new InvincibilityStar(1,2),
                new PowerMushroom(1,2), new Flagpole(1,2)};
            this.ITEM_LIST = ITEM_LIST;

            this.ALL_LIST = new List<IGameObject[]>
            {
                BLOCK_LIST,
                ENEMY_LIST,
                ITEM_LIST
            };

            blocksMenu = CreateInteractableButtons(this.ALL_LIST);

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //make button transluscent
            Color buttonColor = GameUtility.Instance.BUTTON_COLOR;

            spriteBatch.Begin(); //Drawing for objects not affected by camera

            //Draw game objects in level
            foreach (IGameObject obj in LevelEditorObjectManager.Instance.ObjectList)
            {
                obj.Draw(spriteBatch);
            }

            if (!paused) //if the game is not paused, draw the pause button
            {
                spriteBatch.Draw(pauseTexture, GameUtility.Instance.ALT_SPRITEBATCH_RECT, buttonColor);
            }

            //draw UI after everything else
            spriteBatch.Draw(banner, buttonBannerRec, Color.White); //Magic numbers will be moved to Utility Class once finalized
            mouseController.Draw(spriteBatch);
            allListsButton.Draw(gameTime, spriteBatch);

            //Draw buttons in menus
            foreach (IComponent component in blocksMenu)
            {
                component.Draw(gameTime, spriteBatch);
            }

            spriteBatch.End();

            //if the game is paused, draw the paused overlay
            if (paused)
            {
                pauseState.Draw(gameTime, spriteBatch);
            }


        }

        public override void Update(GameTime gameTime)
        {
            //If Menu is open, Update Pause State
            if (paused)
            {
                pauseState.Update(gameTime);

            }
            //If not, Don't, Update Pause Button though
            else
            {
                pauseButton.Update(gameTime);
                /*
                 * TO-DO:
                 * store the current selected item and have put that into update
                 */
                mouseController.Update(gameTime, currentObject, buttonBannerRec);

                foreach (IComponent component in blocksMenu)
                {
                    component.Update(gameTime);
                }

                //dont update objects as they will start moving
                /*foreach (IGameObject obj in LevelEditorObjectManager.Instance.ObjectList)
                {
                    obj.Update(gameTime);
                }*/
                //update sprites instead for animation
                foreach (IGameObject obj in LevelEditorObjectManager.Instance.ObjectList)
                {
                    obj.sprite.Update(gameTime);
                }
            }

            //Update object manager
            LevelEditorObjectManager.Instance.Update();

            //update paused
            this.paused = game.paused;
        }


        private void PauseButton_Click(object sender, ButtonArgs e)
        {
            //make the game paused
            game.paused = true;
        }

        private void ChooseBlockButton_Click(object sender, ButtonArgs e)
        {
            DragDrop brickDrag = new DragDrop(e.gameObject);
            currentObject = e.gameObject;
            //LevelEditorObjectManager.Instance.ReqAdd(brickDrag);
        }

        private List<IComponent> CreateInteractableButtons(List<IGameObject[]> allObjs)
        {
            List<IComponent> buttonList = new List<IComponent>();

            Texture2D buttonBackground = new Texture2D(graphicsDevice, 1, 1);
            buttonBackground.SetData(new[] { Color.White });

            Vector2 lastButtonEnd = new Vector2(0, 630);

            foreach (IGameObject[] ButtonListObjects in allObjs)
            {
                for (int i = 0; i < ButtonListObjects.Length; i++)
                {
                    float newLoc = 20 + lastButtonEnd.X;
                    //checks if sprite is bigger than display left and goes to next row if it is
                    if (newLoc + ButtonListObjects[i].sprite.spriteWidth > GameUtility.Instance.GRAPHICS_BUFFER_WIDTH)
                    {
                        lastButtonEnd.Y = lastButtonEnd.Y + 30 + 64;
                        lastButtonEnd.X = 0;
                    }
                    SpriteButton BlockButton = new SpriteButton(ButtonListObjects[i].sprite, buttonBackground)
                    {
                        buttonPosition = new Vector2(20 + lastButtonEnd.X, lastButtonEnd.Y),
                        createdObject = ButtonListObjects[i]
                    };
                    lastButtonEnd.X = BlockButton.rectangle.Width + BlockButton.buttonPosition.X;
                    BlockButton.Click += ChooseBlockButton_Click;

                    buttonList.Add(BlockButton);
                }
            }

            return buttonList;
        }

        /*private List<IComponent> CreateInteractableButtonsAll(List<IGameObject[]> allObjs)
        {
            List<IComponent> buttonList = new List<IComponent>();
            foreach (IGameObject[] group in allObjs) 
            {
                List<IComponent> tempList = CreateInteractableButtons(group);
                foreach(IComponent com in tempList)
                {
                    buttonList.Add(com);
                }
            }
            return buttonList;
        }*/

    }
}
