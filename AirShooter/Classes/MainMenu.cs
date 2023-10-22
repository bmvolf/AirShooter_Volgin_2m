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
    public class MainMenu
    {
        private List<Label> buttonList = new List<Label>();
        private Texture2D texture;
        private int selected;
        private KeyboardState keyboardState;
        private KeyboardState prevkeyboardState;
        public event Action MenuClickPlay;
        public event Action MenuClickExit;
        private Vector2 position = new Vector2(400, 250);

        public MainMenu()
        {
            selected = 0;
            buttonList.Add(new Label("Play", position, Color.White));
            buttonList.Add(new Label("EXIT", new Vector2(position.X, position.Y + 60), Color.White));
            texture = null;
        }

        public void LoadContent(ContentManager content)
        {
            foreach (Label label in buttonList)
            {
                label.LoadContent(content);
            }
            foreach (Label label in buttonList)
            {
                label.Position = new Vector2(label.Position.X - label.Width / 2, label.Position.Y);
            }
            texture = content.Load<Texture2D>("mainMenu");
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
                    if (MenuClickExit != null)
                    {
                        MenuClickPlay();
                    }
                }
                else if (selected == 1)
                {
                    if (MenuClickExit != null)
                    {
                        MenuClickExit();
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Vector2.Zero, Color.White);
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
        }
    }
}
