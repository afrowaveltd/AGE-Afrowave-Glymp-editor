using AGE_Afrowave_Glyph_editor.IO;
using AGE_Afrowave_Glyph_editor.Models;
using System;
using System.IO;

namespace AGE_Afrowave_Glymp_editor.Services;

public static class GlyphSetLoader
{
   public static GlyphSet LoadFolder(string folderPath, int width, int height)
   {
      var set = new GlyphSet { Width = width, Height = height };

      foreach(var file in Directory.EnumerateFiles(folderPath, "*.glyph"))
      {
         var name = Path.GetFileNameWithoutExtension(file);

         // interní: _NAME.glyph
         if(name.StartsWith("_", StringComparison.Ordinal))
         {
            var glyph = GlyphHexCodec.LoadFromLines(File.ReadLines(file), width, height);
            set.Internal[name] = glyph; // klíč včetně _
            continue;
         }

         // unicode: HEX.glyph (bez U+ a bez paddingu)
         if(!int.TryParse(name, System.Globalization.NumberStyles.HexNumber,
                 System.Globalization.CultureInfo.InvariantCulture, out int cp))
            continue; // nebo hoď error / log

         var g = GlyphHexCodec.LoadFromLines(File.ReadLines(file), width, height);
         set.Unicode[cp] = g;
      }

      return set;
   }
}