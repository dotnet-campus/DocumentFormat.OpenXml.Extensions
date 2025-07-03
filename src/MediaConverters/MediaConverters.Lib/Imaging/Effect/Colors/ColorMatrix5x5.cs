namespace DotNetCampus.MediaConverters.Imaging.Effect.Colors;

// ReSharper disable once InconsistentNaming
public readonly record struct ColorMatrix5x5()
{
    public float Matrix00 { get; init; } = 1.0f;
    public float Matrix01 { get; init; } = 0.0f;
    public float Matrix02 { get; init; } = 0.0f;
    public float Matrix03 { get; init; } = 0.0f;
    public float Matrix04 { get; init; } = 0.0f;
    public float Matrix10 { get; init; } = 0.0f;
    public float Matrix11 { get; init; } = 1.0f;
    public float Matrix12 { get; init; } = 0.0f;
    public float Matrix13 { get; init; } = 0.0f;
    public float Matrix14 { get; init; } = 0.0f;
    public float Matrix20 { get; init; } = 0.0f;
    public float Matrix21 { get; init; } = 0.0f;
    public float Matrix22 { get; init; } = 1.0f;
    public float Matrix23 { get; init; } = 0.0f;
    public float Matrix24 { get; init; } = 0.0f;
    public float Matrix30 { get; init; } = 0.0f;
    public float Matrix31 { get; init; } = 0.0f;
    public float Matrix32 { get; init; } = 0.0f;
    public float Matrix33 { get; init; } = 1.0f;
    public float Matrix34 { get; init; } = 0.0f;
    public float Matrix40 { get; init; } = 0.0f;
    public float Matrix41 { get; init; } = 0.0f;
    public float Matrix42 { get; init; } = 0.0f;
    public float Matrix43 { get; init; } = 0.0f;
    public float Matrix44 { get; init; } = 1.0f;
}