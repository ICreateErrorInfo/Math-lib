namespace Moarx.Rasterizer;

public record DirectAttributes {

    public DirectAttributes(DirectColor lineColor, int lineThickness)
        : this(lineColor, lineThickness, DirectColors.Transparent) {

    }

    public DirectAttributes(DirectColor lineColor, int lineThickness, DirectColor fillColor) {
        if (lineThickness < 0) {
            throw new ArgumentOutOfRangeException(nameof(lineThickness));
        }

        FillColor     = fillColor;
        LineColor     = lineColor;
        LineThickness = lineThickness;
    }

    public bool        IsFilled      => FillColor.A != 0;
    public DirectColor LineColor     { get; init; }
    public DirectColor FillColor     { get; init; }
    public int         LineThickness { get; init; }

}