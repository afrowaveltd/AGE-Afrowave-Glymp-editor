using AGE_Afrowave_Glyph_editor.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AGE_Afrowave_Glyph_editor.ViewModels;

public sealed class MainViewModel
{
   public Glyph Glyph { get; }

   public ObservableCollection<PixelCellViewModel> Pixels { get; }

   public int CellSize { get; } = 24;

   public int EditorWidth => Width * CellSize;
   public int EditorHeight => Height * CellSize;

   public int Width => Glyph.Width;
   public int Height => Glyph.Height;

   public int SelectedRow { get; private set; } = -1;

   public MainViewModel()
   {
      // zatím natvrdo, později presety
      Glyph = new Glyph(8, 16);
      Pixels = new ObservableCollection<PixelCellViewModel>();

      BuildPixelGrid();
   }

   private void BuildPixelGrid()
   {
      Pixels.Clear();

      for(int y = 0; y < Glyph.Height; y++)
      {
         for(int x = 0; x < Glyph.Width; x++)
         {
            Pixels.Add(new PixelCellViewModel(Glyph, x, y));
         }
      }
   }

   // klávesy – zatím jen příprava
   public void SelectRow(int row)
   {
      if(row < 0 || row >= Height)
         return;

      SelectedRow = row;
      // později vizuální zvýraznění
   }

   public void ToggleAt(int column)
   {
      if(SelectedRow < 0) return;
      if(column < 0 || column >= Width) return;

      Glyph.TogglePixel(column, SelectedRow);

      // informujeme konkrétní pixel
      int index = (SelectedRow * Width) + column;
      Pixels[index].Toggle();
   }
}