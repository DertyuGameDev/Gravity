public class Subtitle
{
    public string Text { get; private set; }
    public float Duration { get; private set; }

    public Subtitle(string text, float duration)
    {
        Text = text;
        Duration = duration;
    }
}