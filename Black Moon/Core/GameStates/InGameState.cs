using BlackMoon.Player;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using BlackMoon.World.TileScroller;
using BlackMoon.World;
using ContentImporters.MapHandler;
using Microsoft.Xna.Framework;
using BlackMoon.Network.net.packets;

namespace BlackMoon.Core.GameState
{
    public class InGameState : IState
    {
        private Game g;
        private PC player;
        private KeyboardHandler keyboard;
        private MouseHandler mouse;
        private SpriteBatch spriteBatch;
        private TileScroller scroller;
        private EntityManager entityManager;
        private MapData currentMap;
        private float currentDeltaTime;

        public InGameState(Game g)
        {
            this.g = g;
            spriteBatch = new SpriteBatch(g.GraphicsDevice);

            keyboard = new KeyboardHandler();
            mouse = new MouseHandler();
            player = new PC();
            player.texture = g.Content.Load<Texture2D>("TestSprite");
            player.camera = new Camera(g.GraphicsDevice);
            player.movementState.Change("idle");
            //create a builder for frame shit sometime
            
            //endof create a builder for frame shit sometime


            scroller = new TileScroller();

            currentMap = g.Content.Load<MapData>("Maps/TestMap");
            scroller.loadMap(g.Content, currentMap);


            keyboard.AddKeyEvent(Keys.W, KeyboardHandler.KeyEventType.OnKeyDown, () => {
                player.directionFlags.N = true;
                player.directionFlags.CLICK = false;
                player.movementState.Change("move");
            });

            keyboard.AddKeyEvent(Keys.W, KeyboardHandler.KeyEventType.OnKeyUp, () => {
                player.directionFlags.N = false;
            });

            keyboard.AddKeyEvent(Keys.A, KeyboardHandler.KeyEventType.OnKeyDown, () => {
                player.directionFlags.W = true;
                player.directionFlags.CLICK = false;
                player.movementState.Change("move");
            });

            keyboard.AddKeyEvent(Keys.A, KeyboardHandler.KeyEventType.OnKeyUp, () => {
                player.directionFlags.W = false;
            });

            keyboard.AddKeyEvent(Keys.S, KeyboardHandler.KeyEventType.OnKeyDown, () =>
            {
                player.directionFlags.S = true;
                player.directionFlags.CLICK = false;
                player.movementState.Change("move");
            });

            keyboard.AddKeyEvent(Keys.S, KeyboardHandler.KeyEventType.OnKeyUp, () =>
            {
                player.directionFlags.S = false;
            });

            keyboard.AddKeyEvent(Keys.D, KeyboardHandler.KeyEventType.OnKeyDown, () =>
            {
                player.directionFlags.E = true;
                player.directionFlags.CLICK = false;
                player.movementState.Change("move");
            });

            keyboard.AddKeyEvent(Keys.D, KeyboardHandler.KeyEventType.OnKeyUp, () =>
            {
                player.directionFlags.E = false;
            });

            keyboard.AddKeyEvent(Keys.OemTilde, KeyboardHandler.KeyEventType.OnKeyPress, () =>
            {
                MemoryManager.DebugMode = !MemoryManager.DebugMode;
            });

            keyboard.AddKeyEvent(Keys.LeftShift, KeyboardHandler.KeyEventType.OnKeyDown, () =>
            {
                player.isRunning = true;
            });

            keyboard.AddKeyEvent(Keys.LeftShift, KeyboardHandler.KeyEventType.OnKeyPress, () =>
            {
                player.isRunning = false;
            });

            keyboard.AddKeyComboDownEvent(new Input.KeyCombo { Keys.LeftShift, Keys.E }, () =>
            {
                Console.WriteLine("SHIFT E");
            });

            keyboard.AddKeyEvent(Keys.E, KeyboardHandler.KeyEventType.OnKeyDown, () =>
            {
                Console.WriteLine("E");
            });

            keyboard.AddKeyEvent(Keys.LeftShift, KeyboardHandler.KeyEventType.OnKeyDown, () =>
            {
                Console.WriteLine("SHIFT");
            });

            mouse.AddMouseEvent(MouseHandler.MouseEventType.OnMouseMove, () =>
            {
                //SetFaceDirection();
            });

            mouse.AddButtonEvent(MouseHandler.ButtonType.RightButton, MouseHandler.ButtonEventType.OnButtonDown, () =>
            {
                //player.directionFlags.CLICK = true;
            });

            mouse.AddButtonEvent(MouseHandler.ButtonType.RightButton, MouseHandler.ButtonEventType.OnButtonUp, () =>
            {
                //player.directionFlags.CLICK = false;
            });

            mouse.AddButtonEvent(MouseHandler.ButtonType.LeftButton, MouseHandler.ButtonEventType.OnButtonDownDelay, () =>
            {
                
            }, 200);

            mouse.AddButtonEvent(MouseHandler.ButtonType.LeftButton, MouseHandler.ButtonEventType.OnButtonClick, () =>
            {
                
            });

            mouse.AddButtonEvent(MouseHandler.ButtonType.LeftButton, MouseHandler.ButtonEventType.OnButtonDragging, () =>
            {

            });

        }

