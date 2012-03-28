using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace TestSpriteToScreen
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D ryu_red;        
        Vector2 tile_origin = new Vector2(0, 0);        
        Vector2 draw_origin;



        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
                
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
            base.Initialize();
        }
                
        protected override void LoadContent()
        {            
            spriteBatch = new SpriteBatch(GraphicsDevice);            
            ryu_red = Content.Load<Texture2D>("Images/Ryu");
            
            //Initialize the draw_origin with two rows and columsns of sprites as a buffer for scrolling
            draw_origin = new Vector2(-2*ryu_red.Height, -2*ryu_red.Width);
        }
        
        protected override void UnloadContent()
        {
            
        }

        
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();
            
            //Scroll controls
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                draw_origin.Y += 5;
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
                draw_origin.Y -= 5;
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                draw_origin.X -= 5;
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
                draw_origin.X += 5;

            //Move the draw_origin back if the user scrolls too close to it.
            if (draw_origin.X > -ryu_red.Width)
                draw_origin.X -= ryu_red.Width;
            if (draw_origin.Y > -ryu_red.Height)
                draw_origin.Y -= ryu_red.Height;

            base.Update(gameTime);
        }       
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteBlendMode.AlphaBlend,
                              SpriteSortMode.FrontToBack,
                              SaveStateMode.None);

            tile_origin = draw_origin;
            while (tile_origin.X < Window.ClientBounds.Width)
            {
                while (tile_origin.Y < Window.ClientBounds.Height)
                {
                    spriteBatch.Draw(ryu_red,
                                     tile_origin,
                                     null,
                                     Color.Red,
                                     0,
                                     Vector2.Zero,
                                     1f,
                                     SpriteEffects.None,
                                     0.5f
                                     );
                    tile_origin.Y += ryu_red.Height;
                }
                tile_origin.Y = draw_origin.Y;
                tile_origin.X += ryu_red.Width;
            }         
                       
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
