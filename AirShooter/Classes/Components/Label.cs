using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AirShooter.Classes.Components
{
    public class Label
    {
        private Color color;
        private string text;
        private Vector2 position;
        private SpriteFont spriteFont;
        public Label(string text, Vector2 position, Color color)
        {
            spriteFont = null;
            this.text = text;
            this.position = position;
            this.color = color;
        }

        public float Width
        {
            get { return spriteFont.MeasureString(text).X; }
        }
        public string Text { get { return text; } set { text = value; } }
        public Vector2 Position { get { return position; } set { position = value; } }

        public void LoadContent(ContentManager content)
        {
            spriteFont = content.Load<SpriteFont>("gameFont");
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.DrawString(spriteFont, text, position, color);
        }
    }
}
