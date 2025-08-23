using Microsoft.Xna.Framework;
using MonoGameLibrary;

namespace CustomGameEngine;

public class Game1 : Core {

    public Game1() : base("Test", 720, 1280, false) {
    }

    protected override void Initialize() {
        base.Initialize();
    }

    protected override void LoadContent() {
    }

    protected override void Update(GameTime gameTime) {
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime) {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        base.Draw(gameTime);
    }
}
