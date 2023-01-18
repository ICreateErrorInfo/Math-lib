namespace Moarx.Rasterizer {

    #region Powershell Script
    /* 
    Add-Type -AssemblyName System.Drawing 

    function GenerateColors(){

        $flags     = [Reflection.BindingFlags]::Public + [Reflection.BindingFlags]::Static
        $colorType = [System.Drawing.Color]
        $props     = [System.Drawing.Color].GetProperties($flags)  | where PropertyType -eq $colorType

        'public static class DirectColors {'
	
        $props | % { $_.GetValue($null, $null) } | Where Name -ne '' | % { 
        
            $hexValue='#{0:X2}{1:X2}{2:X2}{3:X2}' -f $_.A,$_.R,$_.G,$_.B
		
            ""
            "    /// <summary>"
            "    /// Name: $($_.Name)"
            "    /// RGBA: R: $($_.R), G: $($_.G), B: $($_.B), A: $($_.A)"
            "    /// Hex:  $hexValue"
            "    /// </summary>"
            "    public static readonly DirectColor $($_.Name) = DirectColor.FromRgba(r: $($_.R), g: $($_.G), b: $($_.B), a: $($_.A));"
        
        }
	
        '}'
    }

    GenerateColors
    */
    #endregion

    public static class DirectColors {

        /// <summary>
        /// Name: Transparent
        /// RGBA: R: 255, G: 255, B: 255, A: 0
        /// Hex:  #00FFFFFF
        /// </summary>
        public static readonly DirectColor Transparent = DirectColor.FromRgba(r: 255, g: 255, b: 255, a: 0);

        /// <summary>
        /// Name: AliceBlue
        /// RGBA: R: 240, G: 248, B: 255, A: 255
        /// Hex:  #FFF0F8FF
        /// </summary>
        public static readonly DirectColor AliceBlue = DirectColor.FromRgba(r: 240, g: 248, b: 255, a: 255);

        /// <summary>
        /// Name: AntiqueWhite
        /// RGBA: R: 250, G: 235, B: 215, A: 255
        /// Hex:  #FFFAEBD7
        /// </summary>
        public static readonly DirectColor AntiqueWhite = DirectColor.FromRgba(r: 250, g: 235, b: 215, a: 255);

        /// <summary>
        /// Name: Aqua
        /// RGBA: R: 0, G: 255, B: 255, A: 255
        /// Hex:  #FF00FFFF
        /// </summary>
        public static readonly DirectColor Aqua = DirectColor.FromRgba(r: 0, g: 255, b: 255, a: 255);

        /// <summary>
        /// Name: Aquamarine
        /// RGBA: R: 127, G: 255, B: 212, A: 255
        /// Hex:  #FF7FFFD4
        /// </summary>
        public static readonly DirectColor Aquamarine = DirectColor.FromRgba(r: 127, g: 255, b: 212, a: 255);

        /// <summary>
        /// Name: Azure
        /// RGBA: R: 240, G: 255, B: 255, A: 255
        /// Hex:  #FFF0FFFF
        /// </summary>
        public static readonly DirectColor Azure = DirectColor.FromRgba(r: 240, g: 255, b: 255, a: 255);

        /// <summary>
        /// Name: Beige
        /// RGBA: R: 245, G: 245, B: 220, A: 255
        /// Hex:  #FFF5F5DC
        /// </summary>
        public static readonly DirectColor Beige = DirectColor.FromRgba(r: 245, g: 245, b: 220, a: 255);

        /// <summary>
        /// Name: Bisque
        /// RGBA: R: 255, G: 228, B: 196, A: 255
        /// Hex:  #FFFFE4C4
        /// </summary>
        public static readonly DirectColor Bisque = DirectColor.FromRgba(r: 255, g: 228, b: 196, a: 255);

        /// <summary>
        /// Name: Black
        /// RGBA: R: 0, G: 0, B: 0, A: 255
        /// Hex:  #FF000000
        /// </summary>
        public static readonly DirectColor Black = DirectColor.FromRgba(r: 0, g: 0, b: 0, a: 255);

        /// <summary>
        /// Name: BlanchedAlmond
        /// RGBA: R: 255, G: 235, B: 205, A: 255
        /// Hex:  #FFFFEBCD
        /// </summary>
        public static readonly DirectColor BlanchedAlmond = DirectColor.FromRgba(r: 255, g: 235, b: 205, a: 255);

        /// <summary>
        /// Name: Blue
        /// RGBA: R: 0, G: 0, B: 255, A: 255
        /// Hex:  #FF0000FF
        /// </summary>
        public static readonly DirectColor Blue = DirectColor.FromRgba(r: 0, g: 0, b: 255, a: 255);

        /// <summary>
        /// Name: BlueViolet
        /// RGBA: R: 138, G: 43, B: 226, A: 255
        /// Hex:  #FF8A2BE2
        /// </summary>
        public static readonly DirectColor BlueViolet = DirectColor.FromRgba(r: 138, g: 43, b: 226, a: 255);

        /// <summary>
        /// Name: Brown
        /// RGBA: R: 165, G: 42, B: 42, A: 255
        /// Hex:  #FFA52A2A
        /// </summary>
        public static readonly DirectColor Brown = DirectColor.FromRgba(r: 165, g: 42, b: 42, a: 255);

        /// <summary>
        /// Name: BurlyWood
        /// RGBA: R: 222, G: 184, B: 135, A: 255
        /// Hex:  #FFDEB887
        /// </summary>
        public static readonly DirectColor BurlyWood = DirectColor.FromRgba(r: 222, g: 184, b: 135, a: 255);

        /// <summary>
        /// Name: CadetBlue
        /// RGBA: R: 95, G: 158, B: 160, A: 255
        /// Hex:  #FF5F9EA0
        /// </summary>
        public static readonly DirectColor CadetBlue = DirectColor.FromRgba(r: 95, g: 158, b: 160, a: 255);

        /// <summary>
        /// Name: Chartreuse
        /// RGBA: R: 127, G: 255, B: 0, A: 255
        /// Hex:  #FF7FFF00
        /// </summary>
        public static readonly DirectColor Chartreuse = DirectColor.FromRgba(r: 127, g: 255, b: 0, a: 255);

        /// <summary>
        /// Name: Chocolate
        /// RGBA: R: 210, G: 105, B: 30, A: 255
        /// Hex:  #FFD2691E
        /// </summary>
        public static readonly DirectColor Chocolate = DirectColor.FromRgba(r: 210, g: 105, b: 30, a: 255);

        /// <summary>
        /// Name: Coral
        /// RGBA: R: 255, G: 127, B: 80, A: 255
        /// Hex:  #FFFF7F50
        /// </summary>
        public static readonly DirectColor Coral = DirectColor.FromRgba(r: 255, g: 127, b: 80, a: 255);

        /// <summary>
        /// Name: CornflowerBlue
        /// RGBA: R: 100, G: 149, B: 237, A: 255
        /// Hex:  #FF6495ED
        /// </summary>
        public static readonly DirectColor CornflowerBlue = DirectColor.FromRgba(r: 100, g: 149, b: 237, a: 255);

        /// <summary>
        /// Name: Cornsilk
        /// RGBA: R: 255, G: 248, B: 220, A: 255
        /// Hex:  #FFFFF8DC
        /// </summary>
        public static readonly DirectColor Cornsilk = DirectColor.FromRgba(r: 255, g: 248, b: 220, a: 255);

        /// <summary>
        /// Name: Crimson
        /// RGBA: R: 220, G: 20, B: 60, A: 255
        /// Hex:  #FFDC143C
        /// </summary>
        public static readonly DirectColor Crimson = DirectColor.FromRgba(r: 220, g: 20, b: 60, a: 255);

        /// <summary>
        /// Name: Cyan
        /// RGBA: R: 0, G: 255, B: 255, A: 255
        /// Hex:  #FF00FFFF
        /// </summary>
        public static readonly DirectColor Cyan = DirectColor.FromRgba(r: 0, g: 255, b: 255, a: 255);

        /// <summary>
        /// Name: DarkBlue
        /// RGBA: R: 0, G: 0, B: 139, A: 255
        /// Hex:  #FF00008B
        /// </summary>
        public static readonly DirectColor DarkBlue = DirectColor.FromRgba(r: 0, g: 0, b: 139, a: 255);

        /// <summary>
        /// Name: DarkCyan
        /// RGBA: R: 0, G: 139, B: 139, A: 255
        /// Hex:  #FF008B8B
        /// </summary>
        public static readonly DirectColor DarkCyan = DirectColor.FromRgba(r: 0, g: 139, b: 139, a: 255);

        /// <summary>
        /// Name: DarkGoldenrod
        /// RGBA: R: 184, G: 134, B: 11, A: 255
        /// Hex:  #FFB8860B
        /// </summary>
        public static readonly DirectColor DarkGoldenrod = DirectColor.FromRgba(r: 184, g: 134, b: 11, a: 255);

        /// <summary>
        /// Name: DarkGray
        /// RGBA: R: 169, G: 169, B: 169, A: 255
        /// Hex:  #FFA9A9A9
        /// </summary>
        public static readonly DirectColor DarkGray = DirectColor.FromRgba(r: 169, g: 169, b: 169, a: 255);

        /// <summary>
        /// Name: DarkGreen
        /// RGBA: R: 0, G: 100, B: 0, A: 255
        /// Hex:  #FF006400
        /// </summary>
        public static readonly DirectColor DarkGreen = DirectColor.FromRgba(r: 0, g: 100, b: 0, a: 255);

        /// <summary>
        /// Name: DarkKhaki
        /// RGBA: R: 189, G: 183, B: 107, A: 255
        /// Hex:  #FFBDB76B
        /// </summary>
        public static readonly DirectColor DarkKhaki = DirectColor.FromRgba(r: 189, g: 183, b: 107, a: 255);

        /// <summary>
        /// Name: DarkMagenta
        /// RGBA: R: 139, G: 0, B: 139, A: 255
        /// Hex:  #FF8B008B
        /// </summary>
        public static readonly DirectColor DarkMagenta = DirectColor.FromRgba(r: 139, g: 0, b: 139, a: 255);

        /// <summary>
        /// Name: DarkOliveGreen
        /// RGBA: R: 85, G: 107, B: 47, A: 255
        /// Hex:  #FF556B2F
        /// </summary>
        public static readonly DirectColor DarkOliveGreen = DirectColor.FromRgba(r: 85, g: 107, b: 47, a: 255);

        /// <summary>
        /// Name: DarkOrange
        /// RGBA: R: 255, G: 140, B: 0, A: 255
        /// Hex:  #FFFF8C00
        /// </summary>
        public static readonly DirectColor DarkOrange = DirectColor.FromRgba(r: 255, g: 140, b: 0, a: 255);

        /// <summary>
        /// Name: DarkOrchid
        /// RGBA: R: 153, G: 50, B: 204, A: 255
        /// Hex:  #FF9932CC
        /// </summary>
        public static readonly DirectColor DarkOrchid = DirectColor.FromRgba(r: 153, g: 50, b: 204, a: 255);

        /// <summary>
        /// Name: DarkRed
        /// RGBA: R: 139, G: 0, B: 0, A: 255
        /// Hex:  #FF8B0000
        /// </summary>
        public static readonly DirectColor DarkRed = DirectColor.FromRgba(r: 139, g: 0, b: 0, a: 255);

        /// <summary>
        /// Name: DarkSalmon
        /// RGBA: R: 233, G: 150, B: 122, A: 255
        /// Hex:  #FFE9967A
        /// </summary>
        public static readonly DirectColor DarkSalmon = DirectColor.FromRgba(r: 233, g: 150, b: 122, a: 255);

        /// <summary>
        /// Name: DarkSeaGreen
        /// RGBA: R: 143, G: 188, B: 143, A: 255
        /// Hex:  #FF8FBC8F
        /// </summary>
        public static readonly DirectColor DarkSeaGreen = DirectColor.FromRgba(r: 143, g: 188, b: 143, a: 255);

        /// <summary>
        /// Name: DarkSlateBlue
        /// RGBA: R: 72, G: 61, B: 139, A: 255
        /// Hex:  #FF483D8B
        /// </summary>
        public static readonly DirectColor DarkSlateBlue = DirectColor.FromRgba(r: 72, g: 61, b: 139, a: 255);

        /// <summary>
        /// Name: DarkSlateGray
        /// RGBA: R: 47, G: 79, B: 79, A: 255
        /// Hex:  #FF2F4F4F
        /// </summary>
        public static readonly DirectColor DarkSlateGray = DirectColor.FromRgba(r: 47, g: 79, b: 79, a: 255);

        /// <summary>
        /// Name: DarkTurquoise
        /// RGBA: R: 0, G: 206, B: 209, A: 255
        /// Hex:  #FF00CED1
        /// </summary>
        public static readonly DirectColor DarkTurquoise = DirectColor.FromRgba(r: 0, g: 206, b: 209, a: 255);

        /// <summary>
        /// Name: DarkViolet
        /// RGBA: R: 148, G: 0, B: 211, A: 255
        /// Hex:  #FF9400D3
        /// </summary>
        public static readonly DirectColor DarkViolet = DirectColor.FromRgba(r: 148, g: 0, b: 211, a: 255);

        /// <summary>
        /// Name: DeepPink
        /// RGBA: R: 255, G: 20, B: 147, A: 255
        /// Hex:  #FFFF1493
        /// </summary>
        public static readonly DirectColor DeepPink = DirectColor.FromRgba(r: 255, g: 20, b: 147, a: 255);

        /// <summary>
        /// Name: DeepSkyBlue
        /// RGBA: R: 0, G: 191, B: 255, A: 255
        /// Hex:  #FF00BFFF
        /// </summary>
        public static readonly DirectColor DeepSkyBlue = DirectColor.FromRgba(r: 0, g: 191, b: 255, a: 255);

        /// <summary>
        /// Name: DimGray
        /// RGBA: R: 105, G: 105, B: 105, A: 255
        /// Hex:  #FF696969
        /// </summary>
        public static readonly DirectColor DimGray = DirectColor.FromRgba(r: 105, g: 105, b: 105, a: 255);

        /// <summary>
        /// Name: DodgerBlue
        /// RGBA: R: 30, G: 144, B: 255, A: 255
        /// Hex:  #FF1E90FF
        /// </summary>
        public static readonly DirectColor DodgerBlue = DirectColor.FromRgba(r: 30, g: 144, b: 255, a: 255);

        /// <summary>
        /// Name: Firebrick
        /// RGBA: R: 178, G: 34, B: 34, A: 255
        /// Hex:  #FFB22222
        /// </summary>
        public static readonly DirectColor Firebrick = DirectColor.FromRgba(r: 178, g: 34, b: 34, a: 255);

        /// <summary>
        /// Name: FloralWhite
        /// RGBA: R: 255, G: 250, B: 240, A: 255
        /// Hex:  #FFFFFAF0
        /// </summary>
        public static readonly DirectColor FloralWhite = DirectColor.FromRgba(r: 255, g: 250, b: 240, a: 255);

        /// <summary>
        /// Name: ForestGreen
        /// RGBA: R: 34, G: 139, B: 34, A: 255
        /// Hex:  #FF228B22
        /// </summary>
        public static readonly DirectColor ForestGreen = DirectColor.FromRgba(r: 34, g: 139, b: 34, a: 255);

        /// <summary>
        /// Name: Fuchsia
        /// RGBA: R: 255, G: 0, B: 255, A: 255
        /// Hex:  #FFFF00FF
        /// </summary>
        public static readonly DirectColor Fuchsia = DirectColor.FromRgba(r: 255, g: 0, b: 255, a: 255);

        /// <summary>
        /// Name: Gainsboro
        /// RGBA: R: 220, G: 220, B: 220, A: 255
        /// Hex:  #FFDCDCDC
        /// </summary>
        public static readonly DirectColor Gainsboro = DirectColor.FromRgba(r: 220, g: 220, b: 220, a: 255);

        /// <summary>
        /// Name: GhostWhite
        /// RGBA: R: 248, G: 248, B: 255, A: 255
        /// Hex:  #FFF8F8FF
        /// </summary>
        public static readonly DirectColor GhostWhite = DirectColor.FromRgba(r: 248, g: 248, b: 255, a: 255);

        /// <summary>
        /// Name: Gold
        /// RGBA: R: 255, G: 215, B: 0, A: 255
        /// Hex:  #FFFFD700
        /// </summary>
        public static readonly DirectColor Gold = DirectColor.FromRgba(r: 255, g: 215, b: 0, a: 255);

        /// <summary>
        /// Name: Goldenrod
        /// RGBA: R: 218, G: 165, B: 32, A: 255
        /// Hex:  #FFDAA520
        /// </summary>
        public static readonly DirectColor Goldenrod = DirectColor.FromRgba(r: 218, g: 165, b: 32, a: 255);

        /// <summary>
        /// Name: Gray
        /// RGBA: R: 128, G: 128, B: 128, A: 255
        /// Hex:  #FF808080
        /// </summary>
        public static readonly DirectColor Gray = DirectColor.FromRgba(r: 128, g: 128, b: 128, a: 255);

        /// <summary>
        /// Name: Green
        /// RGBA: R: 0, G: 128, B: 0, A: 255
        /// Hex:  #FF008000
        /// </summary>
        public static readonly DirectColor Green = DirectColor.FromRgba(r: 0, g: 128, b: 0, a: 255);

        /// <summary>
        /// Name: GreenYellow
        /// RGBA: R: 173, G: 255, B: 47, A: 255
        /// Hex:  #FFADFF2F
        /// </summary>
        public static readonly DirectColor GreenYellow = DirectColor.FromRgba(r: 173, g: 255, b: 47, a: 255);

        /// <summary>
        /// Name: Honeydew
        /// RGBA: R: 240, G: 255, B: 240, A: 255
        /// Hex:  #FFF0FFF0
        /// </summary>
        public static readonly DirectColor Honeydew = DirectColor.FromRgba(r: 240, g: 255, b: 240, a: 255);

        /// <summary>
        /// Name: HotPink
        /// RGBA: R: 255, G: 105, B: 180, A: 255
        /// Hex:  #FFFF69B4
        /// </summary>
        public static readonly DirectColor HotPink = DirectColor.FromRgba(r: 255, g: 105, b: 180, a: 255);

        /// <summary>
        /// Name: IndianRed
        /// RGBA: R: 205, G: 92, B: 92, A: 255
        /// Hex:  #FFCD5C5C
        /// </summary>
        public static readonly DirectColor IndianRed = DirectColor.FromRgba(r: 205, g: 92, b: 92, a: 255);

        /// <summary>
        /// Name: Indigo
        /// RGBA: R: 75, G: 0, B: 130, A: 255
        /// Hex:  #FF4B0082
        /// </summary>
        public static readonly DirectColor Indigo = DirectColor.FromRgba(r: 75, g: 0, b: 130, a: 255);

        /// <summary>
        /// Name: Ivory
        /// RGBA: R: 255, G: 255, B: 240, A: 255
        /// Hex:  #FFFFFFF0
        /// </summary>
        public static readonly DirectColor Ivory = DirectColor.FromRgba(r: 255, g: 255, b: 240, a: 255);

        /// <summary>
        /// Name: Khaki
        /// RGBA: R: 240, G: 230, B: 140, A: 255
        /// Hex:  #FFF0E68C
        /// </summary>
        public static readonly DirectColor Khaki = DirectColor.FromRgba(r: 240, g: 230, b: 140, a: 255);

        /// <summary>
        /// Name: Lavender
        /// RGBA: R: 230, G: 230, B: 250, A: 255
        /// Hex:  #FFE6E6FA
        /// </summary>
        public static readonly DirectColor Lavender = DirectColor.FromRgba(r: 230, g: 230, b: 250, a: 255);

        /// <summary>
        /// Name: LavenderBlush
        /// RGBA: R: 255, G: 240, B: 245, A: 255
        /// Hex:  #FFFFF0F5
        /// </summary>
        public static readonly DirectColor LavenderBlush = DirectColor.FromRgba(r: 255, g: 240, b: 245, a: 255);

        /// <summary>
        /// Name: LawnGreen
        /// RGBA: R: 124, G: 252, B: 0, A: 255
        /// Hex:  #FF7CFC00
        /// </summary>
        public static readonly DirectColor LawnGreen = DirectColor.FromRgba(r: 124, g: 252, b: 0, a: 255);

        /// <summary>
        /// Name: LemonChiffon
        /// RGBA: R: 255, G: 250, B: 205, A: 255
        /// Hex:  #FFFFFACD
        /// </summary>
        public static readonly DirectColor LemonChiffon = DirectColor.FromRgba(r: 255, g: 250, b: 205, a: 255);

        /// <summary>
        /// Name: LightBlue
        /// RGBA: R: 173, G: 216, B: 230, A: 255
        /// Hex:  #FFADD8E6
        /// </summary>
        public static readonly DirectColor LightBlue = DirectColor.FromRgba(r: 173, g: 216, b: 230, a: 255);

        /// <summary>
        /// Name: LightCoral
        /// RGBA: R: 240, G: 128, B: 128, A: 255
        /// Hex:  #FFF08080
        /// </summary>
        public static readonly DirectColor LightCoral = DirectColor.FromRgba(r: 240, g: 128, b: 128, a: 255);

        /// <summary>
        /// Name: LightCyan
        /// RGBA: R: 224, G: 255, B: 255, A: 255
        /// Hex:  #FFE0FFFF
        /// </summary>
        public static readonly DirectColor LightCyan = DirectColor.FromRgba(r: 224, g: 255, b: 255, a: 255);

        /// <summary>
        /// Name: LightGoldenrodYellow
        /// RGBA: R: 250, G: 250, B: 210, A: 255
        /// Hex:  #FFFAFAD2
        /// </summary>
        public static readonly DirectColor LightGoldenrodYellow = DirectColor.FromRgba(r: 250, g: 250, b: 210, a: 255);

        /// <summary>
        /// Name: LightGreen
        /// RGBA: R: 144, G: 238, B: 144, A: 255
        /// Hex:  #FF90EE90
        /// </summary>
        public static readonly DirectColor LightGreen = DirectColor.FromRgba(r: 144, g: 238, b: 144, a: 255);

        /// <summary>
        /// Name: LightGray
        /// RGBA: R: 211, G: 211, B: 211, A: 255
        /// Hex:  #FFD3D3D3
        /// </summary>
        public static readonly DirectColor LightGray = DirectColor.FromRgba(r: 211, g: 211, b: 211, a: 255);

        /// <summary>
        /// Name: LightPink
        /// RGBA: R: 255, G: 182, B: 193, A: 255
        /// Hex:  #FFFFB6C1
        /// </summary>
        public static readonly DirectColor LightPink = DirectColor.FromRgba(r: 255, g: 182, b: 193, a: 255);

        /// <summary>
        /// Name: LightSalmon
        /// RGBA: R: 255, G: 160, B: 122, A: 255
        /// Hex:  #FFFFA07A
        /// </summary>
        public static readonly DirectColor LightSalmon = DirectColor.FromRgba(r: 255, g: 160, b: 122, a: 255);

        /// <summary>
        /// Name: LightSeaGreen
        /// RGBA: R: 32, G: 178, B: 170, A: 255
        /// Hex:  #FF20B2AA
        /// </summary>
        public static readonly DirectColor LightSeaGreen = DirectColor.FromRgba(r: 32, g: 178, b: 170, a: 255);

        /// <summary>
        /// Name: LightSkyBlue
        /// RGBA: R: 135, G: 206, B: 250, A: 255
        /// Hex:  #FF87CEFA
        /// </summary>
        public static readonly DirectColor LightSkyBlue = DirectColor.FromRgba(r: 135, g: 206, b: 250, a: 255);

        /// <summary>
        /// Name: LightSlateGray
        /// RGBA: R: 119, G: 136, B: 153, A: 255
        /// Hex:  #FF778899
        /// </summary>
        public static readonly DirectColor LightSlateGray = DirectColor.FromRgba(r: 119, g: 136, b: 153, a: 255);

        /// <summary>
        /// Name: LightSteelBlue
        /// RGBA: R: 176, G: 196, B: 222, A: 255
        /// Hex:  #FFB0C4DE
        /// </summary>
        public static readonly DirectColor LightSteelBlue = DirectColor.FromRgba(r: 176, g: 196, b: 222, a: 255);

        /// <summary>
        /// Name: LightYellow
        /// RGBA: R: 255, G: 255, B: 224, A: 255
        /// Hex:  #FFFFFFE0
        /// </summary>
        public static readonly DirectColor LightYellow = DirectColor.FromRgba(r: 255, g: 255, b: 224, a: 255);

        /// <summary>
        /// Name: Lime
        /// RGBA: R: 0, G: 255, B: 0, A: 255
        /// Hex:  #FF00FF00
        /// </summary>
        public static readonly DirectColor Lime = DirectColor.FromRgba(r: 0, g: 255, b: 0, a: 255);

        /// <summary>
        /// Name: LimeGreen
        /// RGBA: R: 50, G: 205, B: 50, A: 255
        /// Hex:  #FF32CD32
        /// </summary>
        public static readonly DirectColor LimeGreen = DirectColor.FromRgba(r: 50, g: 205, b: 50, a: 255);

        /// <summary>
        /// Name: Linen
        /// RGBA: R: 250, G: 240, B: 230, A: 255
        /// Hex:  #FFFAF0E6
        /// </summary>
        public static readonly DirectColor Linen = DirectColor.FromRgba(r: 250, g: 240, b: 230, a: 255);

        /// <summary>
        /// Name: Magenta
        /// RGBA: R: 255, G: 0, B: 255, A: 255
        /// Hex:  #FFFF00FF
        /// </summary>
        public static readonly DirectColor Magenta = DirectColor.FromRgba(r: 255, g: 0, b: 255, a: 255);

        /// <summary>
        /// Name: Maroon
        /// RGBA: R: 128, G: 0, B: 0, A: 255
        /// Hex:  #FF800000
        /// </summary>
        public static readonly DirectColor Maroon = DirectColor.FromRgba(r: 128, g: 0, b: 0, a: 255);

        /// <summary>
        /// Name: MediumAquamarine
        /// RGBA: R: 102, G: 205, B: 170, A: 255
        /// Hex:  #FF66CDAA
        /// </summary>
        public static readonly DirectColor MediumAquamarine = DirectColor.FromRgba(r: 102, g: 205, b: 170, a: 255);

        /// <summary>
        /// Name: MediumBlue
        /// RGBA: R: 0, G: 0, B: 205, A: 255
        /// Hex:  #FF0000CD
        /// </summary>
        public static readonly DirectColor MediumBlue = DirectColor.FromRgba(r: 0, g: 0, b: 205, a: 255);

        /// <summary>
        /// Name: MediumOrchid
        /// RGBA: R: 186, G: 85, B: 211, A: 255
        /// Hex:  #FFBA55D3
        /// </summary>
        public static readonly DirectColor MediumOrchid = DirectColor.FromRgba(r: 186, g: 85, b: 211, a: 255);

        /// <summary>
        /// Name: MediumPurple
        /// RGBA: R: 147, G: 112, B: 219, A: 255
        /// Hex:  #FF9370DB
        /// </summary>
        public static readonly DirectColor MediumPurple = DirectColor.FromRgba(r: 147, g: 112, b: 219, a: 255);

        /// <summary>
        /// Name: MediumSeaGreen
        /// RGBA: R: 60, G: 179, B: 113, A: 255
        /// Hex:  #FF3CB371
        /// </summary>
        public static readonly DirectColor MediumSeaGreen = DirectColor.FromRgba(r: 60, g: 179, b: 113, a: 255);

        /// <summary>
        /// Name: MediumSlateBlue
        /// RGBA: R: 123, G: 104, B: 238, A: 255
        /// Hex:  #FF7B68EE
        /// </summary>
        public static readonly DirectColor MediumSlateBlue = DirectColor.FromRgba(r: 123, g: 104, b: 238, a: 255);

        /// <summary>
        /// Name: MediumSpringGreen
        /// RGBA: R: 0, G: 250, B: 154, A: 255
        /// Hex:  #FF00FA9A
        /// </summary>
        public static readonly DirectColor MediumSpringGreen = DirectColor.FromRgba(r: 0, g: 250, b: 154, a: 255);

        /// <summary>
        /// Name: MediumTurquoise
        /// RGBA: R: 72, G: 209, B: 204, A: 255
        /// Hex:  #FF48D1CC
        /// </summary>
        public static readonly DirectColor MediumTurquoise = DirectColor.FromRgba(r: 72, g: 209, b: 204, a: 255);

        /// <summary>
        /// Name: MediumVioletRed
        /// RGBA: R: 199, G: 21, B: 133, A: 255
        /// Hex:  #FFC71585
        /// </summary>
        public static readonly DirectColor MediumVioletRed = DirectColor.FromRgba(r: 199, g: 21, b: 133, a: 255);

        /// <summary>
        /// Name: MidnightBlue
        /// RGBA: R: 25, G: 25, B: 112, A: 255
        /// Hex:  #FF191970
        /// </summary>
        public static readonly DirectColor MidnightBlue = DirectColor.FromRgba(r: 25, g: 25, b: 112, a: 255);

        /// <summary>
        /// Name: MintCream
        /// RGBA: R: 245, G: 255, B: 250, A: 255
        /// Hex:  #FFF5FFFA
        /// </summary>
        public static readonly DirectColor MintCream = DirectColor.FromRgba(r: 245, g: 255, b: 250, a: 255);

        /// <summary>
        /// Name: MistyRose
        /// RGBA: R: 255, G: 228, B: 225, A: 255
        /// Hex:  #FFFFE4E1
        /// </summary>
        public static readonly DirectColor MistyRose = DirectColor.FromRgba(r: 255, g: 228, b: 225, a: 255);

        /// <summary>
        /// Name: Moccasin
        /// RGBA: R: 255, G: 228, B: 181, A: 255
        /// Hex:  #FFFFE4B5
        /// </summary>
        public static readonly DirectColor Moccasin = DirectColor.FromRgba(r: 255, g: 228, b: 181, a: 255);

        /// <summary>
        /// Name: NavajoWhite
        /// RGBA: R: 255, G: 222, B: 173, A: 255
        /// Hex:  #FFFFDEAD
        /// </summary>
        public static readonly DirectColor NavajoWhite = DirectColor.FromRgba(r: 255, g: 222, b: 173, a: 255);

        /// <summary>
        /// Name: Navy
        /// RGBA: R: 0, G: 0, B: 128, A: 255
        /// Hex:  #FF000080
        /// </summary>
        public static readonly DirectColor Navy = DirectColor.FromRgba(r: 0, g: 0, b: 128, a: 255);

        /// <summary>
        /// Name: OldLace
        /// RGBA: R: 253, G: 245, B: 230, A: 255
        /// Hex:  #FFFDF5E6
        /// </summary>
        public static readonly DirectColor OldLace = DirectColor.FromRgba(r: 253, g: 245, b: 230, a: 255);

        /// <summary>
        /// Name: Olive
        /// RGBA: R: 128, G: 128, B: 0, A: 255
        /// Hex:  #FF808000
        /// </summary>
        public static readonly DirectColor Olive = DirectColor.FromRgba(r: 128, g: 128, b: 0, a: 255);

        /// <summary>
        /// Name: OliveDrab
        /// RGBA: R: 107, G: 142, B: 35, A: 255
        /// Hex:  #FF6B8E23
        /// </summary>
        public static readonly DirectColor OliveDrab = DirectColor.FromRgba(r: 107, g: 142, b: 35, a: 255);

        /// <summary>
        /// Name: Orange
        /// RGBA: R: 255, G: 165, B: 0, A: 255
        /// Hex:  #FFFFA500
        /// </summary>
        public static readonly DirectColor Orange = DirectColor.FromRgba(r: 255, g: 165, b: 0, a: 255);

        /// <summary>
        /// Name: OrangeRed
        /// RGBA: R: 255, G: 69, B: 0, A: 255
        /// Hex:  #FFFF4500
        /// </summary>
        public static readonly DirectColor OrangeRed = DirectColor.FromRgba(r: 255, g: 69, b: 0, a: 255);

        /// <summary>
        /// Name: Orchid
        /// RGBA: R: 218, G: 112, B: 214, A: 255
        /// Hex:  #FFDA70D6
        /// </summary>
        public static readonly DirectColor Orchid = DirectColor.FromRgba(r: 218, g: 112, b: 214, a: 255);

        /// <summary>
        /// Name: PaleGoldenrod
        /// RGBA: R: 238, G: 232, B: 170, A: 255
        /// Hex:  #FFEEE8AA
        /// </summary>
        public static readonly DirectColor PaleGoldenrod = DirectColor.FromRgba(r: 238, g: 232, b: 170, a: 255);

        /// <summary>
        /// Name: PaleGreen
        /// RGBA: R: 152, G: 251, B: 152, A: 255
        /// Hex:  #FF98FB98
        /// </summary>
        public static readonly DirectColor PaleGreen = DirectColor.FromRgba(r: 152, g: 251, b: 152, a: 255);

        /// <summary>
        /// Name: PaleTurquoise
        /// RGBA: R: 175, G: 238, B: 238, A: 255
        /// Hex:  #FFAFEEEE
        /// </summary>
        public static readonly DirectColor PaleTurquoise = DirectColor.FromRgba(r: 175, g: 238, b: 238, a: 255);

        /// <summary>
        /// Name: PaleVioletRed
        /// RGBA: R: 219, G: 112, B: 147, A: 255
        /// Hex:  #FFDB7093
        /// </summary>
        public static readonly DirectColor PaleVioletRed = DirectColor.FromRgba(r: 219, g: 112, b: 147, a: 255);

        /// <summary>
        /// Name: PapayaWhip
        /// RGBA: R: 255, G: 239, B: 213, A: 255
        /// Hex:  #FFFFEFD5
        /// </summary>
        public static readonly DirectColor PapayaWhip = DirectColor.FromRgba(r: 255, g: 239, b: 213, a: 255);

        /// <summary>
        /// Name: PeachPuff
        /// RGBA: R: 255, G: 218, B: 185, A: 255
        /// Hex:  #FFFFDAB9
        /// </summary>
        public static readonly DirectColor PeachPuff = DirectColor.FromRgba(r: 255, g: 218, b: 185, a: 255);

        /// <summary>
        /// Name: Peru
        /// RGBA: R: 205, G: 133, B: 63, A: 255
        /// Hex:  #FFCD853F
        /// </summary>
        public static readonly DirectColor Peru = DirectColor.FromRgba(r: 205, g: 133, b: 63, a: 255);

        /// <summary>
        /// Name: Pink
        /// RGBA: R: 255, G: 192, B: 203, A: 255
        /// Hex:  #FFFFC0CB
        /// </summary>
        public static readonly DirectColor Pink = DirectColor.FromRgba(r: 255, g: 192, b: 203, a: 255);

        /// <summary>
        /// Name: Plum
        /// RGBA: R: 221, G: 160, B: 221, A: 255
        /// Hex:  #FFDDA0DD
        /// </summary>
        public static readonly DirectColor Plum = DirectColor.FromRgba(r: 221, g: 160, b: 221, a: 255);

        /// <summary>
        /// Name: PowderBlue
        /// RGBA: R: 176, G: 224, B: 230, A: 255
        /// Hex:  #FFB0E0E6
        /// </summary>
        public static readonly DirectColor PowderBlue = DirectColor.FromRgba(r: 176, g: 224, b: 230, a: 255);

        /// <summary>
        /// Name: Purple
        /// RGBA: R: 128, G: 0, B: 128, A: 255
        /// Hex:  #FF800080
        /// </summary>
        public static readonly DirectColor Purple = DirectColor.FromRgba(r: 128, g: 0, b: 128, a: 255);

        /// <summary>
        /// Name: RebeccaPurple
        /// RGBA: R: 102, G: 51, B: 153, A: 255
        /// Hex:  #FF663399
        /// </summary>
        public static readonly DirectColor RebeccaPurple = DirectColor.FromRgba(r: 102, g: 51, b: 153, a: 255);

        /// <summary>
        /// Name: Red
        /// RGBA: R: 255, G: 0, B: 0, A: 255
        /// Hex:  #FFFF0000
        /// </summary>
        public static readonly DirectColor Red = DirectColor.FromRgba(r: 255, g: 0, b: 0, a: 255);

        /// <summary>
        /// Name: RosyBrown
        /// RGBA: R: 188, G: 143, B: 143, A: 255
        /// Hex:  #FFBC8F8F
        /// </summary>
        public static readonly DirectColor RosyBrown = DirectColor.FromRgba(r: 188, g: 143, b: 143, a: 255);

        /// <summary>
        /// Name: RoyalBlue
        /// RGBA: R: 65, G: 105, B: 225, A: 255
        /// Hex:  #FF4169E1
        /// </summary>
        public static readonly DirectColor RoyalBlue = DirectColor.FromRgba(r: 65, g: 105, b: 225, a: 255);

        /// <summary>
        /// Name: SaddleBrown
        /// RGBA: R: 139, G: 69, B: 19, A: 255
        /// Hex:  #FF8B4513
        /// </summary>
        public static readonly DirectColor SaddleBrown = DirectColor.FromRgba(r: 139, g: 69, b: 19, a: 255);

        /// <summary>
        /// Name: Salmon
        /// RGBA: R: 250, G: 128, B: 114, A: 255
        /// Hex:  #FFFA8072
        /// </summary>
        public static readonly DirectColor Salmon = DirectColor.FromRgba(r: 250, g: 128, b: 114, a: 255);

        /// <summary>
        /// Name: SandyBrown
        /// RGBA: R: 244, G: 164, B: 96, A: 255
        /// Hex:  #FFF4A460
        /// </summary>
        public static readonly DirectColor SandyBrown = DirectColor.FromRgba(r: 244, g: 164, b: 96, a: 255);

        /// <summary>
        /// Name: SeaGreen
        /// RGBA: R: 46, G: 139, B: 87, A: 255
        /// Hex:  #FF2E8B57
        /// </summary>
        public static readonly DirectColor SeaGreen = DirectColor.FromRgba(r: 46, g: 139, b: 87, a: 255);

        /// <summary>
        /// Name: SeaShell
        /// RGBA: R: 255, G: 245, B: 238, A: 255
        /// Hex:  #FFFFF5EE
        /// </summary>
        public static readonly DirectColor SeaShell = DirectColor.FromRgba(r: 255, g: 245, b: 238, a: 255);

        /// <summary>
        /// Name: Sienna
        /// RGBA: R: 160, G: 82, B: 45, A: 255
        /// Hex:  #FFA0522D
        /// </summary>
        public static readonly DirectColor Sienna = DirectColor.FromRgba(r: 160, g: 82, b: 45, a: 255);

        /// <summary>
        /// Name: Silver
        /// RGBA: R: 192, G: 192, B: 192, A: 255
        /// Hex:  #FFC0C0C0
        /// </summary>
        public static readonly DirectColor Silver = DirectColor.FromRgba(r: 192, g: 192, b: 192, a: 255);

        /// <summary>
        /// Name: SkyBlue
        /// RGBA: R: 135, G: 206, B: 235, A: 255
        /// Hex:  #FF87CEEB
        /// </summary>
        public static readonly DirectColor SkyBlue = DirectColor.FromRgba(r: 135, g: 206, b: 235, a: 255);

        /// <summary>
        /// Name: SlateBlue
        /// RGBA: R: 106, G: 90, B: 205, A: 255
        /// Hex:  #FF6A5ACD
        /// </summary>
        public static readonly DirectColor SlateBlue = DirectColor.FromRgba(r: 106, g: 90, b: 205, a: 255);

        /// <summary>
        /// Name: SlateGray
        /// RGBA: R: 112, G: 128, B: 144, A: 255
        /// Hex:  #FF708090
        /// </summary>
        public static readonly DirectColor SlateGray = DirectColor.FromRgba(r: 112, g: 128, b: 144, a: 255);

        /// <summary>
        /// Name: Snow
        /// RGBA: R: 255, G: 250, B: 250, A: 255
        /// Hex:  #FFFFFAFA
        /// </summary>
        public static readonly DirectColor Snow = DirectColor.FromRgba(r: 255, g: 250, b: 250, a: 255);

        /// <summary>
        /// Name: SpringGreen
        /// RGBA: R: 0, G: 255, B: 127, A: 255
        /// Hex:  #FF00FF7F
        /// </summary>
        public static readonly DirectColor SpringGreen = DirectColor.FromRgba(r: 0, g: 255, b: 127, a: 255);

        /// <summary>
        /// Name: SteelBlue
        /// RGBA: R: 70, G: 130, B: 180, A: 255
        /// Hex:  #FF4682B4
        /// </summary>
        public static readonly DirectColor SteelBlue = DirectColor.FromRgba(r: 70, g: 130, b: 180, a: 255);

        /// <summary>
        /// Name: Tan
        /// RGBA: R: 210, G: 180, B: 140, A: 255
        /// Hex:  #FFD2B48C
        /// </summary>
        public static readonly DirectColor Tan = DirectColor.FromRgba(r: 210, g: 180, b: 140, a: 255);

        /// <summary>
        /// Name: Teal
        /// RGBA: R: 0, G: 128, B: 128, A: 255
        /// Hex:  #FF008080
        /// </summary>
        public static readonly DirectColor Teal = DirectColor.FromRgba(r: 0, g: 128, b: 128, a: 255);

        /// <summary>
        /// Name: Thistle
        /// RGBA: R: 216, G: 191, B: 216, A: 255
        /// Hex:  #FFD8BFD8
        /// </summary>
        public static readonly DirectColor Thistle = DirectColor.FromRgba(r: 216, g: 191, b: 216, a: 255);

        /// <summary>
        /// Name: Tomato
        /// RGBA: R: 255, G: 99, B: 71, A: 255
        /// Hex:  #FFFF6347
        /// </summary>
        public static readonly DirectColor Tomato = DirectColor.FromRgba(r: 255, g: 99, b: 71, a: 255);

        /// <summary>
        /// Name: Turquoise
        /// RGBA: R: 64, G: 224, B: 208, A: 255
        /// Hex:  #FF40E0D0
        /// </summary>
        public static readonly DirectColor Turquoise = DirectColor.FromRgba(r: 64, g: 224, b: 208, a: 255);

        /// <summary>
        /// Name: Violet
        /// RGBA: R: 238, G: 130, B: 238, A: 255
        /// Hex:  #FFEE82EE
        /// </summary>
        public static readonly DirectColor Violet = DirectColor.FromRgba(r: 238, g: 130, b: 238, a: 255);

        /// <summary>
        /// Name: Wheat
        /// RGBA: R: 245, G: 222, B: 179, A: 255
        /// Hex:  #FFF5DEB3
        /// </summary>
        public static readonly DirectColor Wheat = DirectColor.FromRgba(r: 245, g: 222, b: 179, a: 255);

        /// <summary>
        /// Name: White
        /// RGBA: R: 255, G: 255, B: 255, A: 255
        /// Hex:  #FFFFFFFF
        /// </summary>
        public static readonly DirectColor White = DirectColor.FromRgba(r: 255, g: 255, b: 255, a: 255);

        /// <summary>
        /// Name: WhiteSmoke
        /// RGBA: R: 245, G: 245, B: 245, A: 255
        /// Hex:  #FFF5F5F5
        /// </summary>
        public static readonly DirectColor WhiteSmoke = DirectColor.FromRgba(r: 245, g: 245, b: 245, a: 255);

        /// <summary>
        /// Name: Yellow
        /// RGBA: R: 255, G: 255, B: 0, A: 255
        /// Hex:  #FFFFFF00
        /// </summary>
        public static readonly DirectColor Yellow = DirectColor.FromRgba(r: 255, g: 255, b: 0, a: 255);

        /// <summary>
        /// Name: YellowGreen
        /// RGBA: R: 154, G: 205, B: 50, A: 255
        /// Hex:  #FF9ACD32
        /// </summary>
        public static readonly DirectColor YellowGreen = DirectColor.FromRgba(r: 154, g: 205, b: 50, a: 255);

    }

}