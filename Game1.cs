﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Races.Entities;

namespace Races
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Player player = new Player();

        //Manager Classes
        private SpriteManager spriteManager = new SpriteManager();
        private FloorManager floorManager = new FloorManager();
        private ObjectManager objectManager = new ObjectManager();
        private SoundManager soundManager = new SoundManager();
        

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            
            //Sprites 
            spriteManager.whitePixel = Content.Load<Texture2D>("WhitePixel");
            spriteManager.sprPlayer = Content.Load<Texture2D>("Sprites/character");

            spriteManager.sprmapOutside = Content.Load<Texture2D>("Sprites/Outside");

            #region ItemSprites
            spriteManager.sprItems[0] = Content.Load<Texture2D>("Sprites/Crystal");
            #endregion

            //Sounds

            soundManager.itemPickup[0] = Content.Load<SoundEffect>("Sounds/Glass").CreateInstance();

            //Entities
            objectManager.Initialize(spriteManager, 32);
            floorManager.Initialize(spriteManager, objectManager, 32);

            player.Initialize(new Rectangle(1, 1, 32, 32*2), spriteManager, Color.White, 32, floorManager, objectManager, soundManager);
        }

        protected override void Update(GameTime gameTime)
        {
            //KeyboardState keyboardState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            player.Update();

            

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Green);

            // TODO: Add your drawing code here
            SpriteBatch spriteBatch = new SpriteBatch(GraphicsDevice);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);

            floorManager.Draw(spriteBatch);
            objectManager.Draw(spriteBatch);
            player.Draw(spriteBatch);

            spriteBatch.End();
            
            
            base.Draw(gameTime);
        }
    }
}