        public void Enter(params object[] args)
        {
            Console.WriteLine("--InGame State--");
        }

        public void Exit()
        {
            
        }

        public void Draw()
        {




            //player.Draw(spriteBatch);


            spriteBatch.Begin();
            if (MemoryManager.DebugMode)
            {
                spriteBatch.Draw(MemoryManager.TextureCache["TransparentWindow"], new Rectangle((int)player.camera.position.X, (int)player.camera.position.Y, g.SCREENWIDTH, g.SCREENHEIGHT / 3), Color.White);
                spriteBatch.Draw(MemoryManager.TextureCache["TransparentWindow"], new Rectangle((int)player.camera.position.X, (int)player.camera.position.Y, g.SCREENWIDTH, g.SCREENHEIGHT / 3), Color.White);
                spriteBatch.DrawString(MemoryManager.FontCache["Verdana"], "Debug Information", new Vector2(player.camera.position.X + 1, player.camera.position.Y + 5), Color.Black);
                spriteBatch.DrawString(MemoryManager.FontCache["Verdana"], "Debug Information", new Vector2(player.camera.position.X + 3, player.camera.position.Y + 7), Color.Black);
                spriteBatch.DrawString(MemoryManager.FontCache["Verdana"], "Debug Information", new Vector2(player.camera.position.X + 3, player.camera.position.Y + 5), Color.Black);
                spriteBatch.DrawString(MemoryManager.FontCache["Verdana"], "Debug Information", new Vector2(player.camera.position.X + 1, player.camera.position.Y + 7), Color.Black);
                spriteBatch.DrawString(MemoryManager.FontCache["Verdana"], "Debug Information", new Vector2(player.camera.position.X + 2, player.camera.position.Y + 6), Color.White);

                spriteBatch.DrawString(MemoryManager.FontCache["Verdana"], player.X + ", " + player.Y, new Vector2(player.camera.position.X + 2, player.camera.position.Y + 26), Color.White);

                spriteBatch.DrawString(MemoryManager.FontCache["Verdana"], player.destLocation.X + ", " + player.destLocation.Y, new Vector2(player.camera.position.X + 2, player.camera.position.Y + 46), Color.White);

                spriteBatch.DrawString(MemoryManager.FontCache["Verdana"], ".", player.position, Color.White);
                spriteBatch.Draw(MemoryManager.TextureCache["TransparentWindow"], player.position, new Rectangle(0, 0, 5, 5), Color.Pink);
                spriteBatch.Draw(MemoryManager.TextureCache["TransparentWindow"], player.position, new Rectangle(0, 0, 5, 5), Color.Pink);
                spriteBatch.Draw(MemoryManager.TextureCache["TransparentWindow"], player.position, new Rectangle(0, 0, 5, 5), Color.Pink);
                spriteBatch.Draw(MemoryManager.TextureCache["TransparentWindow"], player.position, new Rectangle(0, 0, 5, 5), Color.Pink);
                spriteBatch.Draw(MemoryManager.TextureCache["TransparentWindow"], player.position, new Rectangle(0, 0, 5, 5), Color.Pink);

            }
            spriteBatch.End();
        }

        public void Update(float deltaTime)
        {
            currentDeltaTime = deltaTime;
            HandleInput();
            player.Update(deltaTime);
        }

        public void HandleInput()
        {
            if (!g.IsActive) return;
            keyboard.checkInput(currentDeltaTime);

            //if clicking away from this window into something else within range, will move. BUG
            if (IsMouseInsideWindow()) {
                mouse.checkInput(currentDeltaTime);
            }
        }

        public bool IsMouseInsideWindow()
        {
            MouseState ms = Mouse.GetState();
            Point pos = new Point(ms.X, ms.Y);
            return g.GraphicsDevice.Viewport.Bounds.Contains(pos);
        }
    }
}
