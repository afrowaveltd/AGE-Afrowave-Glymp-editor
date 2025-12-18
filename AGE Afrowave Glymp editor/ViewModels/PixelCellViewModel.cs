using AGE_Afrowave_Glyph_editor.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace AGE_Afrowave_Glyph_editor.ViewModels;

public sealed class PixelCellViewModel(Glyph glyph, int x, int y) : INotifyPropertyChanged
{
   private readonly Glyph _glyph = glyph;
   private readonly int _x = x;
   private readonly int _y = y;

   public bool IsOn
   {
      get => _glyph.GetPixel(_x, _y);
      set
      {
         _glyph.SetPixel(_x, _y, value);
         OnPropertyChanged();
      }
   }

   public void Toggle()
   {
      _glyph.TogglePixel(_x, _y);
      OnPropertyChanged(nameof(IsOn));
   }

   public event PropertyChangedEventHandler? PropertyChanged;

   private void OnPropertyChanged([CallerMemberName] string? name = null)
       => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}