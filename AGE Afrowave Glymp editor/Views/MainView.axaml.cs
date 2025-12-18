using AGE_Afrowave_Glyph_editor.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;

namespace AGE_Afrowave_Glyph_editor;

public partial class MainView : Window
{
   public MainView()
   {
      InitializeComponent();
   }

   private void Pixel_PointerPressed(object? sender, PointerPressedEventArgs e)
   {
      if(sender is Border b &&
          b.DataContext is PixelCellViewModel pixel)
      {
         pixel.Toggle();
      }
   }
}