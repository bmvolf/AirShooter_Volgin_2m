using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using AirShooter.Classes.Components;

namespace AirShooter.Classes
{
    public class GameOver
    {
        private List<Label> buttonList = new List<Label>();
        private Texture2D background;
        private Label lblScore;
        private Vector2 position = new Vector2(400, 200);
        private int selected = 0;
        private KeyboardState keyboardState;
        private KeyboardState prevkeyboardState;
        public event Action ClickAgain;
        public event Action ClickExit;
        public GameOver()
        {
            background = null;
            buttonList.Add(new Label("Try Again", position, Color.White));
            buttonList.Add(new Label("EXIT", new Vector2(position.X, position.Y + 60), Color.White));
            lblScore = new Label("Score: 00", new Vector2(position.X, position.Y + 120), Color.White);
        }
        public void LoadContent(ContentManager content)
        {
            background = content.Load<Texture2D>("endMenu");
            foreach (Label label in buttonList)
            {
                label.LoadContent(content);
                label.Position = new Vector2(label.Position.X - label.Width / 2, label.Position.Y);
            }
            lblScore.LoadContent(content);
            lblScore.Position = new Vector2(lblScore.Position.X - lblScore.Width / 2, lblScore.Position.Y);
        }
        public void Draw(SpriteBatch spriteBatch, int score)
        {
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
            for (int i = 0; i < buttonList.Count; i++)
            {
                if (selected == i)
                {
                    buttonList[i].Draw(spriteBatch, Color.DarkGoldenrod);
                }
                else
                {
                    buttonList[i].Draw(spriteBatch, Color.White);
                }
            }
            lblScore.Text = $"Score:  {score}";
            lblScore.Draw(spriteBatch, Color.White);
        }

        public void Update()
        {
            keyboardState = Keyboard.GetState();
            if (prevkeyboardState.IsKeyUp(Keys.S) && keyboardState.IsKeyDown(Keys.S))
            {
                if (selected < buttonList.Count - 1)
                {
                    selected++;
                }
            }
            if (prevkeyboardState.IsKeyUp(Keys.W) && keyboardState.IsKeyDown(Keys.W))
            {
                if (selected > 0)
                {
                    selected--;
                }
            }

            prevkeyboardState = keyboardState;
            //event click
            if (keyboardState.IsKeyDown(Keys.Enter))
            {
                if (selected == 0)
                {
                    if (ClickAgain != null)
                    {
                        ClickAgain();
                    }
                }
                else if (selected == 1)
                {
                    if (ClickExit != null)
                    {
                        ClickExit();
                    }
                }
            }
        }
    }
}
