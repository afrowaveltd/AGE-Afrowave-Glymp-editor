using System;
using System.Collections.Generic;
using System.Text;

namespace AGE_Afrowave_Glyph_editor.Models;

public sealed class Glyph
{
   public int Width { get; }
   public int Height { get; }

   // 1bpp bitmap: row-major (y * width + x)
   private readonly bool[] _pixels;

   public Glyph(int width, int height)
   {
      if(width <= 0) throw new ArgumentOutOfRangeException(nameof(width));
      if(height <= 0) throw new ArgumentOutOfRangeException(nameof(height));

      Width = width;
      Height = height;
      _pixels = new bool[width * height];
   }

   public bool GetPixel(int x, int y)
   {
      ValidateXY(x, y);
      return _pixels[(y * Width) + x];
   }

   public void SetPixel(int x, int y, bool value)
   {
      ValidateXY(x, y);
      _pixels[(y * Width) + x] = value;
   }

   public bool TogglePixel(int x, int y)
   {
      ValidateXY(x, y);
      int i = (y * Width) + x;
      _pixels[i] = !_pixels[i];
      return _pixels[i];
   }

   public void Clear()
   {
      Array.Clear(_pixels);
   }

   public int CountOn()
   {
      int c = 0;
      for(int i = 0; i < _pixels.Length; i++)
         if(_pixels[i]) c++;
      return c;
   }

   public bool[] SnapshotPixels()
   {
      // kopie pro rendering / preview
      return (bool[])_pixels.Clone();
   }

   private void ValidateXY(int x, int y)
   {
      if((uint)x >= (uint)Width) throw new ArgumentOutOfRangeException(nameof(x));
      if((uint)y >= (uint)Height) throw new ArgumentOutOfRangeException(nameof(y));
   }
}