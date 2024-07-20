namespace JapaneseCallouts.Modules;

internal class BlipPlus
{
    private static readonly List<BlipPlus> AllItems = [];
    private const float SCALE = 0.7f;

    internal Blip Blip { get; }
    private int Cooldown = 300;
    private Entity Entity { get; }
    private bool IsShown { get; set; } = false;

    internal BlipPlus(Entity Entity, Color Color, BlipSprite Sprite)
    {
        this.Entity = Entity;
        if (Entity is not null && Entity.IsValid() && Entity.Exists())
        {
            Blip = new(Entity);
            if (Blip is not null && Blip.IsValid() && Blip.Exists())
            {
                Blip.Scale = SCALE;
                Blip.Sprite = Sprite;
                Blip.Color = Color;
            }
        }
        GameFiber.StartNew(Update);
        AllItems.Add(this);
    }

    internal static void Initialize()
    {
        GameFiber.StartNew(UpdateG);
    }

    private void Update()
    {
        while (true)
        {
            GameFiber.Yield();
            if (Entity is not null && Entity.IsValid() && Entity.Exists())
            {
                if (Entity.IsPedInLoS(Game.LocalPlayer.Character, 0.55f))
                {
                    Cooldown = 300;
                    IsShown = true;
                }
                else
                {
                    if (Cooldown > 0)
                    {
                        Cooldown--;
                    }
                    else
                    {
                        IsShown = false;
                    }
                }

                if (Entity.IsDead)
                {
                    break;
                }
            }
        }
        Dismiss();
    }

    private static void UpdateG()
    {
        while (true)
        {
            GameFiber.Yield();
            foreach (var eb in AllItems)
            {
                if (eb.Blip is not null && eb.Blip.IsValid())
                {
                    if (eb.IsShown)
                    {
                        if (eb.Blip.Alpha < 1.0f)
                        {
                            if (eb.Blip.Alpha > 0.9f)
                            {
                                eb.Blip.Alpha = 1f;
                            }
                            else
                            {
                                eb.Blip.Alpha += 0.1f;
                            }
                        }
                    }
                    else
                    {
                        if (eb.Blip.Alpha > 0f)
                        {
                            if (eb.Blip.Alpha < 0.1f)
                            {
                                eb.Blip.Alpha = 0f;
                            }
                            else
                            {
                                eb.Blip.Alpha -= 0.1f;
                            }
                        }
                    }
                }
            }
        }
    }

    internal void Dismiss()
    {
        if (Blip is not null && Blip.IsValid() && Blip.Exists())
        {
            Blip.Delete();
        }
        if (AllItems.Contains(this)) AllItems.Remove(this);
    }
}