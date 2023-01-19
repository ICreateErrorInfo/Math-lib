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
        public static readonly DirectColor Transparent = DirectColor.FromRgba(red: 255, green: 255, blue: 255, alpha: 0);

        /// <summary>
        /// Name: AliceBlue
        /// RGBA: R: 240, G: 248, B: 255, A: 255
        /// Hex:  #FFF0F8FF
        /// </summary>
        public static readonly DirectColor AliceBlue = DirectColor.FromRgba(red: 240, green: 248, blue: 255, alpha: 255);

        /// <summary>
        /// Name: AntiqueWhite
        /// RGBA: R: 250, G: 235, B: 215, A: 255
        /// Hex:  #FFFAEBD7
        /// </summary>
        public static readonly DirectColor AntiqueWhite = DirectColor.FromRgba(red: 250, green: 235, blue: 215, alpha: 255);

        /// <summary>
        /// Name: Aqua
        /// RGBA: R: 0, G: 255, B: 255, A: 255
        /// Hex:  #FF00FFFF
        /// </summary>
        public static readonly DirectColor Aqua = DirectColor.FromRgba(red: 0, green: 255, blue: 255, alpha: 255);

        /// <summary>
        /// Name: Aquamarine
        /// RGBA: R: 127, G: 255, B: 212, A: 255
        /// Hex:  #FF7FFFD4
        /// </summary>
        public static readonly DirectColor Aquamarine = DirectColor.FromRgba(red: 127, green: 255, blue: 212, alpha: 255);

        /// <summary>
        /// Name: Azure
        /// RGBA: R: 240, G: 255, B: 255, A: 255
        /// Hex:  #FFF0FFFF
        /// </summary>
        public static readonly DirectColor Azure = DirectColor.FromRgba(red: 240, green: 255, blue: 255, alpha: 255);

        /// <summary>
        /// Name: Beige
        /// RGBA: R: 245, G: 245, B: 220, A: 255
        /// Hex:  #FFF5F5DC
        /// </summary>
        public static readonly DirectColor Beige = DirectColor.FromRgba(red: 245, green: 245, blue: 220, alpha: 255);

        /// <summary>
        /// Name: Bisque
        /// RGBA: R: 255, G: 228, B: 196, A: 255
        /// Hex:  #FFFFE4C4
        /// </summary>
        public static readonly DirectColor Bisque = DirectColor.FromRgba(red: 255, green: 228, blue: 196, alpha: 255);

        /// <summary>
        /// Name: Black
        /// RGBA: R: 0, G: 0, B: 0, A: 255
        /// Hex:  #FF000000
        /// </summary>
        public static readonly DirectColor Black = DirectColor.FromRgba(red: 0, green: 0, blue: 0, alpha: 255);

        /// <summary>
        /// Name: BlanchedAlmond
        /// RGBA: R: 255, G: 235, B: 205, A: 255
        /// Hex:  #FFFFEBCD
        /// </summary>
        public static readonly DirectColor BlanchedAlmond = DirectColor.FromRgba(red: 255, green: 235, blue: 205, alpha: 255);

        /// <summary>
        /// Name: Blue
        /// RGBA: R: 0, G: 0, B: 255, A: 255
        /// Hex:  #FF0000FF
        /// </summary>
        public static readonly DirectColor Blue = DirectColor.FromRgba(red: 0, green: 0, blue: 255, alpha: 255);

        /// <summary>
        /// Name: BlueViolet
        /// RGBA: R: 138, G: 43, B: 226, A: 255
        /// Hex:  #FF8A2BE2
        /// </summary>
        public static readonly DirectColor BlueViolet = DirectColor.FromRgba(red: 138, green: 43, blue: 226, alpha: 255);

        /// <summary>
        /// Name: Brown
        /// RGBA: R: 165, G: 42, B: 42, A: 255
        /// Hex:  #FFA52A2A
        /// </summary>
        public static readonly DirectColor Brown = DirectColor.FromRgba(red: 165, green: 42, blue: 42, alpha: 255);

        /// <summary>
        /// Name: BurlyWood
        /// RGBA: R: 222, G: 184, B: 135, A: 255
        /// Hex:  #FFDEB887
        /// </summary>
        public static readonly DirectColor BurlyWood = DirectColor.FromRgba(red: 222, green: 184, blue: 135, alpha: 255);

        /// <summary>
        /// Name: CadetBlue
        /// RGBA: R: 95, G: 158, B: 160, A: 255
        /// Hex:  #FF5F9EA0
        /// </summary>
        public static readonly DirectColor CadetBlue = DirectColor.FromRgba(red: 95, green: 158, blue: 160, alpha: 255);

        /// <summary>
        /// Name: Chartreuse
        /// RGBA: R: 127, G: 255, B: 0, A: 255
        /// Hex:  #FF7FFF00
        /// </summary>
        public static readonly DirectColor Chartreuse = DirectColor.FromRgba(red: 127, green: 255, blue: 0, alpha: 255);

        /// <summary>
        /// Name: Chocolate
        /// RGBA: R: 210, G: 105, B: 30, A: 255
        /// Hex:  #FFD2691E
        /// </summary>
        public static readonly DirectColor Chocolate = DirectColor.FromRgba(red: 210, green: 105, blue: 30, alpha: 255);

        /// <summary>
        /// Name: Coral
        /// RGBA: R: 255, G: 127, B: 80, A: 255
        /// Hex:  #FFFF7F50
        /// </summary>
        public static readonly DirectColor Coral = DirectColor.FromRgba(red: 255, green: 127, blue: 80, alpha: 255);

        /// <summary>
        /// Name: CornflowerBlue
        /// RGBA: R: 100, G: 149, B: 237, A: 255
        /// Hex:  #FF6495ED
        /// </summary>
        public static readonly DirectColor CornflowerBlue = DirectColor.FromRgba(red: 100, green: 149, blue: 237, alpha: 255);

        /// <summary>
        /// Name: Cornsilk
        /// RGBA: R: 255, G: 248, B: 220, A: 255
        /// Hex:  #FFFFF8DC
        /// </summary>
        public static readonly DirectColor Cornsilk = DirectColor.FromRgba(red: 255, green: 248, blue: 220, alpha: 255);

        /// <summary>
        /// Name: Crimson
        /// RGBA: R: 220, G: 20, B: 60, A: 255
        /// Hex:  #FFDC143C
        /// </summary>
        public static readonly DirectColor Crimson = DirectColor.FromRgba(red: 220, green: 20, blue: 60, alpha: 255);

        /// <summary>
        /// Name: Cyan
        /// RGBA: R: 0, G: 255, B: 255, A: 255
        /// Hex:  #FF00FFFF
        /// </summary>
        public static readonly DirectColor Cyan = DirectColor.FromRgba(red: 0, green: 255, blue: 255, alpha: 255);

        /// <summary>
        /// Name: DarkBlue
        /// RGBA: R: 0, G: 0, B: 139, A: 255
        /// Hex:  #FF00008B
        /// </summary>
        public static readonly DirectColor DarkBlue = DirectColor.FromRgba(red: 0, green: 0, blue: 139, alpha: 255);

        /// <summary>
        /// Name: DarkCyan
        /// RGBA: R: 0, G: 139, B: 139, A: 255
        /// Hex:  #FF008B8B
        /// </summary>
        public static readonly DirectColor DarkCyan = DirectColor.FromRgba(red: 0, green: 139, blue: 139, alpha: 255);

        /// <summary>
        /// Name: DarkGoldenrod
        /// RGBA: R: 184, G: 134, B: 11, A: 255
        /// Hex:  #FFB8860B
        /// </summary>
        public static readonly DirectColor DarkGoldenrod = DirectColor.FromRgba(red: 184, green: 134, blue: 11, alpha: 255);

        /// <summary>
        /// Name: DarkGray
        /// RGBA: R: 169, G: 169, B: 169, A: 255
        /// Hex:  #FFA9A9A9
        /// </summary>
        public static readonly DirectColor DarkGray = DirectColor.FromRgba(red: 169, green: 169, blue: 169, alpha: 255);

        /// <summary>
        /// Name: DarkGreen
        /// RGBA: R: 0, G: 100, B: 0, A: 255
        /// Hex:  #FF006400
        /// </summary>
        public static readonly DirectColor DarkGreen = DirectColor.FromRgba(red: 0, green: 100, blue: 0, alpha: 255);

        /// <summary>
        /// Name: DarkKhaki
        /// RGBA: R: 189, G: 183, B: 107, A: 255
        /// Hex:  #FFBDB76B
        /// </summary>
        public static readonly DirectColor DarkKhaki = DirectColor.FromRgba(red: 189, green: 183, blue: 107, alpha: 255);

        /// <summary>
        /// Name: DarkMagenta
        /// RGBA: R: 139, G: 0, B: 139, A: 255
        /// Hex:  #FF8B008B
        /// </summary>
        public static readonly DirectColor DarkMagenta = DirectColor.FromRgba(red: 139, green: 0, blue: 139, alpha: 255);

        /// <summary>
        /// Name: DarkOliveGreen
        /// RGBA: R: 85, G: 107, B: 47, A: 255
        /// Hex:  #FF556B2F
        /// </summary>
        public static readonly DirectColor DarkOliveGreen = DirectColor.FromRgba(red: 85, green: 107, blue: 47, alpha: 255);

        /// <summary>
        /// Name: DarkOrange
        /// RGBA: R: 255, G: 140, B: 0, A: 255
        /// Hex:  #FFFF8C00
        /// </summary>
        public static readonly DirectColor DarkOrange = DirectColor.FromRgba(red: 255, green: 140, blue: 0, alpha: 255);

        /// <summary>
        /// Name: DarkOrchid
        /// RGBA: R: 153, G: 50, B: 204, A: 255
        /// Hex:  #FF9932CC
        /// </summary>
        public static readonly DirectColor DarkOrchid = DirectColor.FromRgba(red: 153, green: 50, blue: 204, alpha: 255);

        /// <summary>
        /// Name: DarkRed
        /// RGBA: R: 139, G: 0, B: 0, A: 255
        /// Hex:  #FF8B0000
        /// </summary>
        public static readonly DirectColor DarkRed = DirectColor.FromRgba(red: 139, green: 0, blue: 0, alpha: 255);

        /// <summary>
        /// Name: DarkSalmon
        /// RGBA: R: 233, G: 150, B: 122, A: 255
        /// Hex:  #FFE9967A
        /// </summary>
        public static readonly DirectColor DarkSalmon = DirectColor.FromRgba(red: 233, green: 150, blue: 122, alpha: 255);

        /// <summary>
        /// Name: DarkSeaGreen
        /// RGBA: R: 143, G: 188, B: 143, A: 255
        /// Hex:  #FF8FBC8F
        /// </summary>
        public static readonly DirectColor DarkSeaGreen = DirectColor.FromRgba(red: 143, green: 188, blue: 143, alpha: 255);

        /// <summary>
        /// Name: DarkSlateBlue
        /// RGBA: R: 72, G: 61, B: 139, A: 255
        /// Hex:  #FF483D8B
        /// </summary>
        public static readonly DirectColor DarkSlateBlue = DirectColor.FromRgba(red: 72, green: 61, blue: 139, alpha: 255);

        /// <summary>
        /// Name: DarkSlateGray
        /// RGBA: R: 47, G: 79, B: 79, A: 255
        /// Hex:  #FF2F4F4F
        /// </summary>
        public static readonly DirectColor DarkSlateGray = DirectColor.FromRgba(red: 47, green: 79, blue: 79, alpha: 255);

        /// <summary>
        /// Name: DarkTurquoise
        /// RGBA: R: 0, G: 206, B: 209, A: 255
        /// Hex:  #FF00CED1
        /// </summary>
        public static readonly DirectColor DarkTurquoise = DirectColor.FromRgba(red: 0, green: 206, blue: 209, alpha: 255);

        /// <summary>
        /// Name: DarkViolet
        /// RGBA: R: 148, G: 0, B: 211, A: 255
        /// Hex:  #FF9400D3
        /// </summary>
        public static readonly DirectColor DarkViolet = DirectColor.FromRgba(red: 148, green: 0, blue: 211, alpha: 255);

        /// <summary>
        /// Name: DeepPink
        /// RGBA: R: 255, G: 20, B: 147, A: 255
        /// Hex:  #FFFF1493
        /// </summary>
        public static readonly DirectColor DeepPink = DirectColor.FromRgba(red: 255, green: 20, blue: 147, alpha: 255);

        /// <summary>
        /// Name: DeepSkyBlue
        /// RGBA: R: 0, G: 191, B: 255, A: 255
        /// Hex:  #FF00BFFF
        /// </summary>
        public static readonly DirectColor DeepSkyBlue = DirectColor.FromRgba(red: 0, green: 191, blue: 255, alpha: 255);

        /// <summary>
        /// Name: DimGray
        /// RGBA: R: 105, G: 105, B: 105, A: 255
        /// Hex:  #FF696969
        /// </summary>
        public static readonly DirectColor DimGray = DirectColor.FromRgba(red: 105, green: 105, blue: 105, alpha: 255);

        /// <summary>
        /// Name: DodgerBlue
        /// RGBA: R: 30, G: 144, B: 255, A: 255
        /// Hex:  #FF1E90FF
        /// </summary>
        public static readonly DirectColor DodgerBlue = DirectColor.FromRgba(red: 30, green: 144, blue: 255, alpha: 255);

        /// <summary>
        /// Name: Firebrick
        /// RGBA: R: 178, G: 34, B: 34, A: 255
        /// Hex:  #FFB22222
        /// </summary>
        public static readonly DirectColor Firebrick = DirectColor.FromRgba(red: 178, green: 34, blue: 34, alpha: 255);

        /// <summary>
        /// Name: FloralWhite
        /// RGBA: R: 255, G: 250, B: 240, A: 255
        /// Hex:  #FFFFFAF0
        /// </summary>
        public static readonly DirectColor FloralWhite = DirectColor.FromRgba(red: 255, green: 250, blue: 240, alpha: 255);

        /// <summary>
        /// Name: ForestGreen
        /// RGBA: R: 34, G: 139, B: 34, A: 255
        /// Hex:  #FF228B22
        /// </summary>
        public static readonly DirectColor ForestGreen = DirectColor.FromRgba(red: 34, green: 139, blue: 34, alpha: 255);

        /// <summary>
        /// Name: Fuchsia
        /// RGBA: R: 255, G: 0, B: 255, A: 255
        /// Hex:  #FFFF00FF
        /// </summary>
        public static readonly DirectColor Fuchsia = DirectColor.FromRgba(red: 255, green: 0, blue: 255, alpha: 255);

        /// <summary>
        /// Name: Gainsboro
        /// RGBA: R: 220, G: 220, B: 220, A: 255
        /// Hex:  #FFDCDCDC
        /// </summary>
        public static readonly DirectColor Gainsboro = DirectColor.FromRgba(red: 220, green: 220, blue: 220, alpha: 255);

        /// <summary>
        /// Name: GhostWhite
        /// RGBA: R: 248, G: 248, B: 255, A: 255
        /// Hex:  #FFF8F8FF
        /// </summary>
        public static readonly DirectColor GhostWhite = DirectColor.FromRgba(red: 248, green: 248, blue: 255, alpha: 255);

        /// <summary>
        /// Name: Gold
        /// RGBA: R: 255, G: 215, B: 0, A: 255
        /// Hex:  #FFFFD700
        /// </summary>
        public static readonly DirectColor Gold = DirectColor.FromRgba(red: 255, green: 215, blue: 0, alpha: 255);

        /// <summary>
        /// Name: Goldenrod
        /// RGBA: R: 218, G: 165, B: 32, A: 255
        /// Hex:  #FFDAA520
        /// </summary>
        public static readonly DirectColor Goldenrod = DirectColor.FromRgba(red: 218, green: 165, blue: 32, alpha: 255);

        /// <summary>
        /// Name: Gray
        /// RGBA: R: 128, G: 128, B: 128, A: 255
        /// Hex:  #FF808080
        /// </summary>
        public static readonly DirectColor Gray = DirectColor.FromRgba(red: 128, green: 128, blue: 128, alpha: 255);

        /// <summary>
        /// Name: Green
        /// RGBA: R: 0, G: 128, B: 0, A: 255
        /// Hex:  #FF008000
        /// </summary>
        public static readonly DirectColor Green = DirectColor.FromRgba(red: 0, green: 128, blue: 0, alpha: 255);

        /// <summary>
        /// Name: GreenYellow
        /// RGBA: R: 173, G: 255, B: 47, A: 255
        /// Hex:  #FFADFF2F
        /// </summary>
        public static readonly DirectColor GreenYellow = DirectColor.FromRgba(red: 173, green: 255, blue: 47, alpha: 255);

        /// <summary>
        /// Name: Honeydew
        /// RGBA: R: 240, G: 255, B: 240, A: 255
        /// Hex:  #FFF0FFF0
        /// </summary>
        public static readonly DirectColor Honeydew = DirectColor.FromRgba(red: 240, green: 255, blue: 240, alpha: 255);

        /// <summary>
        /// Name: HotPink
        /// RGBA: R: 255, G: 105, B: 180, A: 255
        /// Hex:  #FFFF69B4
        /// </summary>
        public static readonly DirectColor HotPink = DirectColor.FromRgba(red: 255, green: 105, blue: 180, alpha: 255);

        /// <summary>
        /// Name: IndianRed
        /// RGBA: R: 205, G: 92, B: 92, A: 255
        /// Hex:  #FFCD5C5C
        /// </summary>
        public static readonly DirectColor IndianRed = DirectColor.FromRgba(red: 205, green: 92, blue: 92, alpha: 255);

        /// <summary>
        /// Name: Indigo
        /// RGBA: R: 75, G: 0, B: 130, A: 255
        /// Hex:  #FF4B0082
        /// </summary>
        public static readonly DirectColor Indigo = DirectColor.FromRgba(red: 75, green: 0, blue: 130, alpha: 255);

        /// <summary>
        /// Name: Ivory
        /// RGBA: R: 255, G: 255, B: 240, A: 255
        /// Hex:  #FFFFFFF0
        /// </summary>
        public static readonly DirectColor Ivory = DirectColor.FromRgba(red: 255, green: 255, blue: 240, alpha: 255);

        /// <summary>
        /// Name: Khaki
        /// RGBA: R: 240, G: 230, B: 140, A: 255
        /// Hex:  #FFF0E68C
        /// </summary>
        public static readonly DirectColor Khaki = DirectColor.FromRgba(red: 240, green: 230, blue: 140, alpha: 255);

        /// <summary>
        /// Name: Lavender
        /// RGBA: R: 230, G: 230, B: 250, A: 255
        /// Hex:  #FFE6E6FA
        /// </summary>
        public static readonly DirectColor Lavender = DirectColor.FromRgba(red: 230, green: 230, blue: 250, alpha: 255);

        /// <summary>
        /// Name: LavenderBlush
        /// RGBA: R: 255, G: 240, B: 245, A: 255
        /// Hex:  #FFFFF0F5
        /// </summary>
        public static readonly DirectColor LavenderBlush = DirectColor.FromRgba(red: 255, green: 240, blue: 245, alpha: 255);

        /// <summary>
        /// Name: LawnGreen
        /// RGBA: R: 124, G: 252, B: 0, A: 255
        /// Hex:  #FF7CFC00
        /// </summary>
        public static readonly DirectColor LawnGreen = DirectColor.FromRgba(red: 124, green: 252, blue: 0, alpha: 255);

        /// <summary>
        /// Name: LemonChiffon
        /// RGBA: R: 255, G: 250, B: 205, A: 255
        /// Hex:  #FFFFFACD
        /// </summary>
        public static readonly DirectColor LemonChiffon = DirectColor.FromRgba(red: 255, green: 250, blue: 205, alpha: 255);

        /// <summary>
        /// Name: LightBlue
        /// RGBA: R: 173, G: 216, B: 230, A: 255
        /// Hex:  #FFADD8E6
        /// </summary>
        public static readonly DirectColor LightBlue = DirectColor.FromRgba(red: 173, green: 216, blue: 230, alpha: 255);

        /// <summary>
        /// Name: LightCoral
        /// RGBA: R: 240, G: 128, B: 128, A: 255
        /// Hex:  #FFF08080
        /// </summary>
        public static readonly DirectColor LightCoral = DirectColor.FromRgba(red: 240, green: 128, blue: 128, alpha: 255);

        /// <summary>
        /// Name: LightCyan
        /// RGBA: R: 224, G: 255, B: 255, A: 255
        /// Hex:  #FFE0FFFF
        /// </summary>
        public static readonly DirectColor LightCyan = DirectColor.FromRgba(red: 224, green: 255, blue: 255, alpha: 255);

        /// <summary>
        /// Name: LightGoldenrodYellow
        /// RGBA: R: 250, G: 250, B: 210, A: 255
        /// Hex:  #FFFAFAD2
        /// </summary>
        public static readonly DirectColor LightGoldenrodYellow = DirectColor.FromRgba(red: 250, green: 250, blue: 210, alpha: 255);

        /// <summary>
        /// Name: LightGreen
        /// RGBA: R: 144, G: 238, B: 144, A: 255
        /// Hex:  #FF90EE90
        /// </summary>
        public static readonly DirectColor LightGreen = DirectColor.FromRgba(red: 144, green: 238, blue: 144, alpha: 255);

        /// <summary>
        /// Name: LightGray
        /// RGBA: R: 211, G: 211, B: 211, A: 255
        /// Hex:  #FFD3D3D3
        /// </summary>
        public static readonly DirectColor LightGray = DirectColor.FromRgba(red: 211, green: 211, blue: 211, alpha: 255);

        /// <summary>
        /// Name: LightPink
        /// RGBA: R: 255, G: 182, B: 193, A: 255
        /// Hex:  #FFFFB6C1
        /// </summary>
        public static readonly DirectColor LightPink = DirectColor.FromRgba(red: 255, green: 182, blue: 193, alpha: 255);

        /// <summary>
        /// Name: LightSalmon
        /// RGBA: R: 255, G: 160, B: 122, A: 255
        /// Hex:  #FFFFA07A
        /// </summary>
        public static readonly DirectColor LightSalmon = DirectColor.FromRgba(red: 255, green: 160, blue: 122, alpha: 255);

        /// <summary>
        /// Name: LightSeaGreen
        /// RGBA: R: 32, G: 178, B: 170, A: 255
        /// Hex:  #FF20B2AA
        /// </summary>
        public static readonly DirectColor LightSeaGreen = DirectColor.FromRgba(red: 32, green: 178, blue: 170, alpha: 255);

        /// <summary>
        /// Name: LightSkyBlue
        /// RGBA: R: 135, G: 206, B: 250, A: 255
        /// Hex:  #FF87CEFA
        /// </summary>
        public static readonly DirectColor LightSkyBlue = DirectColor.FromRgba(red: 135, green: 206, blue: 250, alpha: 255);

        /// <summary>
        /// Name: LightSlateGray
        /// RGBA: R: 119, G: 136, B: 153, A: 255
        /// Hex:  #FF778899
        /// </summary>
        public static readonly DirectColor LightSlateGray = DirectColor.FromRgba(red: 119, green: 136, blue: 153, alpha: 255);

        /// <summary>
        /// Name: LightSteelBlue
        /// RGBA: R: 176, G: 196, B: 222, A: 255
        /// Hex:  #FFB0C4DE
        /// </summary>
        public static readonly DirectColor LightSteelBlue = DirectColor.FromRgba(red: 176, green: 196, blue: 222, alpha: 255);

        /// <summary>
        /// Name: LightYellow
        /// RGBA: R: 255, G: 255, B: 224, A: 255
        /// Hex:  #FFFFFFE0
        /// </summary>
        public static readonly DirectColor LightYellow = DirectColor.FromRgba(red: 255, green: 255, blue: 224, alpha: 255);

        /// <summary>
        /// Name: Lime
        /// RGBA: R: 0, G: 255, B: 0, A: 255
        /// Hex:  #FF00FF00
        /// </summary>
        public static readonly DirectColor Lime = DirectColor.FromRgba(red: 0, green: 255, blue: 0, alpha: 255);

        /// <summary>
        /// Name: LimeGreen
        /// RGBA: R: 50, G: 205, B: 50, A: 255
        /// Hex:  #FF32CD32
        /// </summary>
        public static readonly DirectColor LimeGreen = DirectColor.FromRgba(red: 50, green: 205, blue: 50, alpha: 255);

        /// <summary>
        /// Name: Linen
        /// RGBA: R: 250, G: 240, B: 230, A: 255
        /// Hex:  #FFFAF0E6
        /// </summary>
        public static readonly DirectColor Linen = DirectColor.FromRgba(red: 250, green: 240, blue: 230, alpha: 255);

        /// <summary>
        /// Name: Magenta
        /// RGBA: R: 255, G: 0, B: 255, A: 255
        /// Hex:  #FFFF00FF
        /// </summary>
        public static readonly DirectColor Magenta = DirectColor.FromRgba(red: 255, green: 0, blue: 255, alpha: 255);

        /// <summary>
        /// Name: Maroon
        /// RGBA: R: 128, G: 0, B: 0, A: 255
        /// Hex:  #FF800000
        /// </summary>
        public static readonly DirectColor Maroon = DirectColor.FromRgba(red: 128, green: 0, blue: 0, alpha: 255);

        /// <summary>
        /// Name: MediumAquamarine
        /// RGBA: R: 102, G: 205, B: 170, A: 255
        /// Hex:  #FF66CDAA
        /// </summary>
        public static readonly DirectColor MediumAquamarine = DirectColor.FromRgba(red: 102, green: 205, blue: 170, alpha: 255);

        /// <summary>
        /// Name: MediumBlue
        /// RGBA: R: 0, G: 0, B: 205, A: 255
        /// Hex:  #FF0000CD
        /// </summary>
        public static readonly DirectColor MediumBlue = DirectColor.FromRgba(red: 0, green: 0, blue: 205, alpha: 255);

        /// <summary>
        /// Name: MediumOrchid
        /// RGBA: R: 186, G: 85, B: 211, A: 255
        /// Hex:  #FFBA55D3
        /// </summary>
        public static readonly DirectColor MediumOrchid = DirectColor.FromRgba(red: 186, green: 85, blue: 211, alpha: 255);

        /// <summary>
        /// Name: MediumPurple
        /// RGBA: R: 147, G: 112, B: 219, A: 255
        /// Hex:  #FF9370DB
        /// </summary>
        public static readonly DirectColor MediumPurple = DirectColor.FromRgba(red: 147, green: 112, blue: 219, alpha: 255);

        /// <summary>
        /// Name: MediumSeaGreen
        /// RGBA: R: 60, G: 179, B: 113, A: 255
        /// Hex:  #FF3CB371
        /// </summary>
        public static readonly DirectColor MediumSeaGreen = DirectColor.FromRgba(red: 60, green: 179, blue: 113, alpha: 255);

        /// <summary>
        /// Name: MediumSlateBlue
        /// RGBA: R: 123, G: 104, B: 238, A: 255
        /// Hex:  #FF7B68EE
        /// </summary>
        public static readonly DirectColor MediumSlateBlue = DirectColor.FromRgba(red: 123, green: 104, blue: 238, alpha: 255);

        /// <summary>
        /// Name: MediumSpringGreen
        /// RGBA: R: 0, G: 250, B: 154, A: 255
        /// Hex:  #FF00FA9A
        /// </summary>
        public static readonly DirectColor MediumSpringGreen = DirectColor.FromRgba(red: 0, green: 250, blue: 154, alpha: 255);

        /// <summary>
        /// Name: MediumTurquoise
        /// RGBA: R: 72, G: 209, B: 204, A: 255
        /// Hex:  #FF48D1CC
        /// </summary>
        public static readonly DirectColor MediumTurquoise = DirectColor.FromRgba(red: 72, green: 209, blue: 204, alpha: 255);

        /// <summary>
        /// Name: MediumVioletRed
        /// RGBA: R: 199, G: 21, B: 133, A: 255
        /// Hex:  #FFC71585
        /// </summary>
        public static readonly DirectColor MediumVioletRed = DirectColor.FromRgba(red: 199, green: 21, blue: 133, alpha: 255);

        /// <summary>
        /// Name: MidnightBlue
        /// RGBA: R: 25, G: 25, B: 112, A: 255
        /// Hex:  #FF191970
        /// </summary>
        public static readonly DirectColor MidnightBlue = DirectColor.FromRgba(red: 25, green: 25, blue: 112, alpha: 255);

        /// <summary>
        /// Name: MintCream
        /// RGBA: R: 245, G: 255, B: 250, A: 255
        /// Hex:  #FFF5FFFA
        /// </summary>
        public static readonly DirectColor MintCream = DirectColor.FromRgba(red: 245, green: 255, blue: 250, alpha: 255);

        /// <summary>
        /// Name: MistyRose
        /// RGBA: R: 255, G: 228, B: 225, A: 255
        /// Hex:  #FFFFE4E1
        /// </summary>
        public static readonly DirectColor MistyRose = DirectColor.FromRgba(red: 255, green: 228, blue: 225, alpha: 255);

        /// <summary>
        /// Name: Moccasin
        /// RGBA: R: 255, G: 228, B: 181, A: 255
        /// Hex:  #FFFFE4B5
        /// </summary>
        public static readonly DirectColor Moccasin = DirectColor.FromRgba(red: 255, green: 228, blue: 181, alpha: 255);

        /// <summary>
        /// Name: NavajoWhite
        /// RGBA: R: 255, G: 222, B: 173, A: 255
        /// Hex:  #FFFFDEAD
        /// </summary>
        public static readonly DirectColor NavajoWhite = DirectColor.FromRgba(red: 255, green: 222, blue: 173, alpha: 255);

        /// <summary>
        /// Name: Navy
        /// RGBA: R: 0, G: 0, B: 128, A: 255
        /// Hex:  #FF000080
        /// </summary>
        public static readonly DirectColor Navy = DirectColor.FromRgba(red: 0, green: 0, blue: 128, alpha: 255);

        /// <summary>
        /// Name: OldLace
        /// RGBA: R: 253, G: 245, B: 230, A: 255
        /// Hex:  #FFFDF5E6
        /// </summary>
        public static readonly DirectColor OldLace = DirectColor.FromRgba(red: 253, green: 245, blue: 230, alpha: 255);

        /// <summary>
        /// Name: Olive
        /// RGBA: R: 128, G: 128, B: 0, A: 255
        /// Hex:  #FF808000
        /// </summary>
        public static readonly DirectColor Olive = DirectColor.FromRgba(red: 128, green: 128, blue: 0, alpha: 255);

        /// <summary>
        /// Name: OliveDrab
        /// RGBA: R: 107, G: 142, B: 35, A: 255
        /// Hex:  #FF6B8E23
        /// </summary>
        public static readonly DirectColor OliveDrab = DirectColor.FromRgba(red: 107, green: 142, blue: 35, alpha: 255);

        /// <summary>
        /// Name: Orange
        /// RGBA: R: 255, G: 165, B: 0, A: 255
        /// Hex:  #FFFFA500
        /// </summary>
        public static readonly DirectColor Orange = DirectColor.FromRgba(red: 255, green: 165, blue: 0, alpha: 255);

        /// <summary>
        /// Name: OrangeRed
        /// RGBA: R: 255, G: 69, B: 0, A: 255
        /// Hex:  #FFFF4500
        /// </summary>
        public static readonly DirectColor OrangeRed = DirectColor.FromRgba(red: 255, green: 69, blue: 0, alpha: 255);

        /// <summary>
        /// Name: Orchid
        /// RGBA: R: 218, G: 112, B: 214, A: 255
        /// Hex:  #FFDA70D6
        /// </summary>
        public static readonly DirectColor Orchid = DirectColor.FromRgba(red: 218, green: 112, blue: 214, alpha: 255);

        /// <summary>
        /// Name: PaleGoldenrod
        /// RGBA: R: 238, G: 232, B: 170, A: 255
        /// Hex:  #FFEEE8AA
        /// </summary>
        public static readonly DirectColor PaleGoldenrod = DirectColor.FromRgba(red: 238, green: 232, blue: 170, alpha: 255);

        /// <summary>
        /// Name: PaleGreen
        /// RGBA: R: 152, G: 251, B: 152, A: 255
        /// Hex:  #FF98FB98
        /// </summary>
        public static readonly DirectColor PaleGreen = DirectColor.FromRgba(red: 152, green: 251, blue: 152, alpha: 255);

        /// <summary>
        /// Name: PaleTurquoise
        /// RGBA: R: 175, G: 238, B: 238, A: 255
        /// Hex:  #FFAFEEEE
        /// </summary>
        public static readonly DirectColor PaleTurquoise = DirectColor.FromRgba(red: 175, green: 238, blue: 238, alpha: 255);

        /// <summary>
        /// Name: PaleVioletRed
        /// RGBA: R: 219, G: 112, B: 147, A: 255
        /// Hex:  #FFDB7093
        /// </summary>
        public static readonly DirectColor PaleVioletRed = DirectColor.FromRgba(red: 219, green: 112, blue: 147, alpha: 255);

        /// <summary>
        /// Name: PapayaWhip
        /// RGBA: R: 255, G: 239, B: 213, A: 255
        /// Hex:  #FFFFEFD5
        /// </summary>
        public static readonly DirectColor PapayaWhip = DirectColor.FromRgba(red: 255, green: 239, blue: 213, alpha: 255);

        /// <summary>
        /// Name: PeachPuff
        /// RGBA: R: 255, G: 218, B: 185, A: 255
        /// Hex:  #FFFFDAB9
        /// </summary>
        public static readonly DirectColor PeachPuff = DirectColor.FromRgba(red: 255, green: 218, blue: 185, alpha: 255);

        /// <summary>
        /// Name: Peru
        /// RGBA: R: 205, G: 133, B: 63, A: 255
        /// Hex:  #FFCD853F
        /// </summary>
        public static readonly DirectColor Peru = DirectColor.FromRgba(red: 205, green: 133, blue: 63, alpha: 255);

        /// <summary>
        /// Name: Pink
        /// RGBA: R: 255, G: 192, B: 203, A: 255
        /// Hex:  #FFFFC0CB
        /// </summary>
        public static readonly DirectColor Pink = DirectColor.FromRgba(red: 255, green: 192, blue: 203, alpha: 255);

        /// <summary>
        /// Name: Plum
        /// RGBA: R: 221, G: 160, B: 221, A: 255
        /// Hex:  #FFDDA0DD
        /// </summary>
        public static readonly DirectColor Plum = DirectColor.FromRgba(red: 221, green: 160, blue: 221, alpha: 255);

        /// <summary>
        /// Name: PowderBlue
        /// RGBA: R: 176, G: 224, B: 230, A: 255
        /// Hex:  #FFB0E0E6
        /// </summary>
        public static readonly DirectColor PowderBlue = DirectColor.FromRgba(red: 176, green: 224, blue: 230, alpha: 255);

        /// <summary>
        /// Name: Purple
        /// RGBA: R: 128, G: 0, B: 128, A: 255
        /// Hex:  #FF800080
        /// </summary>
        public static readonly DirectColor Purple = DirectColor.FromRgba(red: 128, green: 0, blue: 128, alpha: 255);

        /// <summary>
        /// Name: RebeccaPurple
        /// RGBA: R: 102, G: 51, B: 153, A: 255
        /// Hex:  #FF663399
        /// </summary>
        public static readonly DirectColor RebeccaPurple = DirectColor.FromRgba(red: 102, green: 51, blue: 153, alpha: 255);

        /// <summary>
        /// Name: Red
        /// RGBA: R: 255, G: 0, B: 0, A: 255
        /// Hex:  #FFFF0000
        /// </summary>
        public static readonly DirectColor Red = DirectColor.FromRgba(red: 255, green: 0, blue: 0, alpha: 255);

        /// <summary>
        /// Name: RosyBrown
        /// RGBA: R: 188, G: 143, B: 143, A: 255
        /// Hex:  #FFBC8F8F
        /// </summary>
        public static readonly DirectColor RosyBrown = DirectColor.FromRgba(red: 188, green: 143, blue: 143, alpha: 255);

        /// <summary>
        /// Name: RoyalBlue
        /// RGBA: R: 65, G: 105, B: 225, A: 255
        /// Hex:  #FF4169E1
        /// </summary>
        public static readonly DirectColor RoyalBlue = DirectColor.FromRgba(red: 65, green: 105, blue: 225, alpha: 255);

        /// <summary>
        /// Name: SaddleBrown
        /// RGBA: R: 139, G: 69, B: 19, A: 255
        /// Hex:  #FF8B4513
        /// </summary>
        public static readonly DirectColor SaddleBrown = DirectColor.FromRgba(red: 139, green: 69, blue: 19, alpha: 255);

        /// <summary>
        /// Name: Salmon
        /// RGBA: R: 250, G: 128, B: 114, A: 255
        /// Hex:  #FFFA8072
        /// </summary>
        public static readonly DirectColor Salmon = DirectColor.FromRgba(red: 250, green: 128, blue: 114, alpha: 255);

        /// <summary>
        /// Name: SandyBrown
        /// RGBA: R: 244, G: 164, B: 96, A: 255
        /// Hex:  #FFF4A460
        /// </summary>
        public static readonly DirectColor SandyBrown = DirectColor.FromRgba(red: 244, green: 164, blue: 96, alpha: 255);

        /// <summary>
        /// Name: SeaGreen
        /// RGBA: R: 46, G: 139, B: 87, A: 255
        /// Hex:  #FF2E8B57
        /// </summary>
        public static readonly DirectColor SeaGreen = DirectColor.FromRgba(red: 46, green: 139, blue: 87, alpha: 255);

        /// <summary>
        /// Name: SeaShell
        /// RGBA: R: 255, G: 245, B: 238, A: 255
        /// Hex:  #FFFFF5EE
        /// </summary>
        public static readonly DirectColor SeaShell = DirectColor.FromRgba(red: 255, green: 245, blue: 238, alpha: 255);

        /// <summary>
        /// Name: Sienna
        /// RGBA: R: 160, G: 82, B: 45, A: 255
        /// Hex:  #FFA0522D
        /// </summary>
        public static readonly DirectColor Sienna = DirectColor.FromRgba(red: 160, green: 82, blue: 45, alpha: 255);

        /// <summary>
        /// Name: Silver
        /// RGBA: R: 192, G: 192, B: 192, A: 255
        /// Hex:  #FFC0C0C0
        /// </summary>
        public static readonly DirectColor Silver = DirectColor.FromRgba(red: 192, green: 192, blue: 192, alpha: 255);

        /// <summary>
        /// Name: SkyBlue
        /// RGBA: R: 135, G: 206, B: 235, A: 255
        /// Hex:  #FF87CEEB
        /// </summary>
        public static readonly DirectColor SkyBlue = DirectColor.FromRgba(red: 135, green: 206, blue: 235, alpha: 255);

        /// <summary>
        /// Name: SlateBlue
        /// RGBA: R: 106, G: 90, B: 205, A: 255
        /// Hex:  #FF6A5ACD
        /// </summary>
        public static readonly DirectColor SlateBlue = DirectColor.FromRgba(red: 106, green: 90, blue: 205, alpha: 255);

        /// <summary>
        /// Name: SlateGray
        /// RGBA: R: 112, G: 128, B: 144, A: 255
        /// Hex:  #FF708090
        /// </summary>
        public static readonly DirectColor SlateGray = DirectColor.FromRgba(red: 112, green: 128, blue: 144, alpha: 255);

        /// <summary>
        /// Name: Snow
        /// RGBA: R: 255, G: 250, B: 250, A: 255
        /// Hex:  #FFFFFAFA
        /// </summary>
        public static readonly DirectColor Snow = DirectColor.FromRgba(red: 255, green: 250, blue: 250, alpha: 255);

        /// <summary>
        /// Name: SpringGreen
        /// RGBA: R: 0, G: 255, B: 127, A: 255
        /// Hex:  #FF00FF7F
        /// </summary>
        public static readonly DirectColor SpringGreen = DirectColor.FromRgba(red: 0, green: 255, blue: 127, alpha: 255);

        /// <summary>
        /// Name: SteelBlue
        /// RGBA: R: 70, G: 130, B: 180, A: 255
        /// Hex:  #FF4682B4
        /// </summary>
        public static readonly DirectColor SteelBlue = DirectColor.FromRgba(red: 70, green: 130, blue: 180, alpha: 255);

        /// <summary>
        /// Name: Tan
        /// RGBA: R: 210, G: 180, B: 140, A: 255
        /// Hex:  #FFD2B48C
        /// </summary>
        public static readonly DirectColor Tan = DirectColor.FromRgba(red: 210, green: 180, blue: 140, alpha: 255);

        /// <summary>
        /// Name: Teal
        /// RGBA: R: 0, G: 128, B: 128, A: 255
        /// Hex:  #FF008080
        /// </summary>
        public static readonly DirectColor Teal = DirectColor.FromRgba(red: 0, green: 128, blue: 128, alpha: 255);

        /// <summary>
        /// Name: Thistle
        /// RGBA: R: 216, G: 191, B: 216, A: 255
        /// Hex:  #FFD8BFD8
        /// </summary>
        public static readonly DirectColor Thistle = DirectColor.FromRgba(red: 216, green: 191, blue: 216, alpha: 255);

        /// <summary>
        /// Name: Tomato
        /// RGBA: R: 255, G: 99, B: 71, A: 255
        /// Hex:  #FFFF6347
        /// </summary>
        public static readonly DirectColor Tomato = DirectColor.FromRgba(red: 255, green: 99, blue: 71, alpha: 255);

        /// <summary>
        /// Name: Turquoise
        /// RGBA: R: 64, G: 224, B: 208, A: 255
        /// Hex:  #FF40E0D0
        /// </summary>
        public static readonly DirectColor Turquoise = DirectColor.FromRgba(red: 64, green: 224, blue: 208, alpha: 255);

        /// <summary>
        /// Name: Violet
        /// RGBA: R: 238, G: 130, B: 238, A: 255
        /// Hex:  #FFEE82EE
        /// </summary>
        public static readonly DirectColor Violet = DirectColor.FromRgba(red: 238, green: 130, blue: 238, alpha: 255);

        /// <summary>
        /// Name: Wheat
        /// RGBA: R: 245, G: 222, B: 179, A: 255
        /// Hex:  #FFF5DEB3
        /// </summary>
        public static readonly DirectColor Wheat = DirectColor.FromRgba(red: 245, green: 222, blue: 179, alpha: 255);

        /// <summary>
        /// Name: White
        /// RGBA: R: 255, G: 255, B: 255, A: 255
        /// Hex:  #FFFFFFFF
        /// </summary>
        public static readonly DirectColor White = DirectColor.FromRgba(red: 255, green: 255, blue: 255, alpha: 255);

        /// <summary>
        /// Name: WhiteSmoke
        /// RGBA: R: 245, G: 245, B: 245, A: 255
        /// Hex:  #FFF5F5F5
        /// </summary>
        public static readonly DirectColor WhiteSmoke = DirectColor.FromRgba(red: 245, green: 245, blue: 245, alpha: 255);

        /// <summary>
        /// Name: Yellow
        /// RGBA: R: 255, G: 255, B: 0, A: 255
        /// Hex:  #FFFFFF00
        /// </summary>
        public static readonly DirectColor Yellow = DirectColor.FromRgba(red: 255, green: 255, blue: 0, alpha: 255);

        /// <summary>
        /// Name: YellowGreen
        /// RGBA: R: 154, G: 205, B: 50, A: 255
        /// Hex:  #FF9ACD32
        /// </summary>
        public static readonly DirectColor YellowGreen = DirectColor.FromRgba(red: 154, green: 205, blue: 50, alpha: 255);

    }

}