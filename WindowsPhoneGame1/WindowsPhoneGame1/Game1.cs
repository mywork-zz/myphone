using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

namespace WindowsPhoneGame1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Camera camera;
        Matrix world = Matrix.Identity;
        BasicEffect basicEffect;

        VertexPositionColor[] triangle;

        Matrix translateMatrix = Matrix.Identity;
        Matrix scaleMatrix = Matrix.CreateScale(0.5f);

        Matrix rotateMatrix = Matrix.Identity;

       
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Frame rate is 30 fps by default for Windows Phone.
            TargetElapsedTime = TimeSpan.FromTicks(333333);

            // Extend battery life under lock.
            InactiveSleepTime = TimeSpan.FromSeconds(1);

            graphics.IsFullScreen = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //test
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            camera = new Camera(this, new Vector3(0, 0, 8), Vector3.Zero, Vector3.Up, MathHelper.PiOver4, GraphicsDevice.Viewport.AspectRatio, 1.0f, 8.0f);
            Components.Add(camera);
            basicEffect = new BasicEffect(GraphicsDevice);

            triangle = new VertexPositionColor[]{
                new VertexPositionColor(new Vector3(0,2,0), Color.Red),
                new VertexPositionColor(new Vector3(2,-2,0),Color.Green),
                new VertexPositionColor(new Vector3(-2,-2,0),Color.Blue)
            };


        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
        TouchPanel.EnabledGestures = GestureType.Tap;  
        if (TouchPanel.IsGestureAvailable)  
        {  
            GestureSample gestureSample = TouchPanel.ReadGesture();  
            if (gestureSample.GestureType == GestureType.Tap)  
            {  
                translateMatrix *= Matrix.CreateTranslation(0.3f, 0, 0);   
                //scaleMatrix *= Matrix.CreateScale(0.9f);  
                rotateMatrix *= Matrix.CreateRotationZ(MathHelper.ToRadians(10));


            }  
        } 

            base.Update(gameTime);
            
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //git test

            // TODO: Add your drawing code here
            RasterizerState rasterizerState = new RasterizerState();  
            rasterizerState.CullMode = CullMode.None;  
            GraphicsDevice.RasterizerState = rasterizerState;  


            basicEffect.World = scaleMatrix * translateMatrix * rotateMatrix;
            //basicEffect.World = scaleMatrix * rotateMatrix * translateMatrix;

            basicEffect.View = camera.view;
            basicEffect.Projection = camera.projection;
            basicEffect.VertexColorEnabled = true;

            foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleStrip, triangle, 0, 1);
            }
            
            base.Draw(gameTime);
        }
    }
}
