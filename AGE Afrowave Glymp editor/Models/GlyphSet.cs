using System;
using System.Collections.Generic;

namespace AGE_Afrowave_Glyph_editor.Models;

public sealed class GlyphSet
{
   public required int Width { get; init; }
   public required int Height { get; init; }

   public Dictionary<int, Glyph> Unicode { get; } = new();
   public Dictionary<string, Glyph> Internal { get; } = new(StringComparer.OrdinalIgnoreCase);
}