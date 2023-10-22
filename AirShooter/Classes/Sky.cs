using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AirShooter.Classes
{
    public class Sky
    {
        //fields
        public Vector2 position11;
        public Vector2 position12;
        public Vector2 position21;
        public Vector2 position22;
        public Vector2 position31;
        public Vector2 position32;
        public Texture2D texture1;
        public Texture2D texture2;
        public Texture2D texture3;
        private float speed1;
        private float speed2;
        private float speed3;
        //construct
        public Sky()
        {
            texture1 = null;
            texture2 = null;
            texture3 = null;
            position11 = Vector2.Zero;
            position12 = new Vector2(800, 0);
            position21 = Vector2.Zero;
            position22 = new Vector2(800, 0);
            position31 = Vector2.Zero;
            position32 = new Vector2(800, 0);
            speed1 = 1; 
            speed2 = 2;
            speed3 = 3;
        }
        //methods
        public void LoadContent(ContentManager content)
        {
            texture1 = content.Load<Texture2D>("mainbackground");
            texture2 = content.Load<Texture2D>("bgLayer1");
            texture3 = content.Load<Texture2D>("bgLayer2");
        }
        public void Update()
        {
            position11.X -= speed1;
            position12.X -= speed1;
            position21.X -= speed2;
            position22.X -= speed2;
            position31.X -= speed3;
            position32.X -= speed3;
            if(position12.X < 0)
            {
                position11.X = 0;
                position12.X = 800;
            }
            if(position22.X < 0)
            {
                position21.X = 0;
                position22.X = 800;
            }
            if(position32.X < 0)
            {
                position31.X = 0;
                position32.X = 800;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture1, position11, Color.White);
            spriteBatch.Draw(texture1, position12, Color.White);

            spriteBatch.Draw(texture2, position21, Color.White);
            spriteBatch.Draw(texture2, position22, Color.White);

            spriteBatch.Draw(texture3, position31, Color.White);
            spriteBatch.Draw(texture3, position32, Color.White);
        }

    }
}
