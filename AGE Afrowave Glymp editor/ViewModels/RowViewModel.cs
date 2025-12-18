using AGE_Afrowave_Glyph_editor.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace AGE_Afrowave_Glyph_editor.ViewModels
{
   public sealed class RowViewModel : INotifyPropertyChanged
   {
      private readonly Glyph _glyph;
      public int RowIndex { get; }
      public string RowLabel { get; }

      public ObservableCollection<PixelCellViewModel> Cells { get; }

      private string _hexText = "";
      public string HexText
      {
         get => _hexText;
         private set { _hexText = value; OnPropertyChanged(); }
      }

      private string _binText = "";
      public string BinText
      {
         get => _binText;
         private set { _binText = value; OnPropertyChanged(); }
      }

      public RowViewModel(Glyph glyph, int rowIndex)
      {
         _glyph = glyph;
         RowIndex = rowIndex;
         RowLabel = RowIndexToLabel(rowIndex);

         Cells = new ObservableCollection<PixelCellViewModel>();
         for(int x = 0; x < glyph.Width; x++)
         {
         Cells.Add(new PixelCellViewModel(glyph, x, rowIndex, () => Recompute()));
         }

         Recompute();
      }

      public void Recompute()
      {
         // Sestavíme bajty v řádku (MSB-left)
         int rowBytes = (_glyph.Width + 7) / 8;
         Span<byte> bytes = rowBytes <= 64 ? stackalloc byte[rowBytes] : new byte[rowBytes];

         for(int b = 0; b < rowBytes; b++)
         {
            byte v = 0;
            for(int bit = 0; bit < 8; bit++)
            {
               int x = (b * 8) + bit;
               if(x >= _glyph.Width) break;
               if(_glyph.GetPixel(x, RowIndex))
                  v |= (byte)(1 << (7 - bit));
            }
            bytes[b] = v;
         }

         // HEX (např. "3C" nebo "3C 81" – zatím bez mezer, kdybys chtěl, přidáme)
         var sbHex = new StringBuilder(rowBytes * 2);
         for(int i = 0; i < bytes.Length; i++)
            sbHex.Append(bytes[i].ToString("X2"));
         HexText = sbHex.ToString();

         // BIN (skupiny po 4 bitech s mezerou: "0011 1100")
         // Pro více bajtů to bude delší: "0011 1100 1000 0001"
         var sbBin = new StringBuilder(rowBytes * 8 + rowBytes * 2);
         for(int i = 0; i < bytes.Length; i++)
         {
            for(int bit = 7; bit >= 0; bit--)
            {
               sbBin.Append(((bytes[i] >> bit) & 1) == 1 ? '1' : '0');
               if(bit == 4) sbBin.Append(' '); // 0000 1111
            }
            if(i < bytes.Length - 1) sbBin.Append(' ');
         }
         BinText = sbBin.ToString();
      }

      private static string RowIndexToLabel(int rowIndex)
      {
         // A=0, B=1... P=15 (pro 16 řádků)
         if(rowIndex is >= 0 and < 26)
            return ((char)('A' + rowIndex)).ToString();
         return rowIndex.ToString();
      }

      public event PropertyChangedEventHandler? PropertyChanged;
      private void OnPropertyChanged([CallerMemberName] string? name = null)
          => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
   }
}