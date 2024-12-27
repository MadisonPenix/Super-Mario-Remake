using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TeamMilkGame.Sprites;
using TeamMilkGame.States;
using TeamMilkGame.UtilityClasses;

namespace TeamMilkGame.GameStates
{
    public class GameTransitions : State
    {
        private int countFrames;
        private bool isFinished;
        private Vector2 locationPoint;
        private Vector2 sizePoint;
        private Rectangle BGSource;
        private Rectangle titleSource;
        private Texture2D MenuIntroFrames;
        private ISprite menuIntro;
        // possible other transitions later

        public GameTransitions(MilkGame game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content)
        {
            MenuIntroFrames = content.Load<Texture2D>("mario_animated");
            menuIntro = new AnimatedSprite(MenuIntroFrames, 6, 6, Vector2.Zero, GameUtility.Instance.MENU_TRANSITION_FRAMES_END, GameUtility.Instance.MENU_TRANSITION_FRAMETIME, 0, 0, false, GameUtility.Instance.TITLE_MULT);
            countFrames = 0;
            isFinished = false;
            BGSource = GameUtility.Instance.MENU_TRANSITION_BG_SOURCE;
            titleSource = GameUtility.Instance.MENU_TRANSITION_TITLE_SOURCE;
            Vector2 ViewportCenter = new Vector2(graphicsDevice.Viewport.Width / 2f, graphicsDevice.Viewport.Height / 2f);
            locationPoint = new Vector2(ViewportCenter.X - titleSource.Width * GameUtility.Instance.TITLE_MULT / 2, GameUtility.Instance.TITLE_Y_POS);
            sizePoint = new Vector2(titleSource.Width * GameUtility.Instance.TITLE_MULT, titleSource.Height * GameUtility.Instance.TITLE_MULT);
        }

        public override void Draw(GameTime gametime, SpriteBatch spriteBatch) { }


        public override void Update(GameTime gameTime)
        {
            menuIntro.Update(gameTime);
        }

        /*
         * countFrames is used to keep track of how far the animation has played, incremented with each call.
         * totalFrames is the frame count of the animation + a delay amount after it's finished.
         * bool return value used by calls to determine whether or not the animation is finished yet,
         *      if it isn't finished it won't proceed with the rest of its Draw() code until it is.
         * when countFrames >= totalFrames we don't need to call this anymore for now (return true).
         * if countFrames is in between the framecount for the animation and the delay amount,
         *      the animation is finished but we still need to delay until countFrames reaches totalFrames,
         *      so it draws its final frame repeatedly until then (return false).
         * if the animation isn't over yet, draw the next frame in the animation like any other AnimatedSprite.
         */
        public bool DrawMenuIntro(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            // declaring this here because having to write out the expression in the cases is ugly
            const int TOTAL_FRAMES = GameUtility.MENU_TRANSITION_FRAMECOUNT + GameUtility.MENU_TRANSITION_DELAY;
            switch (countFrames)
            {
                case >= TOTAL_FRAMES:
                    isFinished = true;
                    break;
                case >= GameUtility.MENU_TRANSITION_FRAMECOUNT and < TOTAL_FRAMES:
                    //draw the background w/o buttons
                    spriteBatch.Draw(MenuIntroFrames, new Rectangle(0, 0, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height), BGSource, Color.White);
                    spriteBatch.Draw(MenuIntroFrames, new Rectangle(locationPoint.ToPoint(), sizePoint.ToPoint()), titleSource, Color.White);
                    countFrames++;
                    break;
                default:
                    menuIntro.Draw(spriteBatch, Vector2.Zero);
                    countFrames++;
                    break;
            }
            spriteBatch.End();
            return isFinished;
        }
    }
}