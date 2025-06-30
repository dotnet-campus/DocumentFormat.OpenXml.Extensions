using SixLabors.ImageSharp;

namespace DotNetCampus.MediaConverters.Imaging.Effect.Color;

public readonly struct ColorMatrixAdapt
{
    public ColorMatrixAdapt(ColorMatrix imageSharpColorMatrix)
    {
        _imageSharpColorMatrix = imageSharpColorMatrix;
    }

    private readonly ColorMatrix _imageSharpColorMatrix;

    public float Matrix00 => _imageSharpColorMatrix.M11;
    public float Matrix01 => _imageSharpColorMatrix.M12;
    public float Matrix02 => _imageSharpColorMatrix.M13;
    public float Matrix03 => _imageSharpColorMatrix.M14;
    public float Matrix10 => _imageSharpColorMatrix.M21;
    public float Matrix11 => _imageSharpColorMatrix.M22;
    public float Matrix12 => _imageSharpColorMatrix.M23;
    public float Matrix13 => _imageSharpColorMatrix.M24;
    public float Matrix20 => _imageSharpColorMatrix.M31;
    public float Matrix21 => _imageSharpColorMatrix.M32;
    public float Matrix22 => _imageSharpColorMatrix.M33;
    public float Matrix23 => _imageSharpColorMatrix.M34;
    public float Matrix30 => _imageSharpColorMatrix.M41;
    public float Matrix31 => _imageSharpColorMatrix.M42;
    public float Matrix32 => _imageSharpColorMatrix.M43;
    public float Matrix33 => _imageSharpColorMatrix.M44;
    public float Matrix40 => _imageSharpColorMatrix.M51;
    public float Matrix41 => _imageSharpColorMatrix.M52;
    public float Matrix42 => _imageSharpColorMatrix.M53;
    public float Matrix43 => _imageSharpColorMatrix.M54;

    public static implicit operator ColorMatrixAdapt(ColorMatrix imageSharpColorMatrix)
    {
        return new ColorMatrixAdapt(imageSharpColorMatrix);
    }
}