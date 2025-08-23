using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameLibrary;

public class Core : Game {

    internal static Core s_instance;
    public static Core Instance => s_instance;

    public static new ContentManager Content { get; private set; }
    public static new GraphicsDevice GraphicsDevice { get; private set; }
    public static GraphicsDeviceManager Graphics { get; private set; }
    public static SpriteBatch SpriteBatch { get; private set; }

    public Core(string title, int height, int width, bool isFullScreen) {
        s_instance = this;

        Content = base.Content;
        Content.RootDirectory = "Content";

        Graphics = new GraphicsDeviceManager(this);
        Graphics.IsFullScreen = isFullScreen;
        Graphics.PreferredBackBufferHeight = height;
        Graphics.PreferredBackBufferWidth = width;
        Graphics.ApplyChanges();

        IsMouseVisible = true;
        Window.Title = title;
    }

    protected override void Initialize() {
        base.Initialize();
        GraphicsDevice = base.GraphicsDevice;
        SpriteBatch = new SpriteBatch(GraphicsDevice);
    }
}
