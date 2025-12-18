using AGE_Afrowave_Glyph_editor.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace AGE_Afrowave_Glyph_editor.IO;

public static class GlyphHexCodec
{
   // MSB-first: bit 7 je x=0 (levý pixel), bit 0 je x=7
   // Pro width>8 skládáme více bajtů vedle sebe.
   public static Glyph LoadFromLines(IEnumerable<string> lines, int width, int height)
   {
      ArgumentOutOfRangeException.ThrowIfNegativeOrZero(width);
      ArgumentOutOfRangeException.ThrowIfNegativeOrZero(height);

      var cleaned = lines
          .Select(l => l.Trim())
          .Where(l => !string.IsNullOrWhiteSpace(l))
          .ToList();

      if(cleaned.Count != height)
         throw new FormatException($"Expected {height} rows, but got {cleaned.Count}.");

      int rowBytes = (width + 7) / 8;

      var glyph = new Glyph(width, height);

      for(int y = 0; y < height; y++)
      {
         // dovolíme: "3A04" i "3A 04"
         var hex = cleaned[y].Replace(" ", "", StringComparison.Ordinal);
         if(hex.Length != rowBytes * 2)
            throw new FormatException($"Row {y}: expected {rowBytes * 2} hex chars, got {hex.Length}.");

         for(int b = 0; b < rowBytes; b++)
         {
            string byteHex = hex.Substring(b * 2, 2);
            byte value = byte.Parse(byteHex, NumberStyles.HexNumber, CultureInfo.InvariantCulture);

            for(int bit = 0; bit < 8; bit++)
            {
               int x = (b * 8) + bit;
               if(x >= width) break;

               bool on = (value & (1 << (7 - bit))) != 0;
               glyph.SetPixel(x, y, on);
            }
         }
      }

      return glyph;
   }

   public static string SaveToText(Glyph glyph, bool spaced = false, bool uppercase = true)
   {
      int rowBytes = (glyph.Width + 7) / 8;
      var sb = new StringBuilder();

      for(int y = 0; y < glyph.Height; y++)
      {
         for(int b = 0; b < rowBytes; b++)
         {
            byte value = 0;

            for(int bit = 0; bit < 8; bit++)
            {
               int x = (b * 8) + bit;
               if(x >= glyph.Width) break;

               if(glyph.GetPixel(x, y))
                  value |= (byte)(1 << (7 - bit));
            }

            string hx = value.ToString(uppercase ? "X2" : "x2", CultureInfo.InvariantCulture);
            sb.Append(hx);

            if(spaced && b < rowBytes - 1)
               sb.Append(' ');
         }

         if(y < glyph.Height - 1)
            sb.AppendLine();
      }

      return sb.ToString();
   }
}