using MyGame;

var scene = Scene.Build<MainScene>();

var config = Config
    .Builder()
    .Display(display =>
    {
        display.Size = (720, 720);
        display.Title = "My Game";
        display.Icon = Image.Resource("icon.png");
    })
    .Input(input =>
    {
        input.FullscreenButton = Key.Tab;
        input.ExitButton = Key.Escape;
    })
    .Build();

Game.Launch(config, scene);
