namespace JapaneseCallouts.Modules;

internal class EnemyBlip
{
    private static readonly List<EnemyBlip> AllItems = [];
    private const float SCALE = 0.7f;
    private const BlipSprite SPRITE = BlipSprite.Enemy;
    private readonly Color BlipColor = HudColor.Enemy.GetColor();

    internal Blip Blip { get; }
    private int Cooldown = 300;
    private Entity Entity { get; }
    private bool IsShown { get; set; } = false;

    internal EnemyBlip(Entity Entity)
    {
        this.Entity = Entity;
        if (Entity is not null && Entity.IsValid() && Entity.Exists())
        {
            Blip = new(Entity);
            if (Blip is not null && Blip.IsValid() && Blip.Exists())
            {
                Blip.Scale = SCALE;
                Blip.Sprite = SPRITE;
                Blip.Color = BlipColor;
                // Blip.Alpha = 0f;
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
                if (Entity.IsPedInLoS(Main.Player, 0.55f))
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