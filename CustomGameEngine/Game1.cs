using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;

namespace CustomGameEngine;

public class Game1 : Core {

    Texture2D pixel;

    public Game1() : base("Test", 1300, 2200, false) {
    }

    protected override void Initialize() {
        Window.ClientSizeChanged += (sender, args) => {
            if (sender is GameWindow window) {
                UpdateWindowDimensions(window);
            }
        };

        base.Initialize();
    }

    protected override void LoadContent() {
        UpdateWindowDimensions(Window);

        pixel = new Texture2D(base.GraphicsDevice, 1, 1);
        pixel.SetData(new[] { Color.White });

        LogicNew.Init();
    }

    private static readonly float TimePerStep = 0.15f;
    private float TickTracker = 0f;

    protected override void Update(GameTime gameTime) {
        base.Update(gameTime);
        CheckInput();
        float delta = (float) gameTime.ElapsedGameTime.TotalSeconds;

        LogicNew.Tick(delta);

        TickTracker += delta;
        while (TickTracker >= TimePerStep) {
            // LogicNew.Tick(delta);
            TickTracker -= TimePerStep;
        }
    }

    protected override void Draw(GameTime gameTime) {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        SpriteBatch.Begin();

        foreach (var kvp in LogicNew.Voxels) {
            Vector coords = WindowManager.ToRenderCoords(kvp.Key);
            SpriteBatch.Draw(pixel, new Rectangle(coords.X, coords.Y, WindowManager.ZoomLevel, WindowManager.ZoomLevel), Color.Red);
        }

        SpriteBatch.End();
        base.Draw(gameTime);
    }

    private static void CheckInput() {
        MouseState mouse = Mouse.GetState();
        Vector mouseCoords = WindowManager.ToWorldCoords(new Vector(mouse.X, mouse.Y));

        if (mouse.LeftButton == ButtonState.Pressed) {
            LogicNew.CreateVoxel(mouseCoords.X, mouseCoords.Y);
        }
    }

    private static void UpdateWindowDimensions(GameWindow window) {
        WindowManager.UpdateWindowDimensions(window.ClientBounds.Width, window.ClientBounds.Height);
    }
}
