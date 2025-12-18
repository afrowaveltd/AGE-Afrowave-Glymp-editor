using AGE_Afrowave_Glyph_editor.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AGE_Afrowave_Glyph_editor.ViewModels;

public sealed class MainViewModel
{
   public Glyph Glyph { get; }

   public int Width => Glyph.Width;
   public int Height => Glyph.Height;

   public int CellSize { get; } = 22;
   public int EditorWidth => Width * CellSize;

   public ObservableCollection<RowViewModel> Rows { get; }

   public List<string> ColumnLabels { get; }

   public MainViewModel()
   {

      Glyph = new Glyph(8, 16);

      ColumnLabels = Enumerable.Range(0, Glyph.Width)
          .Select(i => i < 10 ? i.ToString() : ((char)('A' + (i - 10))).ToString())
          .ToList();

      Rows = new ObservableCollection<RowViewModel>();
      for(int y = 0; y < Glyph.Height; y++)
         Rows.Add(new RowViewModel(Glyph, y));
   }

   private void ToggleTheme()
   {
      var app = Avalonia.Application.Current;
      if(app is null) return;

      app.RequestedThemeVariant =
          app.RequestedThemeVariant == Avalonia.Styling.ThemeVariant.Dark
              ? Avalonia.Styling.ThemeVariant.Light
              : Avalonia.Styling.ThemeVariant.Dark;
   }

}